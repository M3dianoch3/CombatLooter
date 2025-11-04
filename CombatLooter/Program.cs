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


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
            }
        }
    }
}
