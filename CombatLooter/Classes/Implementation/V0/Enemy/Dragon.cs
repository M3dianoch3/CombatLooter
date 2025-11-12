using CombatLooter.Enum;

namespace CombatLooter.Classes.Implementation.V0.Enemy
{
    public class Dragon : BaseBeing
    {
        private string[] _names = { "Black Dragon", "Red Dragon", "Green Dragon"};

        public Dragon() : base()
        {
        }

        public Dragon(double health, double mana, string name, double armor, int stamina, int strength, int intelligence, int dexterity, Dictionary<DamageModifiers, double> resistances, BaseWeapon weapon, int level)
            : base(health, mana, name, armor, stamina, strength, intelligence, dexterity, resistances, weapon, level, BeingClass.Dragon)
        {
        }

        public Dragon(int level) : base()
        {
            Name = _names[Random.Shared.Next(0, _names.Length)];
            Level = level;
            Class = BeingClass.Dragon;

            switch (Name)
            {
                case "Black Dragon":
                    {
                        MaxHealth = 200 + (level * 30);
                        MaxMana = 100 + (level * 20);
                        Armor = 20 + (level * 5);
                        Stamina = 25 + level;
                        Strength = 30 + level;
                        Intelligence = 15 + (level / 2);
                        Dexterity = 10 + level;
                        EquippedWeapon = null;
                    }
                    break;
                case "Red Dragon":
                    {
                        MaxHealth = 220 + (level * 35);
                        MaxMana = 80 + (level * 15);
                        Armor = 22 + (level * 4);
                        Stamina = 28 + level;
                        Strength = 32 + level;
                        Intelligence = 12 + (level / 2);
                        Dexterity = 12 + level;
                        EquippedWeapon = null;
                    }
                    break;
                case "Green Dragon":
                    {
                        MaxHealth = 180 + (level * 25);
                        MaxMana = 120 + (level * 25);
                        Armor = 18 + (level * 6);
                        Stamina = 22 + level;
                        Strength = 28 + level;
                        Intelligence = 18 + (level / 2);
                        Dexterity = 14 + level;
                        EquippedWeapon = null;
                    }
                    break;
            }

            Resistances = new Dictionary<DamageModifiers, double>
            {
                { DamageModifiers.Fire, 50 },
                { DamageModifiers.Ice, 20 },
                { DamageModifiers.Poison, 30 },
                { DamageModifiers.Lightning, -20 }
            };
        }
    }
}
