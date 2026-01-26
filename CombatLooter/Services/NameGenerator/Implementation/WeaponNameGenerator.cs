using CombatLooter.Classes.Implementation;
using CombatLooter.Classes.Implementation.V0.Weapon;
using CombatLooter.Classes.Interface;
using CombatLooter.Enum;
using CombatLooter.Services.NameGeneratorService.Interface;

namespace CombatLooter.Services.NameGeneratorService.Implementation
{
    /// <summary>
    /// Generates names for weapons based on their characteristics.
    /// </summary>
    public class WeaponNameGenerator : INameGenerator<IWeapon>
    {
        /// <summary>
        /// Generates a name for the specified weapon based on its type, level, and modifiers.
        /// </summary>
        /// <param name="item">The weapon to generate a name for.</param>
        /// <returns>A string representing the generated name for the weapon.</returns>
        public string GenerateName(IWeapon item)
        {
            if (item is not BaseWeapon baseWeapon)
            {
                return "Unknown Weapon";
            }

            var prefix = GeneratePrefix(baseWeapon);
            var typeName = GenerateTypeName(baseWeapon);
            var suffix = GenerateSuffix(baseWeapon);

            return string.IsNullOrEmpty(suffix)
                ? $"{prefix} {typeName}"
                : $"{prefix} {typeName} {suffix}";
        }

        /// <summary>
        /// Generates a prefix based on the weapon's item level (rarity indicator).
        /// </summary>
        private static string GeneratePrefix(BaseWeapon weapon)
        {
            var level = weapon.GetILevel();

            return level switch
            {
                >= 50 => "Legendary",
                >= 30 => "Epic",
                >= 15 => "Rare",
                >= 5 => "Uncommon",
                _ => "Common"
            };
        }

        /// <summary>
        /// Generates the type name based on the specific weapon type.
        /// </summary>
        private static string GenerateTypeName(BaseWeapon weapon)
        {
            return weapon switch
            {
                MeleeWeapon meleeWeapon => meleeWeapon.MeleeWeaponType.ToString(),
                RangedWeapon rangedWeapon => rangedWeapon.RangedWeaponType.ToString(),
                _ => "Weapon"
            };
        }

        /// <summary>
        /// Generates a suffix based on the weapon's damage modifiers.
        /// </summary>
        private static string GenerateSuffix(BaseWeapon weapon)
        {
            var modifiers = weapon.GetDamageModifiers();

            if (modifiers.Count == 0)
            {
                return string.Empty;
            }

            // Get the highest damage modifier to create the suffix
            var highestModifier = modifiers.MaxBy(m => m.Value);

            return highestModifier.Key switch
            {
                DamageModifiers.Fire => "of Flames",
                DamageModifiers.Ice => "of Frost",
                DamageModifiers.Lightning => "of Thunder",
                DamageModifiers.Poison => "of Venom",
                DamageModifiers.True => "of Radiance",
                DamageModifiers.Shadow => "of Shadows",
                _ => string.Empty
            };
        }
    }
}
