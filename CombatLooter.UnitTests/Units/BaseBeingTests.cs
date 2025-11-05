using CombatLooter.Classes.Implementation;
using CombatLooter.Enum;

namespace CombatLooter.UnitTests.Units
{
    public class BaseBeingTests
    {
        private class TestBeing : BaseBeing
        {
            // No additional implementation needed - just makes the abstract class instantiable
            public TestBeing() : base()
            {
            }
            public TestBeing(double health, double mana, string name, double armor, int stamina, int strength, int intelligence, int dexterity, Dictionary<DamageModifiers, double> resistances, BaseWeapon? weapon, int level, BeingClass beingClass) : base(health, mana, name, armor, stamina, strength, intelligence, dexterity, resistances, weapon, level, beingClass)
            {
            }
        }

        [Fact]
        public void DefaultConstructor_ShouldInitializePropertiesCorrectly()
        {
            // Arrange & Act
            var being = new TestBeing();

            // Assert
            Assert.Equal("Unnamed Being", being.Name);
            Assert.Equal(100, being.CurrentHealth);
            Assert.Equal(100, being.CurrentMana);
            Assert.Empty(being.Resistances);
            Assert.Null(being.EquippedWeapon);
            Assert.Equal(10, being.GetAmountAttack());
        }

        [Fact]
        public void TakeDamage_ShouldReduceHealth()
        {
            // Arrange
            var being = new TestBeing();

            // Act
            var isDead = being.TakeDamage(30, new Dictionary<DamageModifiers, double>());

            // Assert
            Assert.Equal(70, being.CurrentHealth);
            Assert.False(isDead);
        }

        [Fact]
        public void TakeDamage_ShouldNotReduceHealthBelowZero()
        {
            // Arrange
            var being = new TestBeing();

            // Act
            var isDead = being.TakeDamage(150, new Dictionary<DamageModifiers, double>());

            // Assert
            Assert.Equal(0, being.CurrentHealth);
            Assert.True(isDead);
        }

        [Fact]
        public void TakeDamage_ShouldNotHealWhenAmountIsNegative()
        {
            // Arrange
            var being = new TestBeing();

            // Act
            var ex = Assert.Throws<ArgumentException>(() => being.TakeDamage(-1, new Dictionary<DamageModifiers, double>()));

            // Assert
            Assert.Equal("amountAttack", ex.ParamName);
        }

        [Theory]
        [InlineData(30)] //Healed 30
        [InlineData(130)] //Healed 50 (max health)
        public void Heal_ShouldIncreaseHealth(double amountToHeal)
        {
            // Arrange
            var being = new TestBeing();
            being.TakeDamage(50, new Dictionary<DamageModifiers, double>());
            var healthBeforeHeal = being.CurrentHealth;
            // Act
            var healed = being.Heal(amountToHeal);

            // Assert
            Assert.Equal(healed, (being.CurrentHealth - healthBeforeHeal));
        }

        [Fact]
        public void Heal_ShouldNotDamage_WhenAmountIsNegative()
        {
            // Arrange
            var being = new TestBeing();

            // Act
            var ex = Assert.Throws<ArgumentException>(() => being.Heal(-5));

            // Assert
            Assert.Equal("amount", ex.ParamName);
        }

        // Replaced single-case test with a Theory that covers multiple scenarios.
        // Each scenario provides:
        // - baseDamage
        // - resistances dictionary
        // - incoming damage modifiers dictionary
        // - expected remaining health
        // - expected isDead flag
        [Theory]
        [MemberData(nameof(GetDamageScenarios))]
        public void TakeDamage_WithVariousModifiersAndResistances_ShouldCalculateDamageCorrectly(double baseDamage, Dictionary<DamageModifiers, double> resistances, Dictionary<DamageModifiers, double> damageModifiers, double expectedRemainingHealth, bool expectedIsDead)
        {
            // Arrange
            var being = new TestBeing(100, 100, "Test Being", 0, 10, 10, 10, 10, resistances, null, 1, BeingClass.Humanoid);

            // Act
            var isDead = being.TakeDamage(baseDamage, damageModifiers);

            // Assert
            Assert.Equal(expectedRemainingHealth, being.CurrentHealth);
            Assert.Equal(expectedIsDead, isDead);
        }

        public static IEnumerable<object[]> GetDamageScenarios()
        {
            // Scenario 1: Mixed modifiers with partial resistances (same as original test)
            var resistances1 = new Dictionary<DamageModifiers, double>
            {
                { DamageModifiers.Fire, 20.0 },
                { DamageModifiers.Ice, 10.0 }
            };
            var modifiers1 = new Dictionary<DamageModifiers, double>
            {
                { DamageModifiers.Fire, 50.0 },      // after 20% resist -> 40
                { DamageModifiers.Ice, 30.0 },       // after 10% resist -> 27
                { DamageModifiers.Lightning, 20.0 }  // no resist -> 20
            };
            // total damage = 10 + 40 + 27 + 20 = 97 -> remaining health = 3
            yield return new object[] { 10.0, resistances1, modifiers1, 3.0, false };

            // Scenario 2: Huge base damage with no modifiers -> should die
            var resistances2 = new Dictionary<DamageModifiers, double>();
            var modifiers2 = new Dictionary<DamageModifiers, double>();
            // total damage = 200 -> remaining health = 0
            yield return new object[] { 200.0, resistances2, modifiers2, 0.0, true };

            // Scenario 3: Some modifiers fully mitigated by resistances
            var resistances3 = new Dictionary<DamageModifiers, double>
            {
                { DamageModifiers.Fire, 100.0 },      // immune to fire
                { DamageModifiers.Lightning, 50.0 }   // half damage from lightning
            };
            var modifiers3 = new Dictionary<DamageModifiers, double>
            {
                { DamageModifiers.Fire, 50.0 },       // after 100% resist -> 0
                { DamageModifiers.Lightning, 40.0 }   // after 50% resist -> 20
            };
            // total damage = 20 (base) + 0 + 20 = 40 -> remaining health = 60
            yield return new object[] { 20.0, resistances3, modifiers3, 60.0, false };
        }
    }
}
