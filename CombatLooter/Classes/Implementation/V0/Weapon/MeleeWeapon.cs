using CombatLooter.Enum;

namespace CombatLooter.Classes.Implementation.V0.Weapon
{
    public class MeleeWeapon : BaseWeapon
    {
        private readonly MeleeWeaponTypes _meleeWeaponType;

        public MeleeWeapon(MeleeWeaponTypes meleeWeaponType) : base()
        {
            _meleeWeaponType = meleeWeaponType;
        }

        public MeleeWeapon( WeaponTypes weaponType, 
                            double baseDamage, 
                            DamageTypes damageType, 
                            double weight, 
                            double attackSpeed, 
                            Dictionary<DamageModifiers, double> damageModifiers, 
                            int iLevel, 
                            string name, 
                            MeleeWeaponTypes meleeWeaponType) 
                    : base( weaponType, 
                            baseDamage, 
                            damageType, 
                            weight, 
                            attackSpeed, 
                            damageModifiers, 
                            iLevel, 
                            name)
        {
            _meleeWeaponType = meleeWeaponType;
        }

        #region Properties
        /// <summary>
        /// Gets the type of the melee weapon.
        /// </summary>
        public MeleeWeaponTypes MeleeWeaponType
        {
            get => _meleeWeaponType;
        }
        #endregion
    }
}
