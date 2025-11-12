using CombatLooter.Classes.Implementation.V0.Weapon;
using CombatLooter.Enum;

namespace CombatLooter.Classes.Implementation.V0.Enemy
{
    public class Goblin : BaseBeing
    {
        private string[] _names = { "Goblin Warrior", "Goblin Skirmisher", "Goblin Shaman" };

        public Goblin() : base()
        {
            // Set default values for Goblin-specific properties
            Name = "Goblin";
            
            Class = BeingClass.Goblin;

        }

        public Goblin(double health, double mana, string name, double armor, int stamina, int strength, int intelligence, int dexterity, Dictionary<DamageModifiers, double> resistances, BaseWeapon weapon, int level, BeingClass beingClass) : base(health, mana, name, armor, stamina, strength, intelligence, dexterity, resistances, weapon, level, beingClass)
        {
        }

        public Goblin(int level) : base()
        {
            // Set default values for Goblin-specific properties based on level
            Name = _names[Random.Shared.Next(0, _names.Length)];
            Level = level;
            Class = BeingClass.Goblin;

            switch (Name)
            {
                case "Goblin Warrior":
                    {
                        MaxHealth = 50 + (level * 10);
                        MaxMana = 20 + (level * 5);
                        Armor = 5 + (level * 2);
                        Stamina = 10 + level;
                        Strength = 8 + level;
                        Intelligence = 5 + (level / 2);
                        Dexterity = 12 + level;
                        EquippedWeapon = new MeleeWeapon(WeaponTypes.Melee, 15, DamageTypes.Physical, 2, 1.1, new Dictionary<DamageModifiers, double>(), level, "Rusty Dagger", MeleeWeaponTypes.Dagger);
                    } break;
                case "Goblin Skirmisher":
                    {
                        MaxHealth = 40 + (level * 8);
                        MaxMana = 25 + (level * 6);
                        Armor = 3 + (level * 1.5);
                        Stamina = 8 + level;
                        Strength = 6 + level;
                        Intelligence = 7 + (level / 2);
                        Dexterity = 15 + level;
                        EquippedWeapon = new RangedWeapon(WeaponTypes.Ranged, 12, DamageTypes.Physical, 3, 1.3, new Dictionary<DamageModifiers, double>(), level, "Short Bow", RangedWeaponTypes.Bow);
                    } break;
                case "Goblin Shaman":
                    {
                        MaxHealth = 35 + (level * 7);
                        MaxMana = 40 + (level * 10);
                        Armor = 2 + (level * 1);
                        Stamina = 6 + level;
                        Strength = 4 + level;
                        Intelligence = 12 + (level);
                        Dexterity = 10 + level;
                        EquippedWeapon = new MeleeWeapon(WeaponTypes.Melee, 10, DamageTypes.Magical, 2.5, 1.2, new Dictionary<DamageModifiers, double>(), level, "Shaman's Staff", MeleeWeaponTypes.Staff);
                    } break;
            }
            // Example resistances
            Resistances = new Dictionary<DamageModifiers, double>
            {
                { DamageModifiers.Poison, 0.05 + (level * 0.005) }
            };
        }
    }
}
