using CombatLooter.Classes.Implementation.V0.Armor;
using CombatLooter.Classes.Implementation.V0.Weapon;
using CombatLooter.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

class Program
{
    static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {NewLine}{Exception}")
            .WriteTo.File("logs/combatlooter.log", rollingInterval: RollingInterval.Day,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .Enrich.FromLogContext()
            .CreateLogger();
        var app = new Application(
            new LoggerFactory().CreateLogger<Application>(),
            new ServiceCollection().BuildServiceProvider()
        );
        app.Run(args);
    }

    public class Application
    {
        private readonly ILogger<Application> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Application(ILogger<Application> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public void Run(string[] args)
        {
            _logger.LogInformation("Combat Looter started!");

            try
            {
                // Create a run
                // Run is an infinite loop of combats until player dies
                // After each combat, the player can select an item to equip

                #region Test // To be removed later
                // Test
                var damageTypes = new Dictionary<CombatLooter.Enum.DamageModifiers, double>()
                {
                    { CombatLooter.Enum.DamageModifiers.Fire, 5.0 },
                    { CombatLooter.Enum.DamageModifiers.Ice, 3.0 }
                };
                var weapon = new MeleeWeapon(CombatLooter.Enum.WeaponTypes.Melee, 20.0, CombatLooter.Enum.DamageTypes.Physical, 2.5, 2.0, damageTypes, 10, "The fucking greater sword", CombatLooter.Enum.MeleeWeaponTypes.Sword);
                Console.WriteLine($"Weapon created: \n{weapon.ToString()}");

                var armor = new ArmorItem(15, CombatLooter.Enum.ArmorSlots.Chest, CombatLooter.Enum.ArmorTypes.Heavy, damageTypes, 15.0, 5, "The Fucking Greater Armor");
                Console.WriteLine($"Armor created: \n{armor.ToString()}");
                #endregion
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
            }
        }
    }
}
