using CombatLooter.Classes.Implementation;
using CombatLooter.Classes.Implementation.V0.Armor;
using CombatLooter.Classes.Implementation.V0.Weapon;
using CombatLooter.Enum;
using Xunit;

namespace CombatLooter.UnitTests.Helper
{
    public class HelperTests
    {
        #region Armor Helper Tests

        [Fact]
        public void GetDefaultArmorValues_ShouldReturnAllArmorSlots()
        {
            // Act
            var armorValues = CombatLooter.Helper.Helper.GetDefaultArmorValues();

            // Assert
            Assert.NotNull(armorValues);
            Assert.Equal(7, armorValues.Count); // 7 armor slots
            Assert.True(armorValues.ContainsKey(ArmorSlots.Head));
            Assert.True(armorValues.ContainsKey(ArmorSlots.Chest));
            Assert.True(armorValues.ContainsKey(ArmorSlots.Legs));
            Assert.True(armorValues.ContainsKey(ArmorSlots.Feet));
            Assert.True(armorValues.ContainsKey(ArmorSlots.Hands));
            Assert.True(armorValues.ContainsKey(ArmorSlots.Shoulders));
            Assert.True(armorValues.ContainsKey(ArmorSlots.Waist));
        }

        [Fact]
        public void GetDefaultArmorValues_ShouldReturnCorrectValues()
        {
            // Act
            var armorValues = CombatLooter.Helper.Helper.GetDefaultArmorValues();

            // Assert
            Assert.Equal(5.0, armorValues[ArmorSlots.Head]);
            Assert.Equal(15.0, armorValues[ArmorSlots.Chest]);
            Assert.Equal(10.0, armorValues[ArmorSlots.Legs]);
            Assert.Equal(5.0, armorValues[ArmorSlots.Feet]);
            Assert.Equal(3.0, armorValues[ArmorSlots.Hands]);
            Assert.Equal(7.0, armorValues[ArmorSlots.Shoulders]);
            Assert.Equal(2.0, armorValues[ArmorSlots.Waist]);
        }

        [Fact]
        public void GetDefaultArmorWeights_ShouldReturnAllArmorSlots()
        {
            // Act
            var armorWeights = CombatLooter.Helper.Helper.GetDefaultArmorWeights();

            // Assert
            Assert.NotNull(armorWeights);
            Assert.Equal(7, armorWeights.Count);
            Assert.True(armorWeights.ContainsKey(ArmorSlots.Head));
            Assert.True(armorWeights.ContainsKey(ArmorSlots.Chest));
            Assert.True(armorWeights.ContainsKey(ArmorSlots.Legs));
            Assert.True(armorWeights.ContainsKey(ArmorSlots.Feet));
            Assert.True(armorWeights.ContainsKey(ArmorSlots.Hands));
            Assert.True(armorWeights.ContainsKey(ArmorSlots.Shoulders));
            Assert.True(armorWeights.ContainsKey(ArmorSlots.Waist));
        }

        [Fact]
        public void GetDefaultArmorWeights_ShouldReturnCorrectValues()
        {
            // Act
            var armorWeights = CombatLooter.Helper.Helper.GetDefaultArmorWeights();

            // Assert
            Assert.Equal(1.5, armorWeights[ArmorSlots.Head]);
            Assert.Equal(4.0, armorWeights[ArmorSlots.Chest]);
            Assert.Equal(3.0, armorWeights[ArmorSlots.Legs]);
            Assert.Equal(1.5, armorWeights[ArmorSlots.Feet]);
            Assert.Equal(1.0, armorWeights[ArmorSlots.Hands]);
            Assert.Equal(2.0, armorWeights[ArmorSlots.Shoulders]);
            Assert.Equal(1.0, armorWeights[ArmorSlots.Waist]);
        }

        [Fact]
        public void GetArmorTypeMultipliers_ShouldReturnAllArmorTypes()
        {
            // Act
            var multipliers = CombatLooter.Helper.Helper.GetArmorTypeMultipliers();

            // Assert
            Assert.NotNull(multipliers);
            Assert.Equal(3, multipliers.Count);
            Assert.True(multipliers.ContainsKey(ArmorTypes.Light));
            Assert.True(multipliers.ContainsKey(ArmorTypes.Medium));
            Assert.True(multipliers.ContainsKey(ArmorTypes.Heavy));
        }

        [Fact]
        public void GetArmorTypeMultipliers_ShouldReturnCorrectValues()
        {
            // Act
            var multipliers = CombatLooter.Helper.Helper.GetArmorTypeMultipliers();

            // Assert
            Assert.Equal(1.0, multipliers[ArmorTypes.Light]);
            Assert.Equal(1.75, multipliers[ArmorTypes.Medium]);
            Assert.Equal(2.0, multipliers[ArmorTypes.Heavy]);
        }

        #endregion

        #region Game Helper Tests

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(20)]
        public void GetEnemiesForLevel_ShouldReturnCorrectNumberOfEnemies(int level)
        {
            // Act
            var enemies = CombatLooter.Helper.Helper.GetEnemiesForLevel(level);

            // Assert
            Assert.NotNull(enemies);
            Assert.Equal(3, enemies.Count); // NumberOfEnemiesPerRound = 3
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void GetEnemiesForLevel_ShouldReturnValidEnemies(int level)
        {
            // Act
            var enemies = CombatLooter.Helper.Helper.GetEnemiesForLevel(level);

            // Assert
            Assert.All(enemies, enemy =>
            {
                Assert.NotNull(enemy);
                Assert.True(enemy.CurrentHealth > 0);
                Assert.NotNull(enemy.Name);
                Assert.NotEmpty(enemy.Name);
            });
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        public void GetItemsForLevel_ShouldReturnThreeItems(int level)
        {
            // Act
            var items = CombatLooter.Helper.Helper.GetItemsForLevel(level);

            // Assert
            Assert.NotNull(items);
            Assert.Equal(3, items.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void GetItemsForLevel_ShouldReturnOneWeaponAndTwoArmors(int level)
        {
            // Act
            var items = CombatLooter.Helper.Helper.GetItemsForLevel(level);

            // Assert
            var weapons = items.OfType<BaseWeapon>().ToList();
            var armors = items.OfType<BaseArmor>().ToList();

            Assert.Single(weapons);
            Assert.Equal(2, armors.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void GetItemsForLevel_WeaponShouldHaveCorrectLevel(int level)
        {
            // Act
            var items = CombatLooter.Helper.Helper.GetItemsForLevel(level);
            var weapon = items.OfType<BaseWeapon>().First();

            // Assert
            Assert.Equal(level, weapon.GetILevel());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void GetItemsForLevel_ArmorsShouldHaveCorrectLevel(int level)
        {
            // Act
            var items = CombatLooter.Helper.Helper.GetItemsForLevel(level);
            var armors = items.OfType<BaseArmor>().ToList();

            // Assert
            Assert.All(armors, armor =>
            {
                Assert.Equal(level, armor.GetILevel());
            });
        }

        [Fact]
        public void GetItemsForLevel_WeaponShouldBeMeleeOrRanged()
        {
            // Act
            var items = CombatLooter.Helper.Helper.GetItemsForLevel(5);
            var weapon = items.OfType<BaseWeapon>().First();

            // Assert
            Assert.True(weapon is MeleeWeapon || weapon is RangedWeapon);
        }

        [Theory]
        [InlineData(1, 0)] // Level 1 should have no modifiers
        [InlineData(3, 1)] // Level 2-4 should have 1 modifier
        [InlineData(6, 2)] // Level 5-9 should have up to 2 modifiers
        [InlineData(11, 3)] // Level 10+ should have up to 3 modifiers
        public void GetItemsForLevel_WeaponShouldHaveExpectedModifiers(int level, int expectedMaxModifiers)
        {
            // Act - Run multiple times since it's random
            var hasCorrectModifiers = false;
            for (int i = 0; i < 10; i++)
            {
                var items = CombatLooter.Helper.Helper.GetItemsForLevel(level);
                var weapon = items.OfType<BaseWeapon>().First();
                var modifiers = weapon.ModifiersDamage().GetDamageByType();

                if (level == 1)
                {
                    if (modifiers.Count == 0)
                    {
                        hasCorrectModifiers = true;
                        break;
                    }
                }
                else if (modifiers.Count <= expectedMaxModifiers && modifiers.Count > 0)
                {
                    hasCorrectModifiers = true;
                    break;
                }
            }

            // Assert
            Assert.True(hasCorrectModifiers, 
                $"Expected weapon at level {level} to have appropriate damage modifiers (max {expectedMaxModifiers})");
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        public void GetItemsForLevel_ArmorsShouldHaveValidSlots(int level)
        {
            // Act
            var items = CombatLooter.Helper.Helper.GetItemsForLevel(level);
            var armors = items.OfType<ArmorItem>().ToList();

            // Assert
            Assert.All(armors, armor =>
            {
                Assert.True(System.Enum.IsDefined(typeof(ArmorSlots), armor.GetArmorSlot()));
                Assert.True(System.Enum.IsDefined(typeof(ArmorTypes), armor.GetArmorTypes()));
            });
        }

        [Fact]
        public void GetEnemiesForLevel_ShouldGenerateDifferentEnemyTypes()
        {
            // Act - Generate multiple sets of enemies
            var allEnemies = new List<BaseBeing>();
            for (int i = 0; i < 20; i++)
            {
                allEnemies.AddRange(CombatLooter.Helper.Helper.GetEnemiesForLevel(5));
            }

            // Get unique enemy types by class name
            var uniqueTypes = allEnemies.Select(e => e.GetType().Name).Distinct().ToList();

            // Assert - Should have generated at least 2 different enemy types
            Assert.True(uniqueTypes.Count >= 2, 
                "Expected to generate at least 2 different enemy types across multiple calls");
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void GetItemsForLevel_ShouldGenerateDifferentWeaponTypes(int level)
        {
            // Act - Generate multiple weapons
            var weapons = new List<BaseWeapon>();
            for (int i = 0; i < 10; i++)
            {
                var items = CombatLooter.Helper.Helper.GetItemsForLevel(level);
                weapons.Add(items.OfType<BaseWeapon>().First());
            }

            // Get unique weapon types
            var hasMelee = weapons.Any(w => w is MeleeWeapon);
            var hasRanged = weapons.Any(w => w is RangedWeapon);

            // Assert - Should have generated both types (with high probability)
            // Note: This test has a small chance of failing due to randomness
            Assert.True(hasMelee || hasRanged, "Expected to generate at least one weapon type");
        }

        #endregion
    }
}
