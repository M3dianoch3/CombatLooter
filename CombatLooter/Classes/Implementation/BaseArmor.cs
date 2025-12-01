using CombatLooter.Classes.Interface;
using CombatLooter.Enum;

namespace CombatLooter.Classes.Implementation
{
    public abstract class BaseArmor : BaseItem, IArmor
    {
        private double _armorValue;
        private readonly ArmorSlots _armorSlot;
        private readonly ArmorTypes _armorType;
        private readonly Dictionary<DamageModifiers, double> _armorResistances;
        private double _weight;

        public BaseArmor(double initialArmorValue, ArmorSlots armorSlot, ArmorTypes armorType, Dictionary<DamageModifiers, double> resistances, double weight, int ilevel, string name) : base(ilevel, name)
        {
            _armorValue = initialArmorValue;
            _armorSlot = armorSlot;
            _armorType = armorType;
            _armorResistances = resistances;
            _weight = weight;
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
        /// Gets the armor resistances
        /// </summary>
        /// <returns>The <see cref="Dictionary{DamageModifiers, int}"/> value representing a set of resistances and its value.</returns>
        public Dictionary<DamageModifiers, double> GetResistances()
        {
            return _armorResistances;
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
        /// Gets the weight of the armor piece
        /// </summary>
        /// <returns>A <see cref="double"/> representing the weight of the armor piece.</returns>
        public double GetWeight()
        {
            return _weight;
        }

        /// <summary>
        /// Changes the armor value by the specified amount.
        /// </summary>
        /// <param name="amount">Amount of armor to change <see cref="double"/></param>
        public void ChangeArmorValue(double amount)
        {
            _armorValue += amount;
        }

        /// <summary>
        /// Override ToString method to provide a string representation of the ArmorItem
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var stringDescription = $"| Armor name: {this.GetName()} | Armor Value: {this.GetArmorValue()}\n" +
                                    $"| Armor Type: {this.GetArmorTypes()} | Armor Slot: {this.GetArmorSlot()}\n";
            if (this.GetResistances() != null && this.GetResistances().Count > 0)
            {
                stringDescription += "| Resistances:\n";
                foreach (var resistance in this.GetResistances())
                {
                    stringDescription += $"| -> {resistance.Key}: {resistance.Value} \n";
                }
                // Remove last comma and space
                stringDescription = stringDescription.TrimEnd(',', ' ');
            }
            return stringDescription;
        }
    }
}
