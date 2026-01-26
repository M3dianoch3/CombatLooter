using CombatLooter.Classes.Interface;
using CombatLooter.Services.NameGeneratorService.Implementation;
using CombatLooter.Services.NameGeneratorService.Interface;

namespace CombatLooter.Services.NameGeneratorService.Implementation
{
    /// <summary>
    /// Composite name generator service that delegates to specialized generators.
    /// This class provides backward compatibility with the original interface while
    /// using the new generic INameGenerator pattern internally.
    /// </summary>
    public class NameGeneratorService : INameGeneratorService
    {
        private readonly INameGenerator<IWeapon> _weaponNameGenerator;
        private readonly INameGenerator<IArmor> _armorNameGenerator;
        private readonly INameGenerator<IItem> _itemNameGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="NameGeneratorService"/> class
        /// with default generators.
        /// </summary>
        public NameGeneratorService()
        {
            _weaponNameGenerator = new WeaponNameGenerator();
            _armorNameGenerator = new ArmorNameGenerator();
            _itemNameGenerator = new ItemNameGenerator();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NameGeneratorService"/> class
        /// with custom generators (useful for dependency injection and testing).
        /// </summary>
        /// <param name="weaponNameGenerator">The weapon name generator to use.</param>
        /// <param name="armorNameGenerator">The armor name generator to use.</param>
        /// <param name="itemNameGenerator">The item name generator to use.</param>
        public NameGeneratorService(
            INameGenerator<IWeapon> weaponNameGenerator,
            INameGenerator<IArmor> armorNameGenerator,
            INameGenerator<IItem> itemNameGenerator)
        {
            _weaponNameGenerator = weaponNameGenerator ?? throw new ArgumentNullException(nameof(weaponNameGenerator));
            _armorNameGenerator = armorNameGenerator ?? throw new ArgumentNullException(nameof(armorNameGenerator));
            _itemNameGenerator = itemNameGenerator ?? throw new ArgumentNullException(nameof(itemNameGenerator));
        }

        /// <summary>
        /// Generate a new name for the given item based on its characteristics.
        /// Routes to the appropriate specialized generator based on item type.
        /// </summary>
        /// <param name="item">A <see cref="IItem"/> instance representing the item to generate a name for.</param>
        /// <returns>A string representing the generated name for the item.</returns>
        public string GenerateName(IItem item)
        {
            // Route to specialized generators based on item type
            return item switch
            {
                IWeapon weapon => _weaponNameGenerator.GenerateName(weapon),
                IArmor armor => _armorNameGenerator.GenerateName(armor),
                _ => _itemNameGenerator.GenerateName(item)
            };
        }

        /// <summary>
        /// Generate a new name for the given weapon based on its characteristics.
        /// </summary>
        /// <param name="weapon">A <see cref="IWeapon"/> instance representing the weapon to generate a name for.</param>
        /// <returns>A string representing the generated name for the weapon.</returns>
        public string GenerateName(IWeapon weapon)
        {
            return _weaponNameGenerator.GenerateName(weapon);
        }

        /// <summary>
        /// Generate a new name for the given armor based on its characteristics.
        /// </summary>
        /// <param name="armor">A <see cref="IArmor"/> instance representing the armor to generate a name for.</param>
        /// <returns>A string representing the generated name for the armor.</returns>
        public string GenerateName(IArmor armor)
        {
            return _armorNameGenerator.GenerateName(armor);
        }
    }
}
