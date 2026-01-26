using CombatLooter.Classes.Interface;

namespace CombatLooter.Classes.Implementation
{
    public class BaseItem : IItem
    {
        private readonly Guid _itemId;
        private int _iLevel;
        private string _name;
        // private Rarity _rarity; // Future implementation for item rarity
        // private int durability; // Future implementation for item durability

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseItem"/> class with a unique identifier and a default item
        /// level of 1. 
        /// </summary>
        /// <remarks>The constructor generates a new globally unique identifier (GUID) for the item,
        /// sets its initial level to 1 and sets its name to "Unnamed Item".</remarks>
        public BaseItem()
        {
            this._itemId = Guid.NewGuid();
            this._iLevel = 1;
            this._name = "Unnamed Item";
        }

        /// <summary>
        /// Method to initialize a new instance of the <see cref="BaseItem"/> class with a
        /// specified item level. ID is generated automatically.
        /// </summary>
        /// <param name="itemLevel">The item level. <see cref="int"/></param>
        /// <param name="name">The item name. <see cref="string"/></param>
        public BaseItem(int itemLevel, string name)
        {
            this._itemId = Guid.NewGuid();
            this._iLevel = itemLevel;
            this._name = name;
        }

        /// <summary>
        /// Method to get the unique identifier of the item.
        /// </summary>
        /// <returns>Item Id <see cref="Guid"/></returns>
        public Guid GetId()
        {
            return this._itemId;
        }

        /// <summary>
        /// Method to get the item level.
        /// </summary>
        /// <returns>Item Level <see cref="int"/></returns>
        public int GetILevel()
        {
            return this._iLevel;
        }

        /// <summary>
        /// Method to set the item level.
        /// </summary>
        /// <param name="newLevel">The new item level. <see cref="int"/></param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the new item level is less than 1.</exception>
        public void SetILevel(int newLevel)
        {
            if (newLevel < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(newLevel), "Item level must be at least 1.");
            }
            this._iLevel = newLevel;
        }

        /// <summary>
        /// Method to get the item name.
        /// </summary>
        /// <returns>Item Name <see cref="string"/></returns>
        public string GetName()
        {
            return this._name;
        }

        /// <summary>
        /// Sets the name of the object to the specified value. 
        /// </summary>
        /// <param name="newName">The new name to assign to the object. Cannot be null or empty.</param>
        public void SetName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(newName));
            }

            this._name = newName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Item ID: {this._itemId}, Name: {this._name}, Item Level: {this._iLevel}";
        }
    }
}
