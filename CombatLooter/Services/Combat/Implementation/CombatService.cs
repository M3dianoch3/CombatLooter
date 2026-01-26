using CombatLooter.Classes.Implementation;
using CombatLooter.Classes.Implementation.V0.Player;
using CombatLooter.Constants;
using CombatLooter.Enum;
using CombatLooter.Events.Implementation;
using CombatLooter.Services.Combat.Interface;
using CombatLooter.Services.Models;
using Microsoft.Extensions.Logging;

namespace CombatLooter.Services.Combat.Implementation
{
    public class CombatService : ICombatService
    {
        private List<BaseBeing> _enemies;
        private Player _player;
        private readonly Random _rng = new();

        private readonly ILogger _logger;

        private List<TurnDetails> _turnsDetails = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        public CombatService(Player player, ILogger logger)
        {
            _player = player;
            _enemies = new List<BaseBeing>();
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        /// <param name="enemies"></param>
        public CombatService(Player player, List<BaseBeing> enemies, ILogger logger)
        {
            _player = player;
            _enemies = enemies;
            _logger = logger;
        }

        #region Combat Logic

        /// <summary>
        /// Runs the combat simulation until either the player dies or all enemies are dead.
        /// - First round: order is by dexterity (highest first).
        /// - Subsequent actions: scheduled by weapon attack speed (fast weapons attack more often).
        /// Returns true if player survives, false if player dies.
        /// Optional logger receives plain-text events for debugging/observability.
        /// Event handler onDamage is invoked on each damage event.
        /// </summary>
        public bool RunCombat(Action<string>? logger = null, EventHandler<DamageEventArgs> onDamage = null)
        {
            logger?.Invoke("Combat started.");

            // Turn count
            var turnNumber = 1;
            
            // Defensive copy of active enemies
            _enemies = _enemies.Where(e => e.GetCurrentHealth() > 0).ToList();

            // Helper to test alive
            static bool IsAlive(BaseBeing b) => b.GetCurrentHealth() > 0;

            // Helper to pick player's target: enemy with the lowest health, random tie-break
            BaseBeing? PickPlayerTarget()
            {
                var alive = _enemies.Where(IsAlive).ToList();
                if (!alive.Any()) return null;
                var minHp = alive.Min(e => e.GetCurrentHealth());
                var candidates = alive.Where(e => Math.Abs(e.GetCurrentHealth() - minHp) < GameBalanceConstants.HealthComparisonDelta).ToList();
                return candidates.Count == 1 ? candidates[0] : candidates[_rng.Next(candidates.Count)];
            }

            // Helper to get attack speed (default attackSpeedWithNoWeapon if no weapon)
            static double GetAttackSpeed(BaseBeing b)
            {
                var weapon = b.GetEquippedWeapon();
                return weapon?.GetAttackSpeed() ?? GameBalanceConstants.attackSpeedWithNoWeapon;
            }

            // ---------- First round: dexterity order ----------
            logger?.Invoke("First round: ordering by dexterity.");
            var participants = new List<BaseBeing> { _player };
            participants.AddRange(_enemies);
            // Sort descending by dexterity; stable tie-break random
            participants = participants
                .OrderByDescending(b => b.GetDexterity())
                .ThenBy(_ => Guid.NewGuid()) // randomize equal dex
                .ToList();

            // Turn's details
            var actionTurnDetails = new TurnDetails();
            
            foreach (var attacker in participants)
            {
                if (!IsAlive(attacker)) continue;

                // Determine target
                BaseBeing? target = attacker == _player ? PickPlayerTarget() : _player;
                if (target == null)
                {
                    logger?.Invoke("No enemies remain. Player wins.");
                    return true;
                }

                // Attack
                var damage = attacker.GetAmountAttack();
                logger?.Invoke($"{attacker.GetName()} attacks {target.GetName()} for {damage} damage.");
                var dead = target.TakeDamage(damage, new Dictionary<DamageModifiers, double>());

                //Damage event
                onDamage?.Invoke(this, new DamageEventArgs
                {
                    AttackerName = attacker.Name,
                    TargetName = target.Name,
                    DamageAmount = damage,
                    IsPlayerTarget = target == _player,
                    MaxHealth = target.MaxHealth,
                    RemainingHealth = target.CurrentHealth,
                    TargetDied = dead
                });

                if (dead)
                {
                    logger?.Invoke($"{target.GetName()} died.");
                    if (target == _player) return false;
                    // remove dead enemy from active list
                    _enemies.Remove(target);
                }
                actionTurnDetails.AddNewAction(turnNumber, attacker.GetName(), "Attack", damage, target.GetName(), dead ? "Dead":"Alive");
            }

            // Adding all action logs to _turnsDetails
            _turnsDetails.Add(actionTurnDetails);
            
            // If combat finished after first round
            if (!_enemies.Any(IsAlive))
            {
                logger?.Invoke("All enemies defeated after first round. Player wins.");
                return true;
            }
            if (!IsAlive(_player))
            {
                logger?.Invoke("Player died during first round.");
                return false;
            }

            // ---------- Subsequent rounds: schedule by weapon speed ----------
            logger?.Invoke("Entering scheduled attack phase (weapon attack speeds).");

            var pq = new PriorityQueue<BaseBeing, double>();
            // Initialize next attack time per being as its attack speed (they will attack after that delay)
            var participantsAlive = new List<BaseBeing> { _player };
            participantsAlive.AddRange(_enemies.Where(IsAlive));
            foreach (var p in participantsAlive)
            {
                double speed = GetAttackSpeed(p);
                // start scheduling at 'speed' (first scheduled attack after first round)
                pq.Enqueue(p, speed);
            }

            int actions = 0;

            while (IsAlive(_player) && _enemies.Any(IsAlive))
            {
                turnNumber++;
                if (actions++ > GameBalanceConstants.MaxCombatActions)
                {
                    logger?.Invoke("Max action limit reached, aborting combat.");
                    break;
                }

                if (pq.Count == 0)
                {
                    // Rebuild queue from alive participants
                    var alive = new List<BaseBeing> { _player };
                    alive.AddRange(_enemies.Where(IsAlive));
                    foreach (var p in alive) pq.Enqueue(p, GetAttackSpeed(p));
                }

                // Dequeue next attacker (earliest next attack time)
                // ISSUE/CONCERN:
                // Here there is a "problem", if any entity (A) has a very slow weapon, other entities that might have
                // lower attack speed could attack few times before that entity (A) gets its turn again.
                // Is this what I want?
                // I think this should be fixed to turn based strategy, where each entity gets its turn in a round-robin fashion
                // So for each combat turn, every entity should have 1 and only 1 chance to attack
                pq.TryDequeue(out var attacker, out var nextTime);

                if(attacker is null)
                {
                    logger?.Invoke("No attacker available, aborting combat.");
                    break;
                }

                // Skip dead attackers
                if (!IsAlive(attacker)) continue;

                // Determine target
                BaseBeing? target = attacker == _player ? PickPlayerTarget() : _player;
                if (target == null)
                {
                    logger?.Invoke("No enemies remain. Player wins.");
                    return true;
                }

                // Attack
                var damage = attacker.GetAmountAttack();
                logger?.Invoke($"[t={nextTime:0.00}] {attacker.GetName()} attacks {target.GetName()} for {damage} damage.");
                var dead = target.TakeDamage(damage, new Dictionary<DamageModifiers, double>());
                if (dead)
                {
                    logger?.Invoke($"{target.GetName()} died.");
                    if (target == _player) return false;
                    _enemies.Remove(target);
                }

                actionTurnDetails.AddNewAction(turnNumber, attacker.GetName(), "Attack", damage, target.GetName(), dead ? "Dead":"Alive");
                
                // Re-enqueue attacker with its next scheduled time
                double attackSpeed = GetAttackSpeed(attacker);
                double nextScheduled = nextTime + attackSpeed;
                pq.Enqueue(attacker, nextScheduled);
            }

            _turnsDetails.Add(actionTurnDetails);
            
            var playerAlive = IsAlive(_player);
            logger?.Invoke(playerAlive ? "Combat ended: player survived." : "Combat ended: player died.");
            return playerAlive;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool RunCombat_V2(Action<string>? logger = null)
        {
            throw new NotImplementedException();
        }

        #endregion
        
        #region Helpers

        private enum OrderMethod
        {
            Dexterity,
            Strength,
            Intelligence,
            Stamina,
            WeaponSpeed
        }
        #endregion
    }
}
