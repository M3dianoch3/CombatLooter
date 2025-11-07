using CombatLooter.Classes.Interface;
using CombatLooter.Enum;

namespace CombatLooter.Classes.Implementation
{
    public abstract class BaseWeapon : BaseItem, IWeapon
    {
        private readonly WeaponTypes _weaponType;
        private double _baseDamage;
        private DamageTypes _damageType;
        private double _weight;
        private double _attackSpeed;

        private Dictionary<DamageModifiers, double> _damageModifiers;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseWeapon"/> class with default values.
        /// </summary>
        public BaseWeapon() : base()
        {
            this._weaponType = WeaponTypes.Melee;
            this._baseDamage = 10.0;
            this._damageType = DamageTypes.Physical;
            this._weight = 5.0;
            this._attackSpeed = 1.0;
            this._damageModifiers = new Dictionary<DamageModifiers, double>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseWeapon"/> class with specified values.
        /// </summary>
        /// <param name="weaponType">The type of the weapon. <see cref="WeaponTypes"/></param>
        /// <param name="baseDamage">The base damage of the weapon. <see cref="double"/></param>
        /// <param name="damageType">The damage type of the weapon. <see cref="DamageTypes"/></param>
        /// <param name="weight">The weight of the weapon. <see cref="double"/></param>
        /// <param name="attackSpeed">The attack speed of the weapon. <see cref="double"/></param>
        /// <param name="damageModifiers">The damage modifiers for the weapon. <see cref="Dictionary{DamageModifiers, double}"/></param>
        /// <param name="iLevel">The item level of the weapon. <see cref="int"/></param>
        /// <param name="name">The name of the weapon. <see cref="string"/></param>
        protected BaseWeapon(WeaponTypes weaponType, double baseDamage, DamageTypes damageType, double weight, double attackSpeed, Dictionary<DamageModifiers, double> damageModifiers, int iLevel, string name) : base(iLevel, name)
        {
            this._weaponType = weaponType;
            this._baseDamage = baseDamage;
            this._damageType = damageType;
            this._weight = weight;
            this._attackSpeed = attackSpeed;
            this._damageModifiers = damageModifiers;
        }

        #region Setters and Getters
        /// <summary>
        /// Gets the type of the weapon.
        /// </summary>
        /// <returns>The weapon type. <see cref="WeaponTypes"/></returns>
        public WeaponTypes GetWeaponType()
        {
            return this._weaponType;
        }

        /// <summary>
        /// Gets the base damage of the weapon.
        /// </summary>
        /// <returns>The base damage value. <see cref="double"/></returns>
        public double GetBaseDamage()
        {
            return this._baseDamage;
        }

        /// <summary>
        /// Sets the base damage of the weapon.
        /// </summary>
        /// <param name="baseDamage">The base damage value to set. <see cref="double"/></param>
        public void SetBaseDamage(double baseDamage)
        {
            this._baseDamage = baseDamage;
        }

        /// <summary>
        /// Gets the damage type of the weapon.
        /// </summary>
        /// <returns>The damage type. <see cref="DamageTypes"/></returns>
        public DamageTypes GetDamageType()
        {
            return this._damageType;
        }

        /// <summary>
        /// Sets the damage type of the weapon.
        /// </summary>
        /// <param name="damageType">The damage type to set. <see cref="DamageTypes"/></param>
        public void SetDamageType(DamageTypes damageType)
        {
            this._damageType = damageType;
        }

        /// <summary>
        /// Gets the weight of the weapon.
        /// </summary>
        /// <returns>The weight value. <see cref="double"/></returns>
        public double GetWeight()
        {
            return this._weight;
        }

        /// <summary>
        /// Sets the weight of the weapon.
        /// </summary>
        /// <param name="weight">The weight value to set. <see cref="double"/></param>
        public void SetWeight(double weight)
        {
            this._weight = weight;
        }

        /// <summary>
        /// Gets the attack speed of the weapon.
        /// </summary>
        /// <returns>The attack speed value. <see cref="double"/></returns>
        public double GetAttackSpeed()
        {
            return this._attackSpeed;
        }

        /// <summary>
        /// Sets the attack speed of the weapon.
        /// </summary>
        /// <param name="attackSpeed">The attack speed value to set. <see cref="double"/></param>
        public void SetAttackSpeed(double attackSpeed)
        {
            this._attackSpeed = attackSpeed;
        }

        /// <summary>
        /// Gets a copy of the damage modifiers dictionary.
        /// </summary>
        /// <returns>A dictionary containing all damage modifiers. <see cref="Dictionary{DamageModifiers, double}"/></returns>
        public Dictionary<DamageModifiers, double> GetDamageModifiers()
        {
            return new Dictionary<DamageModifiers, double>(this._damageModifiers);
        }
        #endregion

        #region Update methods
        /// <summary>
        /// Changes the base damage of the weapon by the specified amount.
        /// </summary>
        /// <param name="amount">The amount to add to the base damage. Can be positive or negative.</param>
        public void ChangeBaseDamage(double amount)
        {
            this._baseDamage += amount;
        }

        /// <summary>
        /// Changes the weapon's attack speed to a new value.
        /// </summary>
        /// <param name="newSpeed">The amount to add to the attack speed. Can be positive or negative. <see cref="double"/></param>
        public void ChangeWeaponAttackSpeed(double amount)
        {
            this._attackSpeed += amount;
        }

        /// <summary>
        /// Changes the weapon's weight to a new value.
        /// </summary>
        /// <param name="newWeight">The amount to add to the weight value. Can be positive or negative. <see cref="double"/></param>
        public void ChangeWeaponWeight(double amount)
        {
            this._weight += amount;
        }
        #endregion

        #region Update damage modifiers methods
        /// <summary>
        /// Adds a damage modifier to the weapon. If the modifier already exists, it increments its value.
        /// </summary>
        /// <param name="type"><see cref="DamageTypes"/>Damage type.</param>
        /// <param name="modifier"><see cref="double"/>Modifier</param>
        public void AddDamageModifier(DamageModifiers type, double modifier)
        {
            if (this._damageModifiers.ContainsKey(type))
            {
                this._damageModifiers[type] += modifier;
            }
            else
            {
                this._damageModifiers[type] = modifier;
            }
        }

        /// <summary>
        /// Decrease a damage modifier from the weapon. If the modifier value reaches zero or below, it is removed.
        /// </summary>
        /// <param name="type"><see cref="DamageTypes"/>Damage type.</param>
        /// <param name="modifier"><see cref="double"/>Modifier</param>
        public void DecreaseDamageModifier(DamageModifiers type, double modifier)
        {
            if (this._damageModifiers.ContainsKey(type))
            {
                this._damageModifiers[type] -= modifier;
                if (this._damageModifiers[type] <= 0)
                {
                    RemoveDamageModifier(type);
                }
            }
        }

        /// <summary>
        /// Removes completely a damage modifier from the weapon.
        /// </summary>
        /// <param name="type"><see cref="DamageTypes"/>Damage type to remove</param>
        public void RemoveDamageModifier(DamageModifiers type)
        {
            if (this._damageModifiers.ContainsKey(type))
            {
                this._damageModifiers.Remove(type);
            }
        }
        #endregion

        #region Calculation methods
        /// <summary>
        /// Return the total damage of the weapon, including all damage types and their modifiers.
        /// </summary>
        /// <returns><see cref="DamageResult"/>DamageResult object.</returns>
        public DamageResult ModifiersDamage()
        {
            var result = new DamageResult();
            double totalDamage = 0;

            foreach (DamageModifiers damageType in System.Enum.GetValues<DamageModifiers>())
            {
                if(_damageModifiers.TryGetValue(damageType, out double modifier))
                {
                    result.AddDamage(damageType, modifier);
                    totalDamage += modifier;
                }
            }
            result.Total = totalDamage;
            return result;
        }
        #endregion
    }

    /// <summary>
    /// DamageResult class to hold damage values by type.
    /// </summary>
    public class DamageResult
    {
        private Dictionary<DamageModifiers, double> _damageByType;
        public double Total { get; set; }

        public DamageResult()
        {
            _damageByType = new Dictionary<DamageModifiers, double>();
            Total = 0;
        }

        /// <summary>
        /// Add damage of a specific type.
        /// </summary>
        /// <param name="type"><see cref="DamageTypes"/>Damage Type</param>
        /// <param name="amount"><see cref="double"/>Amount of damage</param>
        public void AddDamage(DamageModifiers type, double amount)
        {
            if (_damageByType.ContainsKey(type))
            {
                _damageByType[type] += amount;
            }
            else
            {
                _damageByType[type] = amount;
            }
        }

        /// <summary>
        /// Returns a dictionary with damage by type.
        /// </summary>
        /// <returns><see cref="Dictionary{DamageModifiers, double}"/>Dictionary with all damage types and their values</returns>
        public Dictionary<DamageModifiers, double> GetDamageByType()
        {
            return _damageByType;
        }
    }
}
