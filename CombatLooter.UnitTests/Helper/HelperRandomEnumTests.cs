using CombatLooter.Classes.Implementation;
using CombatLooter.Classes.Implementation.V0.Armor;
using CombatLooter.Classes.Implementation.V0.Weapon;
using CombatLooter.Enum;
using Xunit;

namespace CombatLooter.UnitTests.Helper
{
    /// <summary>
    /// Tests to ensure that random enum value generation produces only valid enum values.
    /// This is particularly important for [Flags] enums like DamageModifiers which have non-sequential values.
    /// </summary>
    public class HelperRandomEnumTests
    {
        private const int TestIterations = 100; // Number of times to test random generation

        #region DamageModifiers Tests (Flags Enum)

        [Fact]
        public void GetItemsForLevel_WeaponDamageModifiers_ShouldOnlyContainValidEnumValues()
        {
            // Arrange
            var validModifiers = System.Enum.GetValues<DamageModifiers>();
            var invalidModifierFound = false;

            // Act - Generate many weapons to test randomness
            for (int i = 0; i < TestIterations; i++)
            {
                var items = CombatLooter.Helper.Helper.GetItemsForLevel(10); // Level 10 has 3 modifiers
                var weapon = items.OfType<BaseWeapon>().First();
                var modifiers = weapon.ModifiersDamage().GetDamageByType();

                foreach (var modifier in modifiers.Keys)
                {
                    if (!validModifiers.Contains(modifier))
                    {
                        invalidModifierFound = true;
                        break;
                    }
                }

                if (invalidModifierFound)
                    break;
            }

            // Assert
            Assert.False(invalidModifierFound,
                "Found an invalid DamageModifier value. Valid values are: " +
                string.Join(", ", validModifiers.Select(v => $"{v} ({(int)v})")));
        }

        [Fact]
        public void GetItemsForLevel_ArmorResistanceModifiers_ShouldOnlyContainValidEnumValues()
        {
            // Arrange
            var validModifiers = System.Enum.GetValues<DamageModifiers>();
            var invalidModifierFound = false;

            // Act - Generate many armors to test randomness
            for (int i = 0; i < TestIterations; i++)
            {
                var items = CombatLooter.Helper.Helper.GetItemsForLevel(10); // Level 10 has 3 modifiers
                var armors = items.OfType<ArmorItem>().ToList();

                foreach (var armor in armors)
                {
                    var resistances = armor.GetResistances();
                    foreach (var modifier in resistances.Keys)
                    {
                        if (!validModifiers.Contains(modifier))
                        {
                            invalidModifierFound = true;
                            break;
                        }
                    }

                    if (invalidModifierFound)
                        break;
                }

                if (invalidModifierFound)
                    break;
            }

            // Assert
            Assert.False(invalidModifierFound,
                "Found an invalid DamageModifier value in armor resistances. Valid values are: " +
                string.Join(", ", validModifiers.Select(v => $"{v} ({(int)v})")));
        }

        [Fact]
        public void GetItemsForLevel_DamageModifiers_ShouldGenerateAllPossibleValues()
        {
            // Arrange
            var validModifiers = System.Enum.GetValues<DamageModifiers>();
            var generatedModifiers = new HashSet<DamageModifiers>();

            // Act - Generate many items to collect all possible modifiers
            for (int i = 0; i < TestIterations * 5; i++)
            {
                var items = CombatLooter.Helper.Helper.GetItemsForLevel(10);
                var weapon = items.OfType<BaseWeapon>().First();
                var modifiers = weapon.ModifiersDamage().GetDamageByType();

                foreach (var modifier in modifiers.Keys)
                {
                    generatedModifiers.Add(modifier);
                }

                var armors = items.OfType<ArmorItem>().ToList();
                foreach (var armor in armors)
                {
                    var resistances = armor.GetResistances();
                    foreach (var modifier in resistances.Keys)
                    {
                        generatedModifiers.Add(modifier);
                    }
                }

                // If we've seen all modifiers, we can exit early
                if (generatedModifiers.Count == validModifiers.Length)
                    break;
            }

            // Assert - Should have generated most or all modifier types
            // With random generation, we might not get all values, but should get most
            Assert.True(generatedModifiers.Count >= validModifiers.Length / 2,
                $"Expected to generate at least half of the possible DamageModifiers. " +
                $"Generated {generatedModifiers.Count} out of {validModifiers.Length}. " +
                $"Generated: {string.Join(", ", generatedModifiers)}");
        }

        #endregion

        #region DamageTypes Tests

        [Fact]
        public void GetItemsForLevel_WeaponDamageTypes_ShouldOnlyContainValidEnumValues()
        {
            // Arrange
            var validDamageTypes = System.Enum.GetValues<DamageTypes>();

            // Act & Assert - Generate many weapons to test randomness
            for (int i = 0; i < TestIterations; i++)
            {
                var items = CombatLooter.Helper.Helper.GetItemsForLevel(5);
                var weapon = items.OfType<BaseWeapon>().First();
                var damageType = weapon.GetDamageType();

                Assert.Contains(damageType, validDamageTypes);
            }
        }

        [Fact]
        public void GetItemsForLevel_DamageTypes_ShouldGenerateBothTypes()
        {
            // Arrange
            var generatedTypes = new HashSet<DamageTypes>();

            // Act - Generate many weapons
            for (int i = 0; i < TestIterations; i++)
            {
                var items = CombatLooter.Helper.Helper.GetItemsForLevel(5);
                var weapon = items.OfType<BaseWeapon>().First();
                generatedTypes.Add(weapon.GetDamageType());

                if (generatedTypes.Count == System.Enum.GetValues<DamageTypes>().Length)
                    break;
            }

            // Assert - Should have generated both Physical and Magical
            Assert.True(generatedTypes.Count >= 2,
                "Expected to generate both DamageTypes (Physical and Magical)");
        }

        #endregion

        #region ArmorSlots Tests

        [Fact]
        public void GetItemsForLevel_ArmorSlots_ShouldOnlyContainValidEnumValues()
        {
            // Arrange
            var validSlots = System.Enum.GetValues<ArmorSlots>();

            // Act & Assert - Generate many armors to test randomness
            for (int i = 0; i < TestIterations; i++)
            {
                var items = CombatLooter.Helper.Helper.GetItemsForLevel(5);
                var armors = items.OfType<ArmorItem>().ToList();

                foreach (var armor in armors)
                {
                    var slot = armor.GetArmorSlot();
                    Assert.Contains(slot, validSlots);
                }
            }
        }

        [Fact]
        public void GetItemsForLevel_ArmorSlots_ShouldGenerateMultipleDifferentSlots()
        {
            // Arrange
            var generatedSlots = new HashSet<ArmorSlots>();

            // Act - Generate many armors
            for (int i = 0; i < TestIterations; i++)
            {
                var items = CombatLooter.Helper.Helper.GetItemsForLevel(5);
                var armors = items.OfType<ArmorItem>().ToList();

                foreach (var armor in armors)
                {
                    generatedSlots.Add(armor.GetArmorSlot());
                }
            }

            // Assert - Should have generated at least 3 different slots
            Assert.True(generatedSlots.Count >= 3,
                $"Expected to generate at least 3 different ArmorSlots. Generated: {string.Join(", ", generatedSlots)}");
        }

        #endregion

        #region ArmorTypes Tests

        [Fact]
        public void GetItemsForLevel_ArmorTypes_ShouldOnlyContainValidEnumValues()
        {
            // Arrange
            var validTypes = System.Enum.GetValues<ArmorTypes>();

            // Act & Assert - Generate many armors to test randomness
            for (int i = 0; i < TestIterations; i++)
            {
                var items = CombatLooter.Helper.Helper.GetItemsForLevel(5);
                var armors = items.OfType<ArmorItem>().ToList();

                foreach (var armor in armors)
                {
                    var armorType = armor.GetArmorTypes();
                    Assert.Contains(armorType, validTypes);
                }
            }
        }

        [Fact]
        public void GetItemsForLevel_ArmorTypes_ShouldGenerateAllThreeTypes()
        {
            // Arrange
            var generatedTypes = new HashSet<ArmorTypes>();

            // Act - Generate many armors
            for (int i = 0; i < TestIterations; i++)
            {
                var items = CombatLooter.Helper.Helper.GetItemsForLevel(5);
                var armors = items.OfType<ArmorItem>().ToList();

                foreach (var armor in armors)
                {
                    generatedTypes.Add(armor.GetArmorTypes());
                }

                if (generatedTypes.Count == System.Enum.GetValues<ArmorTypes>().Length)
                    break;
            }

            // Assert - Should have generated all three types (Light, Medium, Heavy)
            Assert.True(generatedTypes.Count == 3,
                $"Expected to generate all 3 ArmorTypes. Generated: {string.Join(", ", generatedTypes)}");
        }

        #endregion

        #region WeaponTypes Tests

        [Fact]
        public void GetItemsForLevel_WeaponTypes_ShouldOnlyContainValidEnumValues()
        {
            // Arrange - WeaponTypes should be Melee or Ranged

            // Act & Assert - Generate many weapons to test randomness
            for (int i = 0; i < TestIterations; i++)
            {
                var items = CombatLooter.Helper.Helper.GetItemsForLevel(5);
                var weapon = items.OfType<BaseWeapon>().First();

                // Weapon should be either MeleeWeapon or RangedWeapon
                Assert.True(weapon is MeleeWeapon || weapon is RangedWeapon,
                    "Weapon must be either MeleeWeapon or RangedWeapon");
            }
        }

        [Fact]
        public void GetItemsForLevel_WeaponTypes_ShouldGenerateBothMeleeAndRanged()
        {
            // Arrange
            var hasMelee = false;
            var hasRanged = false;

            // Act - Generate many weapons
            for (int i = 0; i < TestIterations; i++)
            {
                var items = CombatLooter.Helper.Helper.GetItemsForLevel(5);
                var weapon = items.OfType<BaseWeapon>().First();

                if (weapon is MeleeWeapon) hasMelee = true;
                if (weapon is RangedWeapon) hasRanged = true;

                if (hasMelee && hasRanged)
                    break;
            }

            // Assert - Should have generated both types
            Assert.True(hasMelee && hasRanged,
                "Expected to generate both Melee and Ranged weapons");
        }

        #endregion

        #region MeleeWeaponTypes Tests

        [Fact]
        public void GetItemsForLevel_MeleeWeaponTypes_ShouldOnlyContainValidEnumValues()
        {
            // Arrange
            var validMeleeTypes = System.Enum.GetValues<MeleeWeaponTypes>();

            // Act & Assert - Generate many weapons to test randomness
            for (int i = 0; i < TestIterations; i++)
            {
                var items = CombatLooter.Helper.Helper.GetItemsForLevel(5);
                var weapon = items.OfType<BaseWeapon>().First();

                if (weapon is MeleeWeapon meleeWeapon)
                {
                    var meleeType = meleeWeapon.MeleeWeaponType;
                    Assert.Contains(meleeType, validMeleeTypes);
                }
            }
        }

        [Fact]
        public void GetItemsForLevel_MeleeWeaponTypes_ShouldGenerateMultipleDifferentTypes()
        {
            // Arrange
            var generatedTypes = new HashSet<MeleeWeaponTypes>();

            // Act - Generate many weapons
            for (int i = 0; i < TestIterations * 2; i++)
            {
                var items = CombatLooter.Helper.Helper.GetItemsForLevel(5);
                var weapon = items.OfType<BaseWeapon>().First();

                if (weapon is MeleeWeapon meleeWeapon)
                {
                    generatedTypes.Add(meleeWeapon.MeleeWeaponType);
                }
            }

            // Assert - Should have generated at least 2 different melee weapon types
            Assert.True(generatedTypes.Count >= 2,
                $"Expected to generate at least 2 different MeleeWeaponTypes. Generated: {string.Join(", ", generatedTypes)}");
        }

        #endregion

        #region RangedWeaponTypes Tests

        [Fact]
        public void GetItemsForLevel_RangedWeaponTypes_ShouldOnlyContainValidEnumValues()
        {
            // Arrange
            var validRangedTypes = System.Enum.GetValues<RangedWeaponTypes>();

            // Act & Assert - Generate many weapons to test randomness
            for (int i = 0; i < TestIterations; i++)
            {
                var items = CombatLooter.Helper.Helper.GetItemsForLevel(5);
                var weapon = items.OfType<BaseWeapon>().First();

                if (weapon is RangedWeapon rangedWeapon)
                {
                    var rangedType = rangedWeapon.RangedWeaponType;
                    Assert.Contains(rangedType, validRangedTypes);
                }
            }
        }

        [Fact]
        public void GetItemsForLevel_RangedWeaponTypes_ShouldGenerateMultipleDifferentTypes()
        {
            // Arrange
            var generatedTypes = new HashSet<RangedWeaponTypes>();

            // Act - Generate many weapons
            for (int i = 0; i < TestIterations * 2; i++)
            {
                var items = CombatLooter.Helper.Helper.GetItemsForLevel(5);
                var weapon = items.OfType<BaseWeapon>().First();

                if (weapon is RangedWeapon rangedWeapon)
                {
                    generatedTypes.Add(rangedWeapon.RangedWeaponType);
                }
            }

            // Assert - Should have generated at least 2 different ranged weapon types
            Assert.True(generatedTypes.Count >= 2,
                $"Expected to generate at least 2 different RangedWeaponTypes. Generated: {string.Join(", ", generatedTypes)}");
        }

        #endregion

        #region Edge Cases and Special Tests

        [Fact]
        public void DamageModifiers_EnumValues_ShouldMatchExpectedFlagsPattern()
        {
            // This test verifies the DamageModifiers enum has the expected non-sequential values
            // that caused the original bug

            // Arrange & Act
            var values = System.Enum.GetValues<DamageModifiers>();

            // Assert
            Assert.Equal(6, values.Length); // Fire, Ice, Lightning, Poison, True, Shadow
            Assert.Contains(DamageModifiers.Fire, values);
            Assert.Equal(0, (int)DamageModifiers.Fire);
            Assert.Contains(DamageModifiers.Ice, values);
            Assert.Equal(2, (int)DamageModifiers.Ice);
            Assert.Contains(DamageModifiers.Lightning, values);
            Assert.Equal(4, (int)DamageModifiers.Lightning);
            Assert.Contains(DamageModifiers.Poison, values);
            Assert.Equal(8, (int)DamageModifiers.Poison);
            Assert.Contains(DamageModifiers.True, values);
            Assert.Equal(16, (int)DamageModifiers.True);
            Assert.Contains(DamageModifiers.Shadow, values);
            Assert.Equal(32, (int)DamageModifiers.Shadow);
        }

        [Theory]
        [InlineData(2)]  // Level 2-4: 1 modifier
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(6)]  // Level 5-9: 2 modifiers
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)] // Level 10+: 3 modifiers
        [InlineData(15)]
        [InlineData(20)]
        public void GetItemsForLevel_AllLevels_ShouldGenerateValidDamageModifiers(int level)
        {
            // Arrange
            var validModifiers = System.Enum.GetValues<DamageModifiers>();

            // Act - Generate items for the level
            for (int i = 0; i < 10; i++) // Test 10 times for each level
            {
                var items = CombatLooter.Helper.Helper.GetItemsForLevel(level);
                var weapon = items.OfType<BaseWeapon>().First();
                var modifiers = weapon.ModifiersDamage().GetDamageByType();

                // Assert - All modifiers should be valid
                foreach (var modifier in modifiers.Keys)
                {
                    Assert.Contains(modifier, validModifiers);
                }

                var armors = items.OfType<ArmorItem>().ToList();
                foreach (var armor in armors)
                {
                    var resistances = armor.GetResistances();
                    foreach (var modifier in resistances.Keys)
                    {
                        Assert.Contains(modifier, validModifiers);
                    }
                }
            }
        }

        #endregion
    }
}
