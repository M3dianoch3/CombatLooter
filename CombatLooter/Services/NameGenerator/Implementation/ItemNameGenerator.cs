using CombatLooter.Classes.Implementation;
using CombatLooter.Classes.Interface;
using CombatLooter.Services.NameGeneratorService.Interface;

namespace CombatLooter.Services.NameGeneratorService.Implementation
{
    /// <summary>
    /// Generates names for generic items based on their characteristics.
    /// This is a fallback generator for items that don't have a specialized generator.
    /// </summary>
    public class ItemNameGenerator : INameGenerator<IItem>
    {
        /// <summary>
        /// Generates a name for the specified item based on its level.
        /// </summary>
        /// <param name="item">The item to generate a name for.</param>
        /// <returns>A string representing the generated name for the item.</returns>
        public string GenerateName(IItem item)
        {
            var prefix = GeneratePrefix(item);
            var existingName = item.GetName();

            // If the item already has a meaningful name, use it with the prefix
            if (!string.IsNullOrWhiteSpace(existingName) && existingName != "Unnamed Item")
            {
                return $"{prefix} {existingName}";
            }

            return $"{prefix} Item";
        }

        /// <summary>
        /// Generates a prefix based on the item's level (rarity indicator).
        /// </summary>
        private static string GeneratePrefix(IItem item)
        {
            var level = item.GetILevel();

            return level switch
            {
                >= 50 => "Legendary",
                >= 30 => "Epic",
                >= 15 => "Rare",
                >= 5 => "Uncommon",
                _ => "Common"
            };
        }
    }
}
