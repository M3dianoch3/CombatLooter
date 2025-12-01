using CombatLooter.Classes.Implementation;
using CombatLooter.Classes.Implementation.V0.Armor;
using CombatLooter.Classes.Implementation.V0.Player;
using CombatLooter.Classes.Implementation.V0.Weapon;
using CombatLooter.Enum;
using CombatLooter.Extensions;
using CombatLooter.Helper;
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
                List<BaseBeing> enemiesCombat = CombatLooter.Helper.Helper.GetEnemiesForLevel(player.Level);
                
                // Invoke the combat service to run a combat
                var combat = new Combat(player, enemiesCombat, _logger);
                playerAlive = combat.RunCombat(logger: msg => _logger.LogInformation(msg));

                if (!playerAlive) break;

                // Increase player's level in 1 (for now), 1 combat won = 1 level
                player.IncreaseLevel_basedOnWeapon();

                // After combat, if the player is still alive, create loot (based on enemies?) and let the player choose equipment


                // For now, we'll just simulate the end of combat
                playerAlive = false; // Placeholder to exit loop
            }
            _logger.LogInformation("Game over. The player has died.");
        }
    }
}
