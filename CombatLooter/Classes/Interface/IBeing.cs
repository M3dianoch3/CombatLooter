using CombatLooter.Classes.Implementation;
using CombatLooter.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatLooter.Classes.Interface
{
    /// <summary>
    /// Interfaces for any being in the game.
    /// </summary>
    public interface IBeing
    {
        #region Update methods
        /// <summary>
        /// Method to change current health value
        /// </summary>
        /// <param name="amount">Amount to change health by</param>
        /// <returns>A <see cref="double"/> that represents the actual amount healed</returns>
        double ChangeCurrentHealth(double amount);

        /// <summary>
        /// Method to change current mana value
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>A <see cref="double"/> that represents the actual amount restored</returns>
        double ChangeCurrentMana(double amount);

        /// <summary>
        /// Method to change max health value
        /// </summary>
        /// <param name="amount">Amount to change health by</param>
        void ChangeMaxHealth(double amount);

        /// <summary>
        /// Method to change max mana value
        /// </summary>
        /// <param name="amount"></param>
        void ChangeMaxMana(double amount);

        /// <summary>
        /// Method to change the name of the being
        /// </summary>
        /// <param name="newName"></param>
        void ChangeName(string newName);

        /// <summary>
        /// Method to change Armor value
        /// </summary>
        /// <param name="amount">Amount of armor to change</param>
        void ChangeArmor(double amount);

        /// <summary>
        /// Method to change Stamina value
        /// </summary>
        /// <param name="amount"></param>
        void ChangeStamina(int amount);

        /// <summary>
        /// Method to change Strength value
        /// </summary>
        /// <param name="amount"></param>
        void ChangeStrength(int amount);

        /// <summary>
        /// Method to change Intelligence value
        /// </summary>
        /// <param name="amount"></param>
        void ChangeIntelligence(int amount);

        /// <summary>
        /// Method to change Dexterity value
        /// </summary>
        /// <param name="amount"></param>
        void ChangeDexterity(int amount);
        #endregion

        #region Setters and Getters
        /// <summary>
        /// Gets the current health of the being
        /// </summary>
        /// <returns>The health value as a double</returns>
        double GetCurrentHealth();

        /// <summary>
        /// Sets the health of the being to a specific value
        /// </summary>
        /// <param name="health">The new health value to set</param>
        void SetCurrentHealth(double health);

        /// <summary>
        /// Gets the current mana of the being
        /// </summary>
        /// <returns>The mana value as a double</returns>
        double GetCurrentMana();

        /// <summary>
        /// Sets the mana of the being to a specific value
        /// </summary>
        /// <param name="mana">The new mana value to set</param>
        void SetCurrentMana(double mana);

        /// <summary>
        /// Gets the maximum health of the being
        /// </summary>
        double GetMaxHealth();

        /// <summary>
        /// Sets the maximum health of the being
        /// </summary>
        /// <param name="maxHealth">Amount to set</param>
        void SetMaxHealth(double maxHealth);

        /// <summary>
        /// Gets the maximum mana of the being
        /// </summary>
        double GetMaxMana();

        /// <summary>
        /// Sets the maximum mana of the being
        /// </summary>
        /// <param name="maxMana">Amount to set</param>
        void SetMaxMana(double maxMana);

        /// <summary>
        /// Gets the current name of the being
        /// </summary>
        /// <returns>The name as a string</returns>
        string GetName();

        /// <summary>
        /// Sets the name of the being to a specific value
        /// </summary>
        /// <param name="name">The new name to set</param>
        void SetName(string name);

        /// <summary>
        /// Gets the current armor value of the being
        /// </summary>
        /// <returns>The armor value as a double</returns>
        double GetArmor();

        /// <summary>
        /// Sets the armor of the being to a specific value
        /// </summary>
        /// <param name="armor">The new armor value to set</param>
        void SetArmor(double armor);

        /// <summary>
        /// Gets the current stamina value of the being
        /// </summary>
        /// <returns>The stamina value as an int</returns>
        int GetStamina();

        /// <summary>
        /// Sets the stamina of the being to a specific value
        /// </summary>
        /// <param name="stamina">The new stamina value to set</param>
        void SetStamina(int stamina);

        /// <summary>
        /// Gets the current strength value of the being
        /// </summary>
        /// <returns>The strength value as an int</returns>
        int GetStrength();

        /// <summary>
        /// Sets the strength of the being to a specific value
        /// </summary>
        /// <param name="strength">The new strength value to set</param>
        void SetStrength(int strength);

        /// <summary>
        /// Gets the current intelligence value of the being
        /// </summary>
        /// <returns>The intelligence value as an int</returns>
        int GetIntelligence();

        /// <summary>
        /// Sets the intelligence of the being to a specific value
        /// </summary>
        /// <param name="intelligence">The new intelligence value to set</param>
        void SetIntelligence(int intelligence);

        /// <summary>
        /// Gets the current dexterity value of the being
        /// </summary>
        /// <returns>The dexterity value as an int</returns>
        int GetDexterity();

        /// <summary>
        /// Sets the dexterity of the being to a specific value
        /// </summary>
        /// <param name="dexterity">The new dexterity value to set</param>
        void SetDexterity(int dexterity);

        /// <summary>
        /// Gets the resistances dictionary of the being
        /// </summary>
        /// <returns>The resistances dictionary mapping DamageModifiers to resistance values</returns>
        Dictionary<DamageModifiers, double> GetResistances();

        /// <summary>
        /// Sets the resistances dictionary of the being
        /// </summary>
        /// <param name="resistances">The new resistances dictionary to set</param>
        void SetResistances(Dictionary<DamageModifiers, double> resistances);

        /// <summary>
        /// Gets the current level of the being
        /// </summary>
        /// <returns>The level as an <see cref="int"/></returns>
        int GetLevel();

        /// <summary>
        /// Retrieves the currently equipped weapon.
        /// </summary>
        /// <returns>The currently equipped weapon as a <see cref="BaseWeapon"/> instance, or <see langword="null"/> if no weapon
        /// is equipped.</returns>
        BaseWeapon? GetEquippedWeapon();

        /// <summary>
        /// Sets the class of the being.
        /// </summary>
        /// <param name="beingClass"><see cref="BeingClass"/></param>
        void SetBeingClass(BeingClass beingClass);

        /// <summary>
        /// Gets the class of the being.
        /// </summary>
        /// <returns><see cref="BeingClass"/></returns>
        BeingClass GetBeingClass();
        #endregion
    }
}
