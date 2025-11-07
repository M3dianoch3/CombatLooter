using CombatLooter.Enum;

namespace CombatLooter.Classes.Implementation.V0.Enemy
{
    public class Goblin : BaseBeing
    {
        public Goblin() : base()
        {
            // Set default values for Goblin-specific properties
            Name = "Goblin";
            
            Class = BeingClass.Goblin;

        }

        public Goblin(double health, double mana, string name, double armor, int stamina, int strength, int intelligence, int dexterity, Dictionary<DamageModifiers, double> resistances, BaseWeapon weapon, int level, BeingClass beingClass) : base(health, mana, name, armor, stamina, strength, intelligence, dexterity, resistances, weapon, level, beingClass)
        {
        }
    }
}
