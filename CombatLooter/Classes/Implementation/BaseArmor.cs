using CombatLooter.Classes.Interface;
using CombatLooter.Enum;

namespace CombatLooter.Classes.Implementation
{
    public class BaseArmor : BaseItem, IArmor
    {
        private double _armorValue;
        private readonly ArmorSlots _armorSlot;
        private readonly ArmorTypes _armorType;

        public BaseArmor(double initialArmorValue, ArmorSlots armorSlot, ArmorTypes armorType, int ilevel, string name) : base(ilevel, name)
        {
            _armorValue = initialArmorValue;
            _armorSlot = armorSlot;
            _armorType = armorType;
        }

        /// <summary>
        /// Retrieves the armor type associated with the current instance.
        /// </summary>
        /// <returns>The <see cref="ArmorTypes"/> value representing the armor type.</returns>
        public ArmorTypes GetArmorTypes()
        {
            return _armorType;
        }

        /// <summary>
        /// Retrieves the armor slot associated with the current instance.
        /// </summary>
        /// <returns>The <see cref="ArmorSlots"/> value representing the armor slot.</returns>
        public ArmorSlots GetArmorSlot()
        {
            return _armorSlot;
        }

        /// <summary>
        /// Retrieves the armor value associated with the current instance.
        /// </summary>
        /// <returns>The <see cref="double"/> value representing the armor value.</returns>
        public double GetArmorValue()
        {
            return _armorValue;
        }

        /// <summary>
        /// Changes the armor value by the specified amount.
        /// </summary>
        /// <param name="amount">Amount of armor to change <see cref="double"/></param>
        public void ChangeArmorValue(double amount)
        {
            _armorValue += amount;
        }
    }
}
