using CombatLooter.Enum;

namespace CombatLooter.Classes.Implementation.V0.Weapon
{
    public class RangedWeapon : BaseWeapon
    {
        private readonly RangedWeaponTypes _rangedWeaponType;

        public RangedWeapon(RangedWeaponTypes rangedWeaponType) : base()
        {
            _rangedWeaponType = rangedWeaponType;
        }

        public RangedWeapon(WeaponTypes weaponType,
                            double baseDamage,
                            DamageTypes damageType,
                            double weight,
                            double attackSpeed,
                            Dictionary<DamageModifiers, double> damageModifiers,
                            int iLevel,
                            string name,
                            RangedWeaponTypes rangedWeaponType)
                    : base(weaponType,
                            baseDamage,
                            damageType,
                            weight,
                            attackSpeed,
                            damageModifiers,
                            iLevel,
                            name)
        {
            _rangedWeaponType = rangedWeaponType;
        }

        #region Properties
        /// <summary>
        /// Gets the type of the ranged weapon.
        /// </summary>
        public RangedWeaponTypes RangedWeaponType
        {
            get => _rangedWeaponType;
        }
        #endregion
    }
}
