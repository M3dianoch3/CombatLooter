using CombatLooter.Classes.Implementation;
using CombatLooter.Classes.Interface;
using CombatLooter.Enum;
using CombatLooter.Services.NameGeneratorService.Interface;

namespace CombatLooter.Services.NameGeneratorService.Implementation
{
    /// <summary>
    /// Generates names for armor pieces based on their characteristics.
    /// </summary>
    public class ArmorNameGenerator : INameGenerator<IArmor>
    {
        /// <summary>
        /// Generates a name for the specified armor based on its type, slot, level, and resistances.
        /// </summary>
        /// <param name="item">The armor to generate a name for.</param>
        /// <returns>A string representing the generated name for the armor.</returns>
        public string GenerateName(IArmor item)
        {
            if (item is not BaseArmor baseArmor)
            {
                return "Unknown Armor";
            }

            var prefix = GeneratePrefix(baseArmor);
            var material = GenerateMaterial(baseArmor);
            var slotName = GenerateSlotName(baseArmor);
            var suffix = GenerateSuffix(baseArmor);

            return string.IsNullOrEmpty(suffix)
                ? $"{prefix} {material} {slotName}"
                : $"{prefix} {material} {slotName} {suffix}";
        }

        /// <summary>
        /// Generates a prefix based on the armor's item level (rarity indicator).
        /// </summary>
        private static string GeneratePrefix(BaseArmor armor)
        {
            var level = armor.GetILevel();

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
        /// Generates a material name based on the armor type.
        /// </summary>
        private static string GenerateMaterial(BaseArmor armor)
        {
            return armor.GetArmorTypes() switch
            {
                ArmorTypes.Light => "Leather",
                ArmorTypes.Medium => "Chain",
                ArmorTypes.Heavy => "Plate",
                _ => "Cloth"
            };
        }

        /// <summary>
        /// Generates the slot name for the armor piece.
        /// </summary>
        private static string GenerateSlotName(BaseArmor armor)
        {
            return armor.GetArmorSlot() switch
            {
                ArmorSlots.Head => "Helm",
                ArmorSlots.Chest => "Chestplate",
                ArmorSlots.Legs => "Leggings",
                ArmorSlots.Feet => "Boots",
                ArmorSlots.Hands => "Gauntlets",
                ArmorSlots.Shoulders => "Pauldrons",
                ArmorSlots.Waist => "Belt",
                _ => "Armor"
            };
        }

        /// <summary>
        /// Generates a suffix based on the armor's resistances.
        /// </summary>
        private static string GenerateSuffix(BaseArmor armor)
        {
            var resistances = armor.GetResistances();

            if (resistances == null || resistances.Count == 0)
            {
                return string.Empty;
            }

            // Get the highest resistance to create the suffix
            var highestResistance = resistances.MaxBy(r => r.Value);

            return highestResistance.Key switch
            {
                DamageModifiers.Fire => "of Fire Warding",
                DamageModifiers.Ice => "of Frost Warding",
                DamageModifiers.Lightning => "of Storm Warding",
                DamageModifiers.Poison => "of Toxin Warding",
                DamageModifiers.True => "of Divine Protection",
                DamageModifiers.Shadow => "of Shadow Warding",
                _ => string.Empty
            };
        }
    }
}
