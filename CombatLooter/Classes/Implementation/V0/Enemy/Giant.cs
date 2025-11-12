using CombatLooter.Enum;

namespace CombatLooter.Classes.Implementation.V0.Enemy
{
    public class Giant : BaseBeing
    {
        private string[] _names = { "Hill Giant", "Stone Giant", "Frost Giant" };

        public Giant() : base()
        {
            Name = "Giant";
            Class = BeingClass.Giant;
        }

        public Giant(double health, double mana, string name, double armor, int stamina, int strength, int intelligence, int dexterity, Dictionary<DamageModifiers, double> resistances, BaseWeapon weapon, int level, BeingClass beingClass) : base(health, mana, name, armor, stamina, strength, intelligence, dexterity, resistances, weapon, level, beingClass)
        {
        }

        public Giant(int level) : base()
        {
            Name = _names[Random.Shared.Next(0, _names.Length)];
            Level = level;
            Class = BeingClass.Giant;
            switch (Name)
            {
                case "Hill Giant":
                    {
                        MaxHealth = 150 + (level * 25);
                        MaxMana = 30 + (level * 5);
                        Armor = 15 + (level * 4);
                        Stamina = 20 + level;
                        Strength = 25 + level;
                        Intelligence = 8 + (level / 2);
                        Dexterity = 6 + level;
                        EquippedWeapon = null;
                        Resistances = new Dictionary<DamageModifiers, double>
                        {
                            { DamageModifiers.Fire, 40 },
                            { DamageModifiers.Poison, -15 }
                        };
                    }
                    break;
                case "Stone Giant":
                    {
                        MaxHealth = 170 + (level * 30);
                        MaxMana = 20 + (level * 4);
                        Armor = 18 + (level * 5);
                        Stamina = 22 + level;
                        Strength = 28 + level;
                        Intelligence = 7 + (level / 2);
                        Dexterity = 5 + level;
                        EquippedWeapon = null;
                        Resistances = new Dictionary<DamageModifiers, double>
                        {
                            { DamageModifiers.Lightning, 40 },
                            { DamageModifiers.Ice, -15 }
                        };
                    }
                    break;
                case "Frost Giant":
                    {
                        MaxHealth = 160 + (level * 28);
                        MaxMana = 25 + (level * 6);
                        Armor = 16 + (level * 4.5);
                        Stamina = 21 + level;
                        Strength = 26 + level;
                        Intelligence = 9 + (level / 2);
                        Dexterity = 7 + level;
                        EquippedWeapon = null;
                        Resistances = new Dictionary<DamageModifiers, double>
                        {
                            { DamageModifiers.Ice, 40 },
                            { DamageModifiers.Fire, -15 }
                        };
                    }
                    break;
            }
        }
    }
}
