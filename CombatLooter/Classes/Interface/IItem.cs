namespace CombatLooter.Classes.Interface
{
    /// <summary>
    /// Interface of an item. This is the lowest level interface for all items.
    /// </summary>
    public interface IItem
    {
        /// <summary>
        /// Method to get the unique identifier of the item.
        /// </summary>
        /// <returns>Item Id <see cref="Guid"/></returns>
        Guid GetId();

        /// <summary>
        /// Method to get the item level.
        /// </summary>
        /// <returns>Item Level <see cref="int"/></returns>
        int GetILevel();

        /// <summary>
        /// Method to set the item level.
        /// </summary>
        /// <param name="newLevel">The new item level. <see cref="int"/></param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the new item level is less than 1.</exception>
        void SetILevel(int newLevel);

        /// <summary>
        /// Method to get the item name.
        /// </summary>
        /// <returns>Item Name <see cref="string"/></returns>
        string GetName();

        /// <summary>
        /// Method to set the item name.
        /// </summary>
        /// <param name="newName">The new name for the item. <see cref="string"/></param>
        void SetName(string newName);
    }
}
