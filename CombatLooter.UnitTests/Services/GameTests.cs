using CombatLooter.Events.Implementation;
using CombatLooter.Services.Game.Implementation;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CombatLooter.UnitTests.Services
{
    public class GameTests
    {
        [Fact]
        public void StartNewRun_ShouldRaiseCombatEvents()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<GameService>>();
            var game = new GameService(loggerMock.Object);

            var combatStartedEvents = new List<CombatStartedEventArgs>();
            var combatEndedEvents = new List<CombatEndedEventArgs>();

            game.OnCombatStarted += (sender, args) => combatStartedEvents.Add(args);
            game.OnCombatEnded += (sender, args) => combatEndedEvents.Add(args);

            // Act
            game.StartNewRun();

            // Assert
            Assert.NotEmpty(combatStartedEvents); // Ensure combat started events were raised
            Assert.NotEmpty(combatEndedEvents);   // Ensure combat ended events were raised
            Assert.True(combatEndedEvents.All(e => e.PlayerVictory == false)); // Placeholder: Player dies in the current logic
        }

        [Fact]
        public void StartNewRun_ShouldRaiseOnDamageEvent()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<GameService>>();
            var game = new GameService(loggerMock.Object);

            var damageEvents = new List<DamageEventArgs>();
            game.OnDamage += (sender, args) => damageEvents.Add(args);

            // Act
            game.StartNewRun();

            // Assert
            Assert.NotEmpty(damageEvents); // Ensure at least one damage event was raised
            Assert.Contains(damageEvents, e => e.AttackerName == "Hero");
        }
    }
}
