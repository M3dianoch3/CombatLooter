namespace CombatLooter.Enum
{
    /// <summary>
    /// Types of weapons available in the game.
    /// </summary>
    public enum WeaponTypes
    {
        Melee, // Use strength
        Ranged // Use dexterity or intelligence
    }

    /// <summary>
    /// Types of melee weapons.
    /// </summary>
    public enum MeleeWeaponTypes
    {
        Sword, // Fast, balanced damage. Speed [1.1-1.5], Damage [13-18] // DPS: [8.66-16.36]
        Axe, // Slow, high damage. Speed [1.8-2.5], Damage [27-35] // DPS: [10.8-19.44]
        Hammer, // Very slow, very high damage. Speed [2.1-3.0], Damage [32-40] // DPS: [10.66-19.05]
        Dagger, // Very fast, low damage. Speed [0.8-1], Damage [10-15] // DPS: [10-18.75]
        Spear, // Balanced speed and damage. Speed [1.3-1.8], Damage [15-20] // DPS: [8.33-15.38]
        Staff // Balanced speed and magic damage. Speed [1.3-1.8], Damage [15-20] // DPS: [8.33-15.38]
    }

    /// <summary>
    /// Types of ranged weapons.
    /// </summary>
    public enum RangedWeaponTypes
    {
        Bow, // Balanced, balanced physical damage. Speed [1.0-1.5], Damage [12-18] // DPS: [8-18]
        Crossbow, // Slow, high physical damage. Speed [1.5-2.0], Damage [18-25] // DPS: [9-16.66]
        Wand // Fast, low magical damage. Speed [0.8-1.2], Damage [10-15] // DPS: [8.33-18.75]
    }

    /// <summary>
    /// Weapon configuration data including damage and speed ranges for each weapon type.
    /// </summary>
    public static class WeaponRanges
    {
        // Melee weapon ranges
        public static readonly Dictionary<MeleeWeaponTypes, WeaponStats> MeleeWeaponStats = new()
        {
            // Weapon Type       MinDamage   MaxDamage   MinSpeed    MaxSpeed 
            { MeleeWeaponTypes.Sword, new WeaponStats(13, 18, 1.1, 1.5, 1, 1.5) },
            { MeleeWeaponTypes.Axe, new WeaponStats(27, 35, 1.8, 2.5, 2, 3.5) },
            { MeleeWeaponTypes.Hammer, new WeaponStats(32, 40, 2.1, 3.0, 3, 5) },
            { MeleeWeaponTypes.Dagger, new WeaponStats(10, 15, 0.8, 1.0, 0.5, 1) },
            { MeleeWeaponTypes.Spear, new WeaponStats(15, 20, 1.3, 1.8, 1.5, 2) },
            { MeleeWeaponTypes.Staff, new WeaponStats(15, 20, 1.3, 1.8, 1.5, 2.5) }
        };

        // Ranged weapon ranges
        public static readonly Dictionary<RangedWeaponTypes, WeaponStats> RangedWeaponStats = new()
        {
            // Weapon Type       MinDamage   MaxDamage   MinSpeed    MaxSpeed
            { RangedWeaponTypes.Bow, new WeaponStats(12, 18, 1.0, 1.5, 2, 3) },
            { RangedWeaponTypes.Crossbow, new WeaponStats(18, 25, 1.5, 2.0, 4, 6) },
            { RangedWeaponTypes.Wand, new WeaponStats(10, 15, 0.8, 1.2, 0.5, 2) }
        };

        /// <summary>
        /// Gets random damage value for a weapon type.
        /// </summary>
        public static double GetRandomDamage(MeleeWeaponTypes weaponType)
        {
            var stats = MeleeWeaponStats[weaponType];
            return Random.Shared.Next(stats.MinDamage, stats.MaxDamage + 1);
        }

        /// <summary>
        /// Gets random speed value for a weapon type.
        /// </summary>
        public static double GetRandomSpeed(MeleeWeaponTypes weaponType)
        {
            var stats = MeleeWeaponStats[weaponType];
            return stats.MinSpeed + (Random.Shared.NextDouble() * (stats.MaxSpeed - stats.MinSpeed));
        }

        /// <summary>
        /// Gets random damage value for a ranged weapon type.
        /// </summary>
        public static double GetRandomDamage(RangedWeaponTypes weaponType)
        {
            var stats = RangedWeaponStats[weaponType];
            return Random.Shared.Next(stats.MinDamage, stats.MaxDamage + 1);
        }

        /// <summary>
        /// Gets random speed value for a ranged weapon type.
        /// </summary>
        public static double GetRandomSpeed(RangedWeaponTypes weaponType)
        {
            var stats = RangedWeaponStats[weaponType];
            return stats.MinSpeed + (Random.Shared.NextDouble() * (stats.MaxSpeed - stats.MinSpeed));
        }

        /// <summary>
        /// Gets random weight value for a melee weapon type.
        /// </summary>
        /// <param name="weaponType">Weapon type.</param>
        /// <returns></returns>
        public static double GetRandomWeight(MeleeWeaponTypes weaponType)
        {
            var stats = MeleeWeaponStats[weaponType];
            return stats.GetRandomWeigth();
        }

        /// <summary>
        /// Gets random weight value for a ranged weapon type.
        /// </summary>
        /// <param name="weaponType">Weapon type.</param>
        /// <returns>double</returns>
        public static double GetRandomWeight(RangedWeaponTypes weaponType)
        {
            var stats = RangedWeaponStats[weaponType];
            return stats.GetRandomWeigth();
        }
    }

    /// <summary>
    /// Represents stat ranges for a weapon type.
    /// </summary>
    public record WeaponStats(int MinDamage, int MaxDamage, double MinSpeed, double MaxSpeed, double MinWeigth, double MaxWeigth)
    {
        /// <summary>
        /// Gets a random damage value within the range.
        /// </summary>
        public double GetRandomDamage() => Random.Shared.Next(MinDamage, MaxDamage + 1);

        /// <summary>
        /// Gets a random speed value within the range.
        /// </summary>
        public double GetRandomSpeed() => MinSpeed + (Random.Shared.NextDouble() * (MaxSpeed - MinSpeed));

        /// <summary>
        /// Gets a random weight value within the range.
        /// </summary>
        /// <returns></returns>
        public double GetRandomWeigth() => MinWeigth + (Random.Shared.NextDouble() * (MaxWeigth - MinWeigth));
    }
}
