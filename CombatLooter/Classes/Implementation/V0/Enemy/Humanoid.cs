using CombatLooter.Classes.Implementation.V0.Weapon;
using CombatLooter.Enum;

namespace CombatLooter.Classes.Implementation.V0.Enemy
{
    public class Humanoid : BaseBeing
    {
        private string[] _names = { "Bandit", "Barbarian", "Assassin" };

        public Humanoid() : base()
        {
            // Set default values for Humanoid-specific properties
            Name = "Humanoid";
            Class = BeingClass.Humanoid;
        }

        public Humanoid(double health, double mana, string name, double armor, int stamina, int strength, int intelligence, int dexterity, Dictionary<DamageModifiers, double> resistances, BaseWeapon weapon, int level, BeingClass beingClass) : base(health, mana, name, armor, stamina, strength, intelligence, dexterity, resistances, weapon, level, beingClass)
        {
        }

        public Humanoid(int level) : base()
        {
            // Set default values for Humanoid-specific properties based on level
            Name = _names[Random.Shared.Next(0, _names.Length)];
            Level = level;
            Class = BeingClass.Humanoid;
            switch (Name)
            {
                case "Bandit":
                    {
                        MaxHealth = 70 + (level * 14);
                        MaxMana = 20 + (level * 5);
                        Armor = 5 + (level * 2);
                        Stamina = 14 + level;
                        Strength = 12 + level;
                        Intelligence = 6 + (level / 2);
                        Dexterity = 16 + level;
                        EquippedWeapon = new MeleeWeapon(WeaponTypes.Melee, 15, DamageTypes.Physical, 1.5, 1.6, new Dictionary<DamageModifiers, double>(), level, "Bandit's scimitar", MeleeWeaponTypes.Sword);
                    } break;
                case "Barbarian":
                    {
                        MaxHealth = 90 + (level * 18);
                        MaxMana = 15 + (level * 4);
                        Armor = 7 + (level * 2.5);
                        Stamina = 16 + level;
                        Strength = 16 + level;
                        Intelligence = 5 + (level / 2);
                        Dexterity = 12 + level;
                        EquippedWeapon = new MeleeWeapon(WeaponTypes.Melee, 25, DamageTypes.Physical, 5, 2.5, new Dictionary<DamageModifiers, double>(), level, "Barbarian's axe", MeleeWeaponTypes.Axe);
                    } break;
                case "Assassin":
                    {
                        MaxHealth = 65 + (level * 13);
                        MaxMana = 25 + (level * 6);
                        Armor = 4 + (level * 1.8);
                        Stamina = 13 + level;
                        Strength = 10 + level;
                        Intelligence = 7 + (level / 2);
                        Dexterity = 18 + level;
                        EquippedWeapon = new MeleeWeapon(WeaponTypes.Melee, 10, DamageTypes.Physical, 1, 1.1, new Dictionary<DamageModifiers, double>(), level, "Assassin's daggers", MeleeWeaponTypes.Dagger);
                    } break;
            }

            Resistances = new Dictionary<DamageModifiers, double>
            {
                { DamageModifiers.Poison, 25 },
                { DamageModifiers.Fire, -10 }
            };
        }
    }
}
