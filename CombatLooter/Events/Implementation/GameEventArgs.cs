namespace CombatLooter.Events.Implementation
{
    /// <summary>
    /// Base class for all game events
    /// </summary>
    public abstract class GameEventArgs : EventArgs
    {
        public DateTime Timestamp { get; } = DateTime.UtcNow;
    }

    /// <summary>
    /// Event args for any damage event (dealt or taken). 
    /// Use this for both player attacking enemies and enemies attacking player.
    /// </summary>
    public class DamageEventArgs : GameEventArgs
    {
        public string AttackerName { get; init; } = string.Empty;
        public string TargetName { get; init; } = string.Empty;
        public double DamageAmount { get; init; }
        public double RemainingHealth { get; init; }
        public double MaxHealth { get; init; }
        public bool TargetDied { get; init; }
        public bool IsPlayerTarget { get; init; }

        /// <summary>
        /// Health percentage (0.0 to 1.0) useful for UI health bars
        /// </summary>
        public double HealthPercentage => MaxHealth > 0 ? RemainingHealth / MaxHealth : 0;
    }

    public class CombatStartedEventArgs : GameEventArgs
    {
        public int CombatNumber { get; init; }
        public int EnemyCount { get; init; }
        public IReadOnlyList<string> EnemyNames { get; init; } = [];
    }

    public class CombatEndedEventArgs : GameEventArgs
    {
        public bool PlayerVictory { get; init; }
        public int TurnsTaken { get; init; }
    }

    public class LevelUpEventArgs : GameEventArgs
    {
        public int NewLevel { get; init; }
        public int StrengthGained { get; init; }
        public int DexterityGained { get; init; }
        public int IntelligenceGained { get; init; }
    }

    public class LootAvailableEventArgs : GameEventArgs
    {
        public IReadOnlyList<object> AvailableItems { get; init; } = [];
    }

    public class LogMessageEventArgs : GameEventArgs
    {
        public string Message { get; init; } = string.Empty;
        public LogLevel Level { get; init; } = LogLevel.Info;

        public enum LogLevel { Debug, Info, Warning, Error }
    }
}
