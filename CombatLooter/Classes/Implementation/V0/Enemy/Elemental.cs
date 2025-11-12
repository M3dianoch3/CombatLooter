using CombatLooter.Enum;

namespace CombatLooter.Classes.Implementation.V0.Enemy
{
    public class Elemental : BaseBeing
    {
        private string[] _names = { "Fire Elemental", "Ice Elemental", "Lightning Elemental" };
        public Elemental() : base()
        {
            // Set default values for Elemental-specific properties
            Name = "Elemental";
            Class = BeingClass.Elemental;
        }

        public Elemental(double health, double mana, string name, double armor, int stamina, int strength, int intelligence, int dexterity, Dictionary<DamageModifiers, double> resistances, BaseWeapon weapon, int level, BeingClass beingClass) : base(health, mana, name, armor, stamina, strength, intelligence, dexterity, resistances, weapon, level, beingClass)
        {
        }

        public Elemental(int level) : base()
        {
            // Set default values for Elemental-specific properties based on level
            Name = _names[Random.Shared.Next(0, _names.Length)];
            Level = level;
            Class = BeingClass.Elemental;
            switch (Name)
            {
                case "Fire Elemental":
                    {
                        MaxHealth = 90 + (level * 18);
                        MaxMana = 80 + (level * 15);
                        Armor = 7 + (level * 2.5);
                        Stamina = 14 + level;
                        Strength = 12 + level;
                        Intelligence = 16 + (level / 2);
                        Dexterity = 10 + level;
                        EquippedWeapon = null;
                        Resistances = new Dictionary<DamageModifiers, double>
                        {
                            { DamageModifiers.Fire, 50 },
                            { DamageModifiers.Ice, -20 }
                        };
                    }
                    break;
                case "Ice Elemental":
                    {
                        MaxHealth = 85 + (level * 16);
                        MaxMana = 90 + (level * 18);
                        Armor = 6 + (level * 2);
                        Stamina = 13 + level;
                        Strength = 10 + level;
                        Intelligence = 18 + (level / 2);
                        Dexterity = 12 + level;
                        EquippedWeapon = null;
                        Resistances = new Dictionary<DamageModifiers, double>
                        {
                            { DamageModifiers.Ice, 50 },
                            { DamageModifiers.Fire, -20 }
                        };
                    }
                    break;
                case "Lightning Elemental":
                    {
                        MaxHealth = 100 + (level * 20);
                        MaxMana = 70 + (level * 12);
                        Armor = 8 + (level * 3);
                        Stamina = 16 + level;
                        Strength = 14 + level;
                        Intelligence = 12 + (level / 2);
                        Dexterity = 8 + level;
                        EquippedWeapon = null;
                        Resistances = new Dictionary<DamageModifiers, double>
                        {
                            { DamageModifiers.Lightning, 50 },
                            { DamageModifiers.Shadow, -20 }
                        };
                    }
                    break;
            }
        }
    }
}
