using CombatLooter.Classes.Implementation.V0.Weapon;
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

        #region Properties

        /// <summary>
        /// Gets or sets the current health of the being.
        /// </summary>
        public double CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }

        /// <summary>
        /// Gets or sets the current mana of the being.
        /// </summary>
        public double CurrentMana
        {
            get => _currentMana;
            set => _currentMana = value;
        }

        /// <summary>
        /// Gets or sets the maximum health of the being.
        /// </summary>
        public double MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        /// <summary>
        /// Gets or sets the maximum mana of the being.
        /// </summary>
        public double MaxMana
        {
            get => _maxMana;
            set => _maxMana = value;
        }

        /// <summary>
        /// Gets or sets the name of the being.
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        /// <summary>
        /// Gets or sets the armor value of the being.
        /// </summary>
        public double Armor
        {
            get => _armor;
            set => _armor = value;
        }

        /// <summary>
        /// Gets or sets the stamina value of the being.
        /// </summary>
        public int Stamina
        {
            get => _stamina;
            set => _stamina = value;
        }

        /// <summary>
        /// Gets or sets the strength value of the being.
        /// </summary>
        public int Strength
        {
            get => _strength;
            set => _strength = value;
        }

        /// <summary>
        /// Gets or sets the intelligence value of the being.
        /// </summary>
        public int Intelligence
        {
            get => _intelligence;
            set => _intelligence = value;
        }

        /// <summary>
        /// Gets or sets the dexterity value of the being.
        /// </summary>
        public int Dexterity
        {
            get => _dexterity;
            set => _dexterity = value;
        }

        /// <summary>
        /// Gets or sets the resistances dictionary of the being.
        /// </summary>
        public Dictionary<DamageModifiers, double> Resistances
        {
            get => _resistances;
            set => _resistances = value;
        }

        /// <summary>
        /// Gets or sets the currently equipped weapon.
        /// </summary>
        public BaseWeapon? EquippedWeapon
        {
            get => _equippedWeapon;
            set => _equippedWeapon = value;
        }

        /// <summary>
        /// Gets or sets the current level of the being.
        /// </summary>
        public int Level
        {
            get => _level;
            set => _level = value;
        }

        /// <summary>
        /// Gets or sets the class of the being.
        /// </summary>
        public BeingClass Class
        {
            get => _class;
            set => _class = value;
        }

        #endregion

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
        /// Changes the max health value by the specified amount
        /// </summary>
        /// <param name="amount">Amount to change max health by</param>
        public void ChangeMaxHealth(double amount) => _maxHealth += amount;

        /// <summary>
        /// Changes the max mana value by the specified amount
        /// </summary>
        /// <param name="amount">Amount to change max mana by</param>
        public void ChangeMaxMana(double amount) => _maxMana += amount;

        /// <summary>
        /// Changes the name of the being
        /// </summary>
        /// <param name="newName">The new name to assign to the being</param>
        public void ChangeName(string newName) => _name = newName;

        /// <summary>
        /// Changes the armor value of the being by the specified amount
        /// </summary>
        /// <param name="amount">Amount of armor to change (positive to increase, negative to decrease)</param>
        public void ChangeArmor(double amount) => _armor += amount;

        /// <summary>
        /// Changes the stamina value of the being by the specified amount
        /// </summary>
        /// <param name="amount">Amount to change stamina by (positive to increase, negative to decrease)</param>
        public void ChangeStamina(int amount) => _stamina += amount;

        /// <summary>
        /// Changes the strength value of the being by the specified amount
        /// </summary>
        /// <param name="amount">Amount to change strength by (positive to increase, negative to decrease)</param>
        public void ChangeStrength(int amount) => _strength += amount;

        /// <summary>
        /// Changes the intelligence value of the being by the specified amount
        /// </summary>
        /// <param name="amount">Amount to change intelligence by (positive to increase, negative to decrease)</param>
        public void ChangeIntelligence(int amount) => _intelligence += amount;

        /// <summary>
        /// Changes the dexterity value of the being by the specified amount
        /// </summary>
        /// <param name="amount">Amount to change dexterity by (positive to increase, negative to decrease)</param>
        public void ChangeDexterity(int amount) => _dexterity += amount;
        #endregion

        #region Backward Compatibility Methods (Deprecated - Use Properties Instead)

        [Obsolete("Use CurrentHealth property instead")]
        public double GetCurrentHealth() => CurrentHealth;

        [Obsolete("Use CurrentHealth property instead")]
        public void SetCurrentHealth(double health) => CurrentHealth = health;

        [Obsolete("Use CurrentMana property instead")]
        public double GetCurrentMana() => CurrentMana;

        [Obsolete("Use CurrentMana property instead")]
        public void SetCurrentMana(double mana) => CurrentMana = mana;

        [Obsolete("Use MaxHealth property instead")]
        public double GetMaxHealth() => MaxHealth;

        [Obsolete("Use MaxHealth property instead")]
        public void SetMaxHealth(double maxHealth) => MaxHealth = maxHealth;

        [Obsolete("Use MaxMana property instead")]
        public double GetMaxMana() => MaxMana;

        [Obsolete("Use MaxMana property instead")]
        public void SetMaxMana(double maxMana) => MaxMana = maxMana;

        [Obsolete("Use Name property instead")]
        public string GetName() => Name;

        [Obsolete("Use Name property instead")]
        public void SetName(string name) => Name = name;

        [Obsolete("Use Armor property instead")]
        public double GetArmor() => Armor;

        [Obsolete("Use Armor property instead")]
        public void SetArmor(double armor) => Armor = armor;

        [Obsolete("Use Stamina property instead")]
        public int GetStamina() => Stamina;

        [Obsolete("Use Stamina property instead")]
        public void SetStamina(int stamina) => Stamina = stamina;

        [Obsolete("Use Strength property instead")]
        public int GetStrength() => Strength;

        [Obsolete("Use Strength property instead")]
        public void SetStrength(int strength) => Strength = strength;

        [Obsolete("Use Intelligence property instead")]
        public int GetIntelligence() => Intelligence;

        [Obsolete("Use Intelligence property instead")]
        public void SetIntelligence(int intelligence) => Intelligence = intelligence;

        [Obsolete("Use Dexterity property instead")]
        public int GetDexterity() => Dexterity;

        [Obsolete("Use Dexterity property instead")]
        public void SetDexterity(int dexterity) => Dexterity = dexterity;

        [Obsolete("Use Resistances property instead")]
        public Dictionary<DamageModifiers, double> GetResistances() => Resistances;

        [Obsolete("Use Resistances property instead")]
        public void SetResistances(Dictionary<DamageModifiers, double> resistances) => Resistances = resistances;

        [Obsolete("Use Level property instead")]
        public int GetLevel() => Level;

        [Obsolete("Use EquippedWeapon property instead")]
        public BaseWeapon? GetEquippedWeapon() => EquippedWeapon;

        [Obsolete("Use Class property instead")]
        public void SetBeingClass(BeingClass beingClass) => Class = beingClass;

        [Obsolete("Use Class property instead")]
        public BeingClass GetBeingClass() => Class;

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
                return Strength;
            }

            double baseTotalDamage = this._equippedWeapon.GetBaseDamage();

            switch (this._equippedWeapon.GetDamageType())
            {
                case DamageTypes.Physical:
                    if (this._equippedWeapon is MeleeWeapon)
                    {
                        return baseTotalDamage + (baseTotalDamage * Strength / 100);
                    }
                    else if (this._equippedWeapon is RangedWeapon)
                    {
                        return baseTotalDamage + (baseTotalDamage * Dexterity / 100);
                    }
                    return baseTotalDamage;
                case DamageTypes.Magical:
                    return baseTotalDamage + (baseTotalDamage * Intelligence / 100);
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
            return CurrentHealth <= 0;
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

            if (CurrentHealth == MaxHealth)
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

        /// <summary>
        /// Override ToString method to provide a string representation of the Being
        /// </summary>
        /// <returns>A string representing the Being's attributes.</returns>
        public override string ToString()
        {
            var stringResult = $"Name: {this._name}, Class: {this._class}, Level: {this._level}, Health: {this._currentHealth}/{this._maxHealth}, Mana: {this._currentMana}/{this._maxMana}, Armor: {this._armor}, Stamina: {this._stamina}, Strength: {this._strength}, Intelligence: {this._intelligence}, Dexterity: {this._dexterity}";
            return stringResult;    
        }
    }
}
