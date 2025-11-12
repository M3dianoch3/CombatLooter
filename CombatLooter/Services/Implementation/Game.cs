using CombatLooter.Classes.Implementation;
using CombatLooter.Classes.Implementation.V0.Player;
using CombatLooter.Classes.Implementation.V0.Weapon;
using CombatLooter.Enum;
using Microsoft.Extensions.Logging;

namespace CombatLooter.Services.Implementation
{
    /// <summary>
    /// Game class controls the overall game flow. Combats, player progression, and item management will be handled here.
    /// </summary>
    public class Game
    {
        #region Initial Values for player
        private const double InitialHealth = 100.0;
        private const double InitialMana = 50.0;
        private const string InitialName = "Hero";
        private const double InitialArmor = 5.0;
        private const int InitialStat = 10;

        private static MeleeWeapon InitialWeapon = new(Enum.WeaponTypes.Melee, 10, Enum.DamageTypes.Physical, 2, 2, [], 1, "Wooden Sword", Enum.MeleeWeaponTypes.Sword);
        #endregion

        private readonly ILogger<Game> _logger;
        private Player player;

        public Game(ILogger<Game> logger)
        {
            _logger = logger;
            player = new Player(InitialHealth, InitialMana, InitialName, InitialArmor, InitialStat, InitialStat, InitialStat, InitialStat, new Dictionary<Enum.DamageModifiers, double>(), InitialWeapon, 1, Enum.BeingClass.Humanoid); ;
        }

        public void StartNewRun()
        {
            _logger.LogInformation("Starting a new run...");
            // Initialize player
            //player = new Player(InitialHealth, InitialMana, InitialName, InitialArmor, InitialStat, InitialStat, InitialStat, InitialStat, new Dictionary<Enum.DamageModifiers, double>(), InitialWeapon, 1, Enum.BeingClass.Humanoid);

            // Loop through combats until the player dies
            bool playerAlive = true;
            while (playerAlive)
            {
                _logger.LogInformation("Starting a new combat...");
                // Create enemies based on Player level
                List<BaseBeing> enemiesCombat = Helper.GetEnemiesForLevel(player.Level);
                
                // Invoke the combat service to run a combat
                var combat = new Combat(player, enemiesCombat, _logger);
                playerAlive = combat.RunCombat(logger: msg => _logger.LogInformation(msg));

                if (!playerAlive) break;

                // After combat, if the player is still alive, create loot (based on enemies?) and let the player choose equipment

                // Increase player's level in 1 (for now), 1 combat won = 1 level
                player.IncreaseLevel_basedOnWeapon();
                // For now, we'll just simulate the end of combat
                playerAlive = false; // Placeholder to exit loop
            }
            _logger.LogInformation("Game over. The player has died.");
        }
    }

    static class Helper
    { 
        public static List<BaseBeing> GetEnemiesForLevel(int level)
        {
            List<BaseBeing> enemyList = new List<BaseBeing>();
            for (int i = 0; i < 3; i++)
            {
                enemyList.Add(Helper.CreateEnemyRandomForLevel(level));
            }
            return enemyList;
        }

        public static List<BaseWeapon> GetWeaponsForLevel(int level)
        {
            return new List<BaseWeapon>();
        }

        private static BaseBeing CreateEnemyRandomForLevel(int level)
        {
            int random = Random.Shared.Next(0, 8);

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
            _damageType = (DamageTypes)Random.Shared.Next(0, System.Enum.GetValues<DamageTypes>().Length);

            // Third, select type of weapon based on weapon type chosen
            MeleeWeaponTypes _meleeWeaponType = (MeleeWeaponTypes)Random.Shared.Next(System.Enum.GetValues<MeleeWeaponTypes>().Length);
            RangedWeaponTypes _rangedWeaponType = (RangedWeaponTypes)Random.Shared.Next(System.Enum.GetValues<RangedWeaponTypes>().Length);

            // Fourth, set weapon damage modifiers based on level

            Dictionary<DamageModifiers, double> damageModifiers = new Dictionary<DamageModifiers, double>();
            if (1 < level && level < 5) //level 2-4
            {
                damageModifiers.Add((DamageModifiers)Random.Shared.Next(0, System.Enum.GetValues<DamageModifiers>().Length), Random.Shared.Next(1, 6));
            }
            else if (5 <= level && level < 10) //level 5-9
            {
                damageModifiers.Add((DamageModifiers)Random.Shared.Next(0, System.Enum.GetValues<DamageModifiers>().Length), Random.Shared.Next(5, 11));
                damageModifiers.Add((DamageModifiers)Random.Shared.Next(0, System.Enum.GetValues<DamageModifiers>().Length), Random.Shared.Next(5, 11));
            }
            else if (10 <= level) //level 10+
            {
                damageModifiers.Add((DamageModifiers)Random.Shared.Next(0, System.Enum.GetValues<DamageModifiers>().Length), Random.Shared.Next(8, 20));
                damageModifiers.Add((DamageModifiers)Random.Shared.Next(0, System.Enum.GetValues<DamageModifiers>().Length), Random.Shared.Next(8, 20));
                damageModifiers.Add((DamageModifiers)Random.Shared.Next(0, System.Enum.GetValues<DamageModifiers>().Length), Random.Shared.Next(8, 20));
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
                    return new MeleeWeapon(_weaponType, _baseDamage, _damageType, _attackSpeed, _weight, damageModifiers, level, $"Level {level} {_meleeWeaponType}", _meleeWeaponType);
                case WeaponTypes.Ranged:
                    return new RangedWeapon(_weaponType, _baseDamage, _damageType, _attackSpeed, _weight, damageModifiers, level, $"Level {level} {_rangedWeaponType}", _rangedWeaponType);
                default:
                    return new MeleeWeapon(_weaponType, _baseDamage, _damageType, _attackSpeed, _weight, damageModifiers, level, $"Level {level} {_meleeWeaponType}", _meleeWeaponType);
            }
        }
    }
}
