using CombatLooter.Classes.Implementation;
using CombatLooter.Classes.Implementation.V0.Player;
using CombatLooter.Classes.Implementation.V0.Weapon;
using CombatLooter.Events.Implementation;
using CombatLooter.Events.Interface;
using CombatLooter.Services.Combat.Implementation;
using Microsoft.Extensions.Logging;

namespace CombatLooter.Services.Game.Implementation
{
    /// <summary>
    /// Game class controls the overall game flow. Combats, player progression, and item management will be handled here.
    /// </summary>
    public class GameService : IGameEventPublisher
    {
        #region Initial Values for player
        private const double InitialHealth = 100.0;
        private const double InitialMana = 50.0;
        private const string InitialName = "Hero";
        private const double InitialArmor = 5.0;
        private const int InitialStat = 10;

        private readonly MeleeWeapon InitialWeapon = new(Enum.WeaponTypes.Melee, 10, Enum.DamageTypes.Physical, 2, 2, [], 1, "Wooden Sword", Enum.MeleeWeaponTypes.Sword);
        #endregion

        #region Events (Observer pattern)
        public event EventHandler<DamageEventArgs>? OnDamage;
        public event EventHandler<CombatStartedEventArgs>? OnCombatStarted;
        public event EventHandler<CombatEndedEventArgs>? OnCombatEnded;
        public event EventHandler<LevelUpEventArgs>? OnLevelUp;
        public event EventHandler<LootAvailableEventArgs>? OnLootAvailable;
        public event EventHandler<LogMessageEventArgs>? OnLogMessage;
        public event EventHandler? OnGameStarted;
        public event EventHandler? OnGameOver;
        #endregion

        private readonly ILogger<GameService> _logger;
        private Player player;

        public GameService(ILogger<GameService> logger)
        {
            _logger = logger;
            player = new Player(InitialHealth, InitialMana, InitialName, InitialArmor, InitialStat, InitialStat, InitialStat, InitialStat, new Dictionary<Enum.DamageModifiers, double>(), InitialWeapon, 1, Enum.BeingClass.Humanoid);
        }

        public void StartNewRun()
        {
            RaiseLogMessage("Starting a new run...");
            OnGameStarted?.Invoke(this, EventArgs.Empty);
            
            // Loop through combats until the player dies
            bool playerAlive = true;
            int combatNumber = 0;

            while (playerAlive)
            {
                combatNumber++;

                // Create enemies based on Player level
                List<BaseBeing> enemiesCombat = Helper.Helper.GetEnemiesForLevel(player.Level);

                // Raise combat started event
                OnCombatStarted?.Invoke(this, new CombatStartedEventArgs
                {
                    CombatNumber = combatNumber,
                    EnemyCount = enemiesCombat.Count,
                    EnemyNames = enemiesCombat.Select(e => e.GetName()).ToList()
                });

                RaiseLogMessage($"Starting combat #{combatNumber} against {enemiesCombat.Count} enemies...");

                // Invoke the combat service to run a combat
                var combat = new CombatService(player, enemiesCombat, _logger);
                playerAlive = combat.RunCombat(
                    logger: msg => RaiseLogMessage(msg),
                    onDamage: (sender, args) => OnDamage?.Invoke(this, args)
                    );

                // Raise combat ended event
                OnCombatEnded?.Invoke(this, new CombatEndedEventArgs
                {
                    PlayerVictory = playerAlive
                });

                if (!playerAlive) break;

                // Track stats before level up
                int oldStr = player.Strength;
                int oldDex = player.Dexterity;
                int oldInt = player.Intelligence;

                // Increase player's level in 1 (for now), 1 combat won = 1 level
                player.IncreaseLevel_basedOnWeapon();

                // Raise level up event
                OnLevelUp?.Invoke(this, new LevelUpEventArgs
                {
                    NewLevel = player.Level,
                    StrengthGained = player.Strength - oldStr,
                    DexterityGained = player.Dexterity - oldDex,
                    IntelligenceGained = player.Intelligence - oldInt
                });

                // After combat, if the player is still alive, create loot (based on enemies?) and let the player choose equipment


            }
            _logger.LogInformation("Game over. The player has died.");
        }

        #region Event Raising Helpers
        private void RaiseLogMessage(string message, LogMessageEventArgs.LogLevel level = LogMessageEventArgs.LogLevel.Info)
        {
            _logger.LogInformation(message);
            OnLogMessage?.Invoke(this, new LogMessageEventArgs { Message = message, Level = level });
        }
        #endregion
    }
}
