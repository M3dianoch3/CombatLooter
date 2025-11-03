using CombatLooter.Classes.Implementation;
using CombatLooter.Enum;

namespace CombatLooter.UnitTests.Weapon
{
    /// <summary>
    /// Weapon tests.
    /// </summary>
    public class WeaponUnitTest
    {
        // Test-only concrete implementation of BaseWeapon
        private class TestWeapon : BaseWeapon
        {
            // No additional implementation needed - just makes the abstract class instantiable
        }

        [Fact]
        public void TotalDamage_WithSingleModifier_CalculatesCorrectly()
        {
            // Arrange
            var weapon = new TestWeapon();
            weapon.AddDamageModifier(DamageModifiers.Fire, 25.0);

            // Act
            var result = weapon.ModifiersDamage();

            // Assert
            Assert.Equal(25.0, result.Total);
            Assert.Equal(25.0, result.GetDamageByType()[DamageModifiers.Fire]);
        }

        [Fact]
        public void TotalDamage_WithMultipleModifiers_CalculatesCorrectly()
        {
            // Arrange
            var weapon = new TestWeapon();
            weapon.AddDamageModifier(DamageModifiers.Fire, 25.0);
            weapon.AddDamageModifier(DamageModifiers.Ice, 30.0);
            weapon.AddDamageModifier(DamageModifiers.Lightning, 15.0);

            // Act
            var result = weapon.ModifiersDamage();

            // Assert
            Assert.Equal(70.0, result.Total);
            Assert.Equal(25.0, result.GetDamageByType()[DamageModifiers.Fire]);
            Assert.Equal(30.0, result.GetDamageByType()[DamageModifiers.Ice]);
            Assert.Equal(15.0, result.GetDamageByType()[DamageModifiers.Lightning]);
        }

        [Fact]
        public void TotalDamage_WithIncrementedModifier_CalculatesCorrectly()
        {
            // Arrange
            var weapon = new TestWeapon();
            weapon.AddDamageModifier(DamageModifiers.Fire, 25.0);
            weapon.AddDamageModifier(DamageModifiers.Fire, 10.0); // Increment existing

            // Act
            var result = weapon.ModifiersDamage();

            // Assert
            Assert.Equal(35.0, result.Total);
            Assert.Equal(35.0, result.GetDamageByType()[DamageModifiers.Fire]);
        }

        [Fact]
        public void TotalDamage_WithNoModifiers_ReturnsZero()
        {
            // Arrange
            var weapon = new TestWeapon();

            // Act
            var result = weapon.ModifiersDamage();

            // Assert
            Assert.Equal(0.0, result.Total);
            Assert.Empty(result.GetDamageByType());
        }
    }
}
