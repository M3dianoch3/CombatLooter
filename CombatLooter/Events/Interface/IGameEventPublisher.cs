using CombatLooter.Events.Implementation;

namespace CombatLooter.Events.Interface
{
    /// <summary>
    /// Interface for subscribing to game events. 
    /// Any UI implementation can subscribe without knowing the concrete Game class.
    /// </summary>
    public interface IGameEventPublisher
    {
        /// <summary>
        /// Raised when any entity deals/takes damage. Check IsPlayerTarget to determine direction.
        /// </summary>
        event EventHandler<DamageEventArgs>? OnDamage;

        event EventHandler<CombatStartedEventArgs>? OnCombatStarted;
        event EventHandler<CombatEndedEventArgs>? OnCombatEnded;
        event EventHandler<LevelUpEventArgs>? OnLevelUp;
        event EventHandler<LootAvailableEventArgs>? OnLootAvailable;
        event EventHandler<LogMessageEventArgs>? OnLogMessage;
        event EventHandler? OnGameStarted;
        event EventHandler? OnGameOver;
    }
}
