using CombatLooter.Classes.Implementation.V0.Weapon;
using CombatLooter.Enum;

namespace CombatLooter.Classes.Implementation.V0.Enemy
{
    public class Demon : BaseBeing
    {
        private string[] _names = { "Imp", "Hellhound", "Succubus" };
        public Demon() : base()
        {
            // Set default values for Demon-specific properties
            Name = "Demon";
            
            Class = BeingClass.Demon;
        }
        public Demon(double health, double mana, string name, double armor, int stamina, int strength, int intelligence, int dexterity, Dictionary<DamageModifiers, double> resistances, BaseWeapon weapon, int level, BeingClass beingClass) : base(health, mana, name, armor, stamina, strength, intelligence, dexterity, resistances, weapon, level, beingClass)
        {
        }
        public Demon(int level) : base()
        {
            // Set default values for Demon-specific properties based on level
            Name = _names[Random.Shared.Next(0, _names.Length)];
            Level = level;
            Class = BeingClass.Demon;
            
            switch (Name)
            {
                case "Imp":
                    {
                        MaxHealth = 60 + (level * 12);
                        MaxMana = 30 + (level * 8);
                        Armor = 4 + (level * 2);
                        Stamina = 12 + level;
                        Strength = 10 + level;
                        Intelligence = 8 + (level / 2);
                        Dexterity = 14 + level;
                        EquippedWeapon = null;
                    } break;
                case "Hellhound":
                    {
                        MaxHealth = 80 + (level * 15);
                        MaxMana = 20 + (level * 5);
                        Armor = 6 + (level * 2.5);
                        Stamina = 15 + level;
                        Strength = 14 + level;
                        Intelligence = 6 + (level / 2);
                        Dexterity = 16 + level;
                        EquippedWeapon = null;
                    } break;
                case "Succubus":
                    {
                        MaxHealth = 70 + (level * 10);
                        MaxMana = 50 + (level * 12);
                        Armor = 5 + (level * 2);
                        Stamina = 10 + level;
                        Strength = 8 + level;
                        Intelligence = 12 + (level / 2);
                        Dexterity = 14 + level;
                        EquippedWeapon = new RangedWeapon(WeaponTypes.Ranged, 20, DamageTypes.Physical, 2.5, 1.3, new Dictionary<DamageModifiers, double> { { DamageModifiers.Shadow, 20.0 } }, 1, "Shadow Bow", RangedWeaponTypes.Bow);
                    } break;
            }

            Resistances = new Dictionary<DamageModifiers, double>
            {
                { DamageModifiers.Shadow, 50 },
                { DamageModifiers.Fire, 20 },
                { DamageModifiers.Ice, 10 },
                { DamageModifiers.True, -20 }
            };
        }
    }
}
