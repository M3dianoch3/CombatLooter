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
            // Initialize player, enemies, and other game state here
            // Loop through combats until the player dies
            bool playerAlive = true;
            while (playerAlive)
            {
                _logger.LogInformation("Starting a new combat...");
                // Here you would invoke the combat service to run a combat
                // For example:
                // playerAlive = _combatService.RunCombat(logger: msg => _logger.LogInformation(msg));
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

        private static BaseBeing CreateEnemyRandomForLevel(int level)
        {
            return null!;
        }

        private static BaseWeapon CreateWeaponRandomForLevel(int level)
        {
            return null!;
        }
    }
}
