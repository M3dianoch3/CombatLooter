using CombatLooter.Classes.Interface;

namespace CombatLooter.Services.NameGeneratorService.Interface
{
    public interface INameGenerator<T> where T : IItem
    {
        /// <summary>
        /// Generates a name for the given item.
        /// </summary>
        /// <param name="item">A <see cref="IItem"/> instance representing the item to generate a name for.</param>
        /// <returns>A string representing the generated name for the item.</returns>
        string GenerateName(T item);
    }
}
