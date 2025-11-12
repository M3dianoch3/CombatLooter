using CombatLooter.Classes.Implementation.V0.Weapon;
using CombatLooter.Enum;

namespace CombatLooter.Classes.Implementation.V0.Enemy
{
    public class Undead : BaseBeing
    {
        private string[] _names = { "Skeleton", "Zombie", "Lich" };

        public Undead() : base()
        {
            // Set default values for Undead-specific properties
            Name = "Undead";
            Class = BeingClass.Undead;
        }

        public Undead(double health, double mana, string name, double armor, int stamina, int strength, int intelligence, int dexterity, Dictionary<DamageModifiers, double> resistances, BaseWeapon weapon, int level, BeingClass beingClass) : base(health, mana, name, armor, stamina, strength, intelligence, dexterity, resistances, weapon, level, beingClass)
        {
        }

        public Undead(int level) : base()
        {
            // Set default values for Undead-specific properties based on level
            Name = _names[Random.Shared.Next(0, _names.Length)];
            Level = level;
            Class = BeingClass.Undead;
            switch (Name)
            {
                case "Skeleton":
                    {
                        MaxHealth = 75 + (level * 15);
                        MaxMana = 15 + (level * 4);
                        Armor = 5 + (level * 2);
                        Stamina = 13 + level;
                        Strength = 12 + level;
                        Intelligence = 6 + (level / 2);
                        Dexterity = 14 + level;
                        EquippedWeapon = new MeleeWeapon(WeaponTypes.Melee, 10, DamageTypes.Physical, 2, 1.1, new Dictionary<DamageModifiers, double>(), level, "Skeleton's Sword", MeleeWeaponTypes.Sword);
                    } break;
                case "Zombie":
                    {
                        MaxHealth = 85 + (level * 17);
                        MaxMana = 10 + (level * 3);
                        Armor = 6 + (level * 2.5);
                        Stamina = 15 + level;
                        Strength = 14 + level;
                        Intelligence = 4 + (level / 2);
                        Dexterity = 10 + level;
                        EquippedWeapon = null;
                    } break;
                case "Lich":
                    {
                        MaxHealth = 70 + (level * 12);
                        MaxMana = 50 + (level * 10);
                        Armor = 4 + (level * 1.8);
                        Stamina = 12 + level;
                        Strength = 10 + level;
                        Intelligence = 14 + (level * 2);
                        Dexterity = 12 + level;
                        EquippedWeapon = new MeleeWeapon(WeaponTypes.Melee, 20, DamageTypes.Magical, 5, 1.8, new Dictionary<DamageModifiers, double>(), level, "Lich's Staff", MeleeWeaponTypes.Staff);
                    } break;
            }
            Resistances = new Dictionary<DamageModifiers, double>
            {
                { DamageModifiers.Poison, 100 }, // Invulnerable to Poison
                { DamageModifiers.True, -30 }
            };
        }
    }
}
