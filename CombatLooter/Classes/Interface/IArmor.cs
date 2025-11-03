using CombatLooter.Enum;

namespace CombatLooter.Classes.Interface
{
    public interface IArmor
    {
        /// <summary>
        /// Retrieves the armor type associated with the current instance.
        /// </summary>
        /// <returns>The <see cref="ArmorTypes"/> value representing the armor type.</returns>
        ArmorTypes GetArmorTypes();

        /// <summary>
        /// Retrieves the armor slot associated with the current instance.
        /// </summary>
        /// <returns>The <see cref="ArmorSlots"/> value representing the armor slot.</returns>
        ArmorSlots GetArmorSlot();

        /// <summary>
        /// Retrieves the armor value associated with the current instance.
        /// </summary>
        /// <returns>The <see cref="double"/> value representing the armor value.</returns>
        double GetArmorValue();

        /// <summary>
        /// Changes the armor value by the specified amount.
        /// </summary>
        /// <param name="amount">Amount of armor to change <see cref="double"/></param>
        void ChangeArmorValue(double amount);
    }
}
