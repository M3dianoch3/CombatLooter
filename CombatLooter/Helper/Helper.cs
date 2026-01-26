using CombatLooter.Classes.Implementation;
using CombatLooter.Classes.Implementation.V0.Armor;
using CombatLooter.Classes.Implementation.V0.Player;
using CombatLooter.Classes.Implementation.V0.Weapon;
using CombatLooter.Constants;
using CombatLooter.Enum;
using CombatLooter.Extensions;
using CombatLooter.Services.NameGeneratorService.Implementation;

namespace CombatLooter.Helper
{
    public static class Helper
    {
        private static NameGeneratorService _nameGeneratorService = new NameGeneratorService();

        #region Armor Helpers

        private static readonly Dictionary<ArmorSlots, double> _armorValueDefaultPerSlot = new Dictionary<ArmorSlots, double>
        {
            { ArmorSlots.Head, 5.0 },
            { ArmorSlots.Chest, 15.0 },
            { ArmorSlots.Legs, 10.0 },
            { ArmorSlots.Feet, 5.0 },
            { ArmorSlots.Hands, 3.0 },
            { ArmorSlots.Shoulders, 7.0 },
            { ArmorSlots.Waist, 2.0 }
        };
        private static readonly Dictionary<ArmorSlots, double> _armorWeightDefaultPerSlot = new Dictionary<ArmorSlots, double>
        {
            { ArmorSlots.Head, 1.5 },
            { ArmorSlots.Chest, 4.0 },
            { ArmorSlots.Legs, 3.0 },
            { ArmorSlots.Feet, 1.5 },
            { ArmorSlots.Hands, 1.0 },
            { ArmorSlots.Shoulders, 2.0 },
            { ArmorSlots.Waist, 1.0 }
        };
        private static readonly Dictionary<ArmorTypes, double> _armorTypeMultiplier = new Dictionary<ArmorTypes, double>
        {
            { ArmorTypes.Light, 1.0 },
            { ArmorTypes.Medium, 1.75 },
            { ArmorTypes.Heavy, 2.0 },
        };

        public static Dictionary<ArmorSlots, double> GetDefaultArmorValues()
        {
            return _armorValueDefaultPerSlot;
        }

        public static Dictionary<ArmorSlots, double> GetDefaultArmorWeights()
        {
            return _armorWeightDefaultPerSlot;
        }

        public static Dictionary<ArmorTypes, double> GetArmorTypeMultipliers()
        {
            return _armorTypeMultiplier;
        }

        #endregion

        #region Game Helpers

        private static readonly int NumberOfEnemiesPerRound = 3;

        // TO BE REFACTORED. It should not use Console.Write/WriteLine directly, this is not good for testing or future UI changes
        // This may be moved to a UI Helper class in the future (Or Game class)
        public static void ChoosePhaseAfterCombat(Player player, List<BaseItem> itemsToChoose)
        {
            // Placeholder for future implementation
            Console.Write("Choose Phase: You have defeated the enemies! You can choose one of the following items:\n");
            foreach(var item in itemsToChoose)
            {
                Console.WriteLine(item.ToString());
            }

            Console.Write("Type the position (1, 2 or 3) of the item you want to choose: ");
            var choose = Console.ReadLine();

            if (int.TryParse(choose, out int selectedItem) && selectedItem >= 1 && selectedItem <= 3)
            {
                Console.WriteLine($"You selected item {selectedItem}.");
                var itemChosen = itemsToChoose[selectedItem - 1];
                
            }
            else
            {
                Console.WriteLine("Invalid selection. Please choose a valid option.");
            }
        }

        public static List<BaseBeing> GetEnemiesForLevel(int level)
        {
            List<BaseBeing> enemyList = new List<BaseBeing>();
            for (int i = 0; i < NumberOfEnemiesPerRound; i++)
            {
                enemyList.Add(Helper.CreateEnemyRandomForLevel(level));
            }
            return enemyList;
        }

        public static List<BaseItem> GetItemsForLevel(int level)
        {
            // For now, always return 1 weapon and 2 armors
            // This must be randomized later for better gameplay
            // Also this would include trinkets, consumables, etc. in the future
            return new List<BaseItem>()
            {
                CreateWeaponRandomForLevel(level),
                CreateArmorRandomForLevel(level),
                CreateArmorRandomForLevel(level),
            };
        }

        private static BaseBeing CreateEnemyRandomForLevel(int level)
        {
            int random = Random.Shared.Next(0, GameBalanceConstants.MaxEnemyTypes);

            switch (random)
            {
                case 0:
                    return new Classes.Implementation.V0.Enemy.Beast(level);
                case 1:
                    return new Classes.Implementation.V0.Enemy.Demon(level);
                case 2:
                    return new Classes.Implementation.V0.Enemy.Dragon(level);
                case 3:
                    return new Classes.Implementation.V0.Enemy.Elemental(level);
                case 4:
                    return new Classes.Implementation.V0.Enemy.Giant(level);
                case 5:
                    return new Classes.Implementation.V0.Enemy.Goblin(level);
                case 6:
                    return new Classes.Implementation.V0.Enemy.Humanoid(level);
                case 7:
                    return new Classes.Implementation.V0.Enemy.Undead(level);
                default:
                    return new Classes.Implementation.V0.Enemy.Humanoid(level);
            }
        }

        private static BaseWeapon CreateWeaponRandomForLevel(int level)
        {
            WeaponTypes _weaponType;
            DamageTypes _damageType;

            double _baseDamage = 10;
            double _weight = 1;
            double _attackSpeed = 1;

            Dictionary<DamageModifiers, double> _damageModifiers = new();

            // First, decide weapon type (Ranged or Melee)
            _weaponType = (WeaponTypes)Random.Shared.Next(0, 2);

            // Second, set weapon damage type (Physical, Magical, etc.)
            _damageType = GetRandomEnumValue<DamageTypes>();

            // Third, select type of weapon based on weapon type chosen
            MeleeWeaponTypes _meleeWeaponType = GetRandomEnumValue<MeleeWeaponTypes>();
            RangedWeaponTypes _rangedWeaponType = GetRandomEnumValue<RangedWeaponTypes>();

            // Fourth, set weapon damage modifiers based on level

            Dictionary<DamageModifiers, double> damageModifiers = new Dictionary<DamageModifiers, double>();
            if (GameBalanceConstants.Tier1MinLevel < level && level <= GameBalanceConstants.Tier1MaxLevel)
            {
                damageModifiers.AddOrSum(GetRandomEnumValue<DamageModifiers>(), Random.Shared.Next(GameBalanceConstants.Tier1MinDamage, GameBalanceConstants.Tier1MaxDamage));
            }
            else if (GameBalanceConstants.Tier2MinLevel <= level && level < GameBalanceConstants.Tier2MaxLevel)
            {
                damageModifiers.AddOrSum(GetRandomEnumValue<DamageModifiers>(), Random.Shared.Next(GameBalanceConstants.Tier2MinDamage, GameBalanceConstants.Tier2MaxDamage));
                damageModifiers.AddOrSum(GetRandomEnumValue<DamageModifiers>(), Random.Shared.Next(GameBalanceConstants.Tier2MinDamage, GameBalanceConstants.Tier2MaxDamage));
            }
            else if (GameBalanceConstants.Tier3MinLevel <= level)
            {
                damageModifiers.AddOrSum(GetRandomEnumValue<DamageModifiers>(), Random.Shared.Next(GameBalanceConstants.Tier3MinDamage, GameBalanceConstants.Tier3MaxDamage));
                damageModifiers.AddOrSum(GetRandomEnumValue<DamageModifiers>(), Random.Shared.Next(GameBalanceConstants.Tier3MinDamage, GameBalanceConstants.Tier3MaxDamage));
                damageModifiers.AddOrSum(GetRandomEnumValue<DamageModifiers>(), Random.Shared.Next(GameBalanceConstants.Tier3MinDamage, GameBalanceConstants.Tier3MaxDamage));
            }

            //Fifth , set base damage and attack speed based on weapon type and level
            switch (_weaponType)
            {
                case WeaponTypes.Melee:
                    _baseDamage = WeaponRanges.GetRandomDamage(_meleeWeaponType) + (level * 2);
                    _attackSpeed = WeaponRanges.GetRandomSpeed(_meleeWeaponType);
                    _weight = WeaponRanges.GetRandomWeight(_meleeWeaponType);
                    break;
                case WeaponTypes.Ranged:
                    _baseDamage = WeaponRanges.GetRandomDamage(_rangedWeaponType) + (level * 2);
                    _attackSpeed = WeaponRanges.GetRandomSpeed(_rangedWeaponType);
                    _weight = WeaponRanges.GetRandomWeight(_rangedWeaponType);
                    break;
            }

            // Sixth, create weapon instance
            switch (_weaponType)
            {
                case WeaponTypes.Melee:
                    var newWeapon = new MeleeWeapon(_weaponType, _baseDamage, _damageType, _attackSpeed, _weight, damageModifiers, level, $"Level {level} {_meleeWeaponType}", _meleeWeaponType);
                    newWeapon.SetName(_nameGeneratorService.GenerateName(newWeapon));
                    return newWeapon;
                case WeaponTypes.Ranged:
                    var newRangedWeapon = new RangedWeapon(_weaponType, _baseDamage, _damageType, _attackSpeed, _weight, damageModifiers, level, $"Level {level} {_rangedWeaponType}", _rangedWeaponType);
                    newRangedWeapon.SetName(_nameGeneratorService.GenerateName(newRangedWeapon));
                    return newRangedWeapon;
                default:
                    return new MeleeWeapon(_weaponType, _baseDamage, _damageType, _attackSpeed, _weight, damageModifiers, level, $"Level {level} {_meleeWeaponType}", _meleeWeaponType);
            }
        }

        private static BaseArmor CreateArmorRandomForLevel(int level)
        {
            ArmorSlots _armorSlot = GetRandomEnumValue<ArmorSlots>();
            ArmorTypes _armorType = GetRandomEnumValue<ArmorTypes>();

            Dictionary<ArmorSlots, double> _armorValuesDefault = GetDefaultArmorValues();
            Dictionary<ArmorSlots, double> _armorWeightsDefault = GetDefaultArmorWeights();
            Dictionary<ArmorTypes, double> _armorMultipliers = GetArmorTypeMultipliers();

            Dictionary<DamageModifiers, double> resistanceModifiers = new Dictionary<DamageModifiers, double>();
            if (GameBalanceConstants.Tier1MinLevel < level && level <= GameBalanceConstants.Tier1MaxLevel)
            {
                resistanceModifiers.AddOrSum(GetRandomEnumValue<DamageModifiers>(), Random.Shared.Next(GameBalanceConstants.Tier1MinDamage, GameBalanceConstants.Tier1MaxDamage));
            }
            else if (GameBalanceConstants.Tier2MinLevel <= level && level < GameBalanceConstants.Tier2MaxLevel)
            {
                resistanceModifiers.AddOrSum(GetRandomEnumValue<DamageModifiers>(), Random.Shared.Next(GameBalanceConstants.Tier2MinDamage, GameBalanceConstants.Tier2MaxDamage));
                resistanceModifiers.AddOrSum(GetRandomEnumValue<DamageModifiers>(), Random.Shared.Next(GameBalanceConstants.Tier2MinDamage, GameBalanceConstants.Tier2MaxDamage));
            }
            else if (GameBalanceConstants.Tier3MinLevel <= level)
            {
                resistanceModifiers.AddOrSum(GetRandomEnumValue<DamageModifiers>(), Random.Shared.Next(GameBalanceConstants.Tier3MinDamage, GameBalanceConstants.Tier3MaxDamage));
                resistanceModifiers.AddOrSum(GetRandomEnumValue<DamageModifiers>(), Random.Shared.Next(GameBalanceConstants.Tier3MinDamage, GameBalanceConstants.Tier3MaxDamage));
                resistanceModifiers.AddOrSum(GetRandomEnumValue<DamageModifiers>(), Random.Shared.Next(GameBalanceConstants.Tier3MinDamage, GameBalanceConstants.Tier3MaxDamage));
            }

            var newArmor = new ArmorItem(_armorValuesDefault[_armorSlot] * _armorMultipliers[_armorType], _armorSlot, _armorType, resistanceModifiers, _armorWeightsDefault[_armorSlot] * _armorMultipliers[_armorType], level, $"{_armorType} {_armorSlot} ({level})");
            newArmor.SetName(_nameGeneratorService.GenerateName(newArmor));
            return newArmor;
        }

        #endregion

        #region Random Helpers
        private static T GetRandomEnumValue<T>() where T : System.Enum
        {
            var values = System.Enum.GetValues(typeof(T));
            return (T)values.GetValue(Random.Shared.Next(values.Length))!;
        }
        #endregion
    }
}
