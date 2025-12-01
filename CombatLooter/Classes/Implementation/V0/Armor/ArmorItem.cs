using CombatLooter.Enum;

namespace CombatLooter.Classes.Implementation.V0.Armor
{
    public class ArmorItem : BaseArmor
    {
        /// <summary>
        /// Constructor for Armor class
        /// </summary>
        /// <param name="initialArmorValue">Armor value</param>
        /// <param name="armorSlot">The slot that the armor occupies</param>
        /// <param name="armorType">The type of armor</param>
        /// <param name="resistances">The resistances against damage modifiers</param>
        /// <param name="ilevel">The level of the armor</param>
        /// <param name="name">The name of the armor</param>
        public ArmorItem(double initialArmorValue, ArmorSlots armorSlot, ArmorTypes armorType, Dictionary<DamageModifiers, double> resistances, double weight, int ilevel, string name) 
            : base(initialArmorValue, armorSlot, armorType, resistances, weight, ilevel, name)
        {
        }
    }
}
