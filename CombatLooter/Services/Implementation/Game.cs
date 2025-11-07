using CombatLooter.Classes.Implementation;
using Microsoft.Extensions.Logging;

namespace CombatLooter.Services.Implementation
{
    /// <summary>
    /// Game class controls the overall game flow. Combats, player progression, and item management will be handled here.
    /// </summary>
    public class Game
    {
        private readonly ILogger<Game> _logger;

        public Game(ILogger<Game> logger)
        {
            _logger = logger;
        }

        public void StartNewRun()
        {
            _logger.LogInformation("Starting a new run...");
            // Initialize player
            // Loop through combats until the player dies
            bool playerAlive = true;
            while (playerAlive)
            {
                _logger.LogInformation("Starting a new combat...");
                // Create enemies based on Player level
                // Here you would invoke the combat service to run a combat
                // For example:
                // playerAlive = _combatService.RunCombat(logger: msg => _logger.LogInformation(msg));
                // After combat, if the player is still alive, create loot (based on enemies?) and let the player choose equipment
                // Increase player's level in 1 (for now), 1 combat won = 1 level
                // For now, we'll just simulate the end of combat
                playerAlive = false; // Placeholder to exit loop
            }
            _logger.LogInformation("Game over. The player has died.");
        }
    }

    static class Helper
    { 
        static List<BaseBeing> GetEnemiesForLevel(int level)
        {
            return new List<BaseBeing>();
        }

        static List<BaseWeapon> GetWeaponsForLevel(int level)
        {
            return new List<BaseWeapon>();
        }

        private static BaseBeing CreateEnemyRandomForLevel(int level)
        {
            int random = Random.Shared.Next(0, 7);

            switch (random)
            {
                case 0:
                    return new Classes.Implementation.V0.Enemy.Beast();
                case 1:
                    return new Classes.Implementation.V0.Enemy.Demon();
                case 2:
                    return new Classes.Implementation.V0.Enemy.Dragon();
                case 3:
                    return new Classes.Implementation.V0.Enemy.Elemental();
                case 4:
                    return new Classes.Implementation.V0.Enemy.Giant();
                case 5:
                    return new Classes.Implementation.V0.Enemy.Goblin();
                case 6:
                    return new Classes.Implementation.V0.Enemy.Humanoid();
                case 7:
                    return new Classes.Implementation.V0.Enemy.Undead();
                default:
                    return new Classes.Implementation.V0.Enemy.Humanoid();
            }
        }

        private static BaseWeapon CreateWeaponRandomForLevel(int level)
        {
            return null!;
        }
    }
}
