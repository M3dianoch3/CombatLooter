using CombatLooter.Events.Implementation;

namespace CombatLooter.Services.Combat.Interface
{
    public interface ICombatService
    {
        /// <summary>
        /// Runs the combat simulation until either the player dies or all enemies are dead.
        /// - First round: order is by dexterity (highest first).
        /// - Subsequent actions: scheduled by weapon attack speed (fast weapons attack more often).
        /// Returns true if player survives, false if player dies.
        /// Optional logger receives plain-text events for debugging/observability.
        /// Event handler onDamage is invoked on each damage event.
        /// </summary>
        bool RunCombat(Action<string>? logger = null, EventHandler<DamageEventArgs> onDamage = null);
    }
}
