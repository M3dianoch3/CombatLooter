using CombatLooter.Classes.Implementation;
using CombatLooter.Enum;

namespace CombatLooter.Classes.Interface
{
    public interface IWeapon : IItem
    {
        WeaponTypes GetWeaponType();
        double GetBaseDamage();
        void SetBaseDamage(double baseDamage);
        DamageTypes GetDamageType();
        void SetDamageType(DamageTypes damageType);
        double GetWeight();
        void SetWeight(double weight);
        double GetAttackSpeed();
        void SetAttackSpeed(double attackSpeed);
        Dictionary<DamageModifiers, double> GetDamageModifiers();

        DamageResult ModifiersDamage();
    }
}
