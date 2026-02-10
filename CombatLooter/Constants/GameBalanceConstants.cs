
namespace CombatLooter.Constants
{
    /// <summary>
    /// Game balance related constants.
    /// </summary>
    public static class GameBalanceConstants
    {
        #region Enemy configuration

        public const int MaxEnemyTypes = 8;
        public const int EnemiesPerRound = 3;

        #endregion

        #region Item Tier Damage/Resistance Ranges

        public const int Tier1MinDamage = 1;
        public const int Tier1MaxDamage = 6;

        public const int Tier2MinDamage = 5;
        public const int Tier2MaxDamage = 11;

        public const int Tier3MinDamage = 8;
        public const int Tier3MaxDamage = 20;

        #endregion

        #region Item Tier Levels

        public const int Tier1MinLevel = 1;      // Levels 1-4
        public const int Tier1MaxLevel = 4;      

        public const int Tier2MinLevel = 5;      // Levels 5-9
        public const int Tier2MaxLevel = 9;

        public const int Tier3MinLevel = 10;     // Levels 10+

        #endregion

        #region Combat configuration

        public const double attackSpeedWithNoWeapon = 1.0;
        public const int MaxCombatActions = 10000;
        public const double HealthComparisonDelta = 0.0001;

        #endregion

        #region Player progression

        public const int mainAttributeIncrease = 5;
        public const int secondaryAttributeIncrease = 2;

        public const int healthIncreasePerLevel = 20;
        public const int manaIncreasePerLevel = 20;

        #endregion
    }
}
