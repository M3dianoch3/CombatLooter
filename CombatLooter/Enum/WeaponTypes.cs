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
}
