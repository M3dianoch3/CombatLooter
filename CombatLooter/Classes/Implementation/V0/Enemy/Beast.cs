using CombatLooter.Enum;

namespace CombatLooter.Classes.Implementation.V0.Enemy
{
    public class Beast : BaseBeing
    {
        private string[] _names = { "Wolf", "Bear", "Tiger" };

        public Beast() : base()
        {
            // Set default values for Beast-specific properties
            Name = "Beast";
            Class = BeingClass.Beast;
        }

        public Beast(double health, double mana, string name, double armor, int stamina, int strength, int intelligence, int dexterity, Dictionary<DamageModifiers, double> resistances, BaseWeapon weapon, int level, BeingClass beingClass) : base(health, mana, name, armor, stamina, strength, intelligence, dexterity, resistances, weapon, level, beingClass)
        {
        }

        public Beast(int level) : base()
        {
            // Set default values for Beast-specific properties based on level
            Name = _names[Random.Shared.Next(0, _names.Length)];
            Level = level;
            Class = BeingClass.Beast;
            switch (Name)
            {
                case "Wolf":
                    {
                        MaxHealth = 60 + (level * 12);
                        MaxMana = 10 + (level * 3);
                        Armor = 4 + (level * 1.5);
                        Stamina = 12 + level;
                        Strength = 10 + level;
                        Intelligence = 4 + (level / 2);
                        Dexterity = 14 + level;
                        EquippedWeapon = null;
                    } break;
                case "Bear":
                    {
                        MaxHealth = 80 + (level * 15);
                        MaxMana = 15 + (level * 4);
                        Armor = 6 + (level * 2);
                        Stamina = 15 + level;
                        Strength = 14 + level;
                        Intelligence = 5 + (level / 2);
                        Dexterity = 10 + level;
                        EquippedWeapon = null;
                    } break;
                case "Tiger":
                    {
                        MaxHealth = 70 + (level * 13);
                        MaxMana = 12 + (level * 3);
                        Armor = 5 + (level * 1.7);
                        Stamina = 13 + level;
                        Strength = 12 + level;
                        Intelligence = 4 + (level / 2);
                        Dexterity = 16 + level;
                        EquippedWeapon = null;
                    } break;
            }

            Resistances = new Dictionary<DamageModifiers, double>
            {
                { DamageModifiers.Fire, 20 },
                { DamageModifiers.Ice, 20 },
                { DamageModifiers.Lightning, 20 },
                { DamageModifiers.Poison, 20 },
            };
        }
    }
}
