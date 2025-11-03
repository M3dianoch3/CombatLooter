using CombatLooter.Classes.Interface;
using CombatLooter.Enum;

namespace CombatLooter.Classes.Implementation
{
    public abstract class BaseBeing : IBeing
    {
        private double _currentHealth;
        private double _currentMana;
        private double _maxHealth;
        private double _maxMana;
        private string _name;
        private double _armor;
        private int _stamina;
        private int _strength;
        private int _intelligence;
        private int _dexterity;
        private Dictionary<DamageModifiers, double> _resistances;
        private BaseWeapon? _equippedWeapon;
        private int _level;
        private BeingClass _class;

        /// <summary>
        /// Default constructor for BaseBeing
        /// </summary>
        protected BaseBeing()
        {
            this._currentHealth = 100f;
            this._maxHealth = 100f;
            this._currentMana = 100f;
            this._maxMana = 100f;
            this._name = "Unnamed Being";
            this._armor = 0f;
            this._stamina = 10;
            this._strength = 10;
            this._intelligence = 10;
            this._dexterity = 10;
            this._resistances = new Dictionary<DamageModifiers, double>();
            this._equippedWeapon = null;
            this._level = 1;
            this._class = BeingClass.Humanoid;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBeing"/> class with specified attributes.
        /// </summary>
        /// <param name="health">The health of the being. <see cref="double"/></param>
        /// <param name="mana">The mana of the being. <see cref="double"/></param>
        /// <param name="name">The name of the being. <see cref="string"/></param>
        /// <param name="armor">The armor value of the being. <see cref="double"/></param>
        /// <param name="stamina">The stamina value of the being. <see cref="int"/></param>
        /// <param name="strength">The strength value of the being. <see cref="int"/></param>
        /// <param name="intelligence">The intelligence value of the being. <see cref="int"/></param>
        /// <param name="dexterity">The dexterity value of the being. <see cref="int"/></param>
        /// <param name="resistances">The damage resistances of the being. <see cref="Dictionary{DamageModifiers, double}"/></param>
        /// <param name="weapon">The equipped weapon of the being. <see cref="BaseWeapon"/></param>
        /// <param name="level">The level of the being. <see cref="int"/></param>
        /// <param name="beingClass">The class of the being. <see cref="BeingClass"/></param>
        protected BaseBeing(double health, double mana, string name, double armor, int stamina, int strength, int intelligence, int dexterity, Dictionary<DamageModifiers, double> resistances, BaseWeapon? weapon, int level, BeingClass beingClass)
        {
            this._currentHealth = health;
            this._currentMana = mana;
            this._maxHealth = health;
            this._maxMana = mana;
            this._name = name;
            this._armor = armor;
            this._stamina = stamina;
            this._strength = strength;
            this._intelligence = intelligence;
            this._dexterity = dexterity;
            this._resistances = resistances;
            this._equippedWeapon = weapon;
            this._level = level;
            this._class = beingClass;
        }

        #region Update methods
        /// <summary>
        /// Changes the health value of the being by the specified amount
        /// </summary>
        /// <param name="amount">Amount to change health by (positive to increase, negative to decrease)</param>
        /// <returns>A <see cref="double"/> that represents the actual amount changed</returns>
        public double ChangeCurrentHealth(double amount)
        {
            if(this._currentHealth + amount > this._maxHealth)
            {
                var result = this._maxHealth - this._currentHealth;
                this._currentHealth = this._maxHealth;
                return result;// Return the actual amount changed
            }
            else if (this._currentHealth + amount <= 0)
            {
                this._currentHealth = 0;
                return 0; // Return 0 as no healing occurred
            }
            else
            {
                this._currentHealth += amount;
                return amount; // Return the actual amount changed
            }
        }

        /// <summary>
        /// Changes the mana value of the being by the specified amount
        /// </summary>
        /// <param name="amount">Amount to change mana by (positive to increase, negative to decrease)</param>
        /// <returns>A <see cref="double"/> that represents the actual amount restored</returns>
        public double ChangeCurrentMana(double amount)
        {
            var previousMana = this._currentMana;
            this._currentMana += amount;
            if (this._currentMana > this._maxMana)
            {
                this._currentMana = this._maxMana;
                return this._maxMana - previousMana; // Return the actual amount changed
            }
            else if (this._currentMana <= 0)
            {
                this._currentMana = 0;
                return 0; // Return 0 as no restoration occurred
            }
            else
            {
                return amount; // Return the actual amount changed
            }
        }

        /// <summary>
        /// Method to change max health value
        /// </summary>
        /// <param name="amount">Amount to change health by</param>
        public void ChangeMaxHealth(double amount)
        {
            this._maxHealth += amount;
        }

        /// <summary>
        /// Method to change max mana value
        /// </summary>
        /// <param name="amount"></param>
        public void ChangeMaxMana(double amount)
        {
            this._maxMana += amount;
        }

        /// <summary>
        /// Changes the name of the being
        /// </summary>
        /// <param name="newName">The new name to assign to the being</param>
        public void ChangeName(string newName)
        {
            this._name = newName;
        }

        /// <summary>
        /// Changes the armor value of the being by the specified amount
        /// </summary>
        /// <param name="amount">Amount of armor to change (positive to increase, negative to decrease)</param>
        public void ChangeArmor(int amount)
        {
            this._armor += amount;
        }

        /// <summary>
        /// Changes the stamina value of the being by the specified amount
        /// </summary>
        /// <param name="amount">Amount to change stamina by (positive to increase, negative to decrease)</param>
        public void ChangeStamina(int amount)
        {
            this._stamina += amount;
        }

        /// <summary>
        /// Changes the strength value of the being by the specified amount
        /// </summary>
        /// <param name="amount">Amount to change strength by (positive to increase, negative to decrease)</param>
        public void ChangeStrength(int amount)
        {
            this._strength += amount;
        }

        /// <summary>
        /// Changes the intelligence value of the being by the specified amount
        /// </summary>
        /// <param name="amount">Amount to change intelligence by (positive to increase, negative to decrease)</param>
        public void ChangeIntelligence(int amount)
        {
            this._intelligence += amount;
        }

        /// <summary>
        /// Changes the dexterity value of the being by the specified amount
        /// </summary>
        /// <param name="amount">Amount to change dexterity by (positive to increase, negative to decrease)</param>
        public void ChangeDexterity(int amount)
        {
            this._dexterity += amount;
        }
        #endregion

        #region Setters and Getters
        /// <summary>
        /// Gets the current health of the being
        /// </summary>
        /// <returns>The health value as a double</returns>
        public double GetCurrentHealth()
        {
            return this._currentHealth;
        }

        /// <summary>
        /// Sets the health of the being to a specific value
        /// </summary>
        /// <param name="health">The new health value to set</param>
        public void SetCurrentHealth(double health)
        {
            this._currentHealth = health;
        }

        /// <summary>
        /// Gets the current mana of the being
        /// </summary>
        /// <returns>The mana value as a double</returns>
        public double GetCurrentMana()
        {
            return this._currentMana;
        }

        /// <summary>
        /// Sets the mana of the being to a specific value
        /// </summary>
        /// <param name="mana">The new mana value to set</param>
        public void SetCurrentMana(double mana)
        {
            this._currentMana = mana;
        }

        // <summary>
        /// Gets the maximum health of the being
        /// </summary>
        public double GetMaxHealth()
        {
            return this._maxHealth;
        }

        /// <summary>
        /// Sets the maximum health of the being
        /// </summary>
        /// <param name="maxHealth">Amount to set</param>
        public void SetMaxHealth(double maxHealth)
        {
            this._maxHealth = maxHealth;
        }

        /// <summary>
        /// Gets the maximum mana of the being
        /// </summary>
        public double GetMaxMana()
        {
            return this._maxMana;
        }

        /// <summary>
        /// Sets the maximum mana of the being
        /// </summary>
        /// <param name="maxMana">Amount to set</param>
        public void SetMaxMana(double maxMana)
        {
            this._maxMana = maxMana;
        }   

        /// <summary>
        /// Gets the current name of the being
        /// </summary>
        /// <returns>The name as a string</returns>
        public string GetName()
        {
            return this._name;
        }

        /// <summary>
        /// Sets the name of the being to a specific value
        /// </summary>
        /// <param name="name">The new name to set</param>
        public void SetName(string name)
        {
            this._name = name;
        }

        /// <summary>
        /// Gets the current armor value of the being
        /// </summary>
        /// <returns>The armor value as a double</returns>
        public double GetArmor()
        {
            return this._armor;
        }

        /// <summary>
        /// Sets the armor of the being to a specific value
        /// </summary>
        /// <param name="armor">The new armor value to set</param>
        public void SetArmor(double armor)
        {
            this._armor = armor;
        }

        /// <summary>
        /// Gets the current stamina value of the being
        /// </summary>
        /// <returns>The stamina value as an int</returns>
        public int GetStamina()
        {
            return this._stamina;
        }

        /// <summary>
        /// Sets the stamina of the being to a specific value
        /// </summary>
        /// <param name="stamina">The new stamina value to set</param>
        public void SetStamina(int stamina)
        {
            this._stamina = stamina;
        }

        /// <summary>
        /// Gets the current strength value of the being
        /// </summary>
        /// <returns>The strength value as an int</returns>
        public int GetStrength()
        {
            return this._strength;
        }

        /// <summary>
        /// Sets the strength of the being to a specific value
        /// </summary>
        /// <param name="strength">The new strength value to set</param>
        public void SetStrength(int strength)
        {
            this._strength = strength;
        }

        /// <summary>
        /// Gets the current intelligence value of the being
        /// </summary>
        /// <returns>The intelligence value as an int</returns>
        public int GetIntelligence()
        {
            return this._intelligence;
        }

        /// <summary>
        /// Sets the intelligence of the being to a specific value
        /// </summary>
        /// <param name="intelligence">The new intelligence value to set</param>
        public void SetIntelligence(int intelligence)
        {
            this._intelligence = intelligence;
        }

        /// <summary>
        /// Gets the current dexterity value of the being
        /// </summary>
        /// <returns>The dexterity value as an int</returns>
        public int GetDexterity()
        {
            return this._dexterity;
        }

        /// <summary>
        /// Sets the dexterity of the being to a specific value
        /// </summary>
        /// <param name="dexterity">The new dexterity value to set</param>
        public void SetDexterity(int dexterity)
        {
            this._dexterity = dexterity;
        }

        /// <summary>
        /// Gets the resistances dictionary of the being
        /// </summary>
        /// <returns>The resistances dictionary mapping DamageModifiers to resistance values</returns>
        public Dictionary<DamageModifiers, double> GetResistances()
        {
            return this._resistances;
        }

        /// <summary>
        /// Sets the resistances dictionary of the being
        /// </summary>
        /// <param name="resistances">The new resistances dictionary to set</param>
        public void SetResistances(Dictionary<DamageModifiers, double> resistances)
        {
            this._resistances = resistances;
        }

        /// <summary>
        /// Gets the current level of the being
        /// </summary>
        /// <returns>The level as an <see cref="int"/></returns>
        public int GetLevel()
        {
            return this._level;
        }

        /// <summary>
        /// Retrieves the currently equipped weapon.
        /// </summary>
        /// <returns>The currently equipped weapon as a <see cref="BaseWeapon"/> instance, or <see langword="null"/> if no weapon
        /// is equipped.</returns>
        public BaseWeapon? GetEquippedWeapon()
        {
            return this._equippedWeapon;
        }

        /// <summary>
        /// Sets the class of the being.
        /// </summary>
        /// <param name="beingClass"><see cref="BeingClass"/></param>
        public void SetBeingClass(BeingClass beingClass)
        {
            this._class = beingClass;
        }

        /// <summary>
        /// Gets the class of the being.
        /// </summary>
        /// <returns><see cref="BeingClass"/></returns>
        public BeingClass GetBeingClass()
        {
            return this._class;
        }
        #endregion

        #region Calculation Methods

        /// <summary>
        /// Gets the total amount of damage the being can deal with a single attack.
        /// </summary>
        /// <returns>The total attack damage as a double.</returns>
        public double GetAmountAttack()
        {
            if (this._equippedWeapon == null)
            {
                return this.GetStrength();
            }

            double baseTotalDamage = this._equippedWeapon.GetBaseDamage();

            switch (this._equippedWeapon.GetDamageType())
            {
                case DamageTypes.Physical:
                    return baseTotalDamage + (baseTotalDamage * this.GetStrength() / 100);
                case DamageTypes.Magical:
                    return baseTotalDamage + (baseTotalDamage * this.GetIntelligence() / 100);
                default:
                    return baseTotalDamage;
            }
        }

        /// <summary>
        /// Applies damage to the being, taking into account any damage modifiers.
        /// </summary>
        /// <param name="baseDamage">The base damage to apply.</param>
        /// <param name="damageModifiers">A dictionary of damage modifiers to apply.</param>
        /// <returns>True if the being is dead after taking damage, false otherwise.</returns>
        public bool TakeDamage(double amountAttack, Dictionary<DamageModifiers, double> damageModifiers)
        {
            if (amountAttack < 0)
                throw new ArgumentException("Damage amount cannot be negative.", nameof(amountAttack));

            double totalDamage = amountAttack;

            if (damageModifiers.Count > 0)
            {
                foreach (var attackModifier in damageModifiers)
                {
                    totalDamage += CalculateDamageAfterResistances(attackModifier.Value, attackModifier.Key);
                }
            }
            this.ChangeCurrentHealth(-totalDamage);
            return this.GetCurrentHealth() <= 0;
        }

        /// <summary>
        /// Heals the being by the specified amount.
        /// </summary>
        /// <param name="amount">A <see cref="double"/> representing the amount to heal.</param>
        /// <returns>The actual amount healed as a <see cref="double"/>.</returns>
        public double Heal(double amount)
        {
            if (amount < 0)
                throw new ArgumentException("Heal amount cannot be negative.", nameof(amount));

            if (this.GetCurrentHealth() == this.GetMaxHealth())
            {
                return 0f;
            }

            return this.ChangeCurrentHealth(amount);
        }

        /// <summary>
        /// Calculates the effective damage after applying resistances.
        /// </summary>
        /// <param name="damage">Damage base <see cref="double"/></param>
        /// <param name="damageModifier">Damage modifier type <see cref="DamageModifiers"/></param>
        /// <returns>The actual damage dealt after applying resistances.</returns>
        private double CalculateDamageAfterResistances(double damage, DamageModifiers damageModifier)
        { 
            if (this._resistances.TryGetValue(damageModifier, out double resistanceValue))
            {
                double damageReduction = damage * (resistanceValue / 100); //percentage reduction
                return damage - damageReduction;
            }
            return damage;
        }
        #endregion
    }
}
