using CombatLooter.Classes.Implementation;
using CombatLooter.Classes.Implementation.V0.Player;
using CombatLooter.Classes.Implementation.V0.Weapon;
using CombatLooter.Enum;

namespace CombatLooter.UnitTests.Armor
{
    public class ArmorUnitTest
    {
        [Fact]
        public void TestArmorCreation()
        {
            // Arrange
            double initialArmorValue = 50.0;
            var armorSlot = ArmorSlots.Chest;
            var armorType = ArmorTypes.Heavy;
            int ilevel = 10;
            string name = "Test Chest Armor";
            // Act
            var armor = new BaseArmor(initialArmorValue, armorSlot, armorType, ilevel, name);
            // Assert
            Assert.Equal(initialArmorValue, armor.GetArmorValue());
            Assert.Equal(armorSlot, armor.GetArmorSlot());
            Assert.Equal(armorType, armor.GetArmorTypes());
            Assert.Equal(ilevel, armor.GetILevel());
            Assert.Equal(name, armor.GetName());
        }

        [Fact]
        public void WhenGetArmorFromPlayer_ThenReturnCorrectArmorClass()
        {
            // Arrange
            var player = new Player(100, 100, "Player test", 25, 10, 10, 10, 10, new Dictionary<DamageModifiers, double>(), new MeleeWeapon(MeleeWeaponTypes.Axe), 1, BeingClass.Humanoid);
            var headArmor = new BaseArmor(15.0, ArmorSlots.Head, ArmorTypes.Light, 5, "Test Head Armor");

            player.Head = headArmor;

            // Act
            var armorRetrieved = player.Head;

            // Assert
            Assert.NotNull(armorRetrieved);
            Assert.IsType<BaseArmor>(armorRetrieved);
            Assert.Equal(ArmorSlots.Head, armorRetrieved.GetArmorSlot());
        }

        [Fact]
        public void WhenSetArmorToPlayer_ThenPreviousArmorIsReplaced()
        {
            // Arrange
            var player = new Player(100, 100, "Player test", 25, 10, 10, 10, 10, new Dictionary<DamageModifiers, double>(), new MeleeWeapon(MeleeWeaponTypes.Axe), 1, BeingClass.Humanoid);
            var firstHeadArmor = new BaseArmor(15.0, ArmorSlots.Head, ArmorTypes.Light, 5, "First Head Armor");
            var secondHeadArmor = new BaseArmor(25.0, ArmorSlots.Head, ArmorTypes.Medium, 8, "Second Head Armor");
            
            // Act
            player.Head = firstHeadArmor;
            var armorAfterFirstSet = player.Head;
            player.Head = secondHeadArmor;
            var armorAfterSecondSet = player.Head;
            
            // Assert
            Assert.Equal(firstHeadArmor, armorAfterFirstSet);
            Assert.Equal(secondHeadArmor, armorAfterSecondSet);
            Assert.NotEqual(armorAfterFirstSet, armorAfterSecondSet);
        }
    }
}
