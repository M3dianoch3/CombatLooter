using CombatLooter.Classes.Implementation;
using CombatLooter.Classes.Implementation.V0.Enemy;
using CombatLooter.Classes.Implementation.V0.Player;
using CombatLooter.Classes.Implementation.V0.Weapon;
using CombatLooter.Enum;
using CombatLooter.Events.Implementation;
using CombatLooter.Services.Combat.Implementation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace CombatLooter.UnitTests.Services
{
    public class CombatTest
    {
        public CombatTest()
        { }

        [Fact]
        public void Combat_RunCombat_PlayerSurvives()
        {
            // Arrange
            var player = TestData.CreateTestPlayer();
            var enemies = TestData.CreateTestEnemy();
            var combat = new CombatService(player, enemies, NullLogger<CombatService>.Instance);
            // Act
            var result = combat.RunCombat();
            // Assert
            Assert.True(result, "Expected player to survive the combat.");
        }

        [Fact]
        public void Combat_RunCombat_PlayerDies()
        {
            // Arrange
            var player = TestData.CreateTestPlayer();
            var enemies = TestData.CreateTestMultipleEnemies();
            var combat = new CombatService(player, enemies, NullLogger<CombatService>.Instance);

            // Act
            var result = combat.RunCombat();

            // Assert
            Assert.False(result, "Expected player to die in the combat.");
        }

        [Fact]
        public void RunCombat_ShouldRaiseOnDamageEvent()
        {
            // Arrange
            var loggerMock = new Mock<ILogger>();
            var player = new Player(100, 50, "Hero", 5, 10, 10, 10, 10, new Dictionary<Enum.DamageModifiers, double>(), null, 1, Enum.BeingClass.Humanoid);
            var enemy = TestData.CreateTestEnemy();
            var combat = new CombatService(player, enemy, loggerMock.Object);

            var damageEvents = new List<DamageEventArgs>();
            EventHandler<DamageEventArgs> onDamage = (sender, args) => damageEvents.Add(args);

            // Act
            var result = combat.RunCombat(logger: null, onDamage: onDamage);

            // Assert
            Assert.NotEmpty(damageEvents); // Ensure at least one damage event was raised
            Assert.Contains(damageEvents, e => e.AttackerName == "Hero" && e.TargetName == enemy.FirstOrDefault()!.Name);
            Assert.Contains(damageEvents, e => e.AttackerName == enemy.FirstOrDefault()!.Name && e.TargetName == "Hero");
        }
    }

    public static class TestData
    {
        public static Player CreateTestPlayer()
        {
            return new Player(
                health: 100,
                mana: 50,
                name: "TestPlayer",
                armor: 10,
                stamina: 10,
                strength: 10,
                intelligence: 10,
                dexterity: 10,
                resistances: new Dictionary<DamageModifiers, double>(),
                weapon: new MeleeWeapon(
                    weaponType: WeaponTypes.Melee,
                    baseDamage: 20,
                    damageType: DamageTypes.Physical,
                    weight: 2,
                    attackSpeed: 2.8,
                    damageModifiers: new Dictionary<DamageModifiers, double>(),
                    iLevel: 1,
                    name: "TestSword",
                    meleeWeaponType: MeleeWeaponTypes.Sword
                ),
                level: 1,
                beingClass: BeingClass.Humanoid
            );
        }

        public static List<BaseBeing> CreateTestEnemy()
        {
            return new List<BaseBeing>
            {
                new Goblin(
                    health: 20,
                    mana: 20,
                    name: "Goblin1",
                    armor: 5,
                    stamina: 5,
                    strength: 5,
                    intelligence: 5,
                    dexterity: 12,
                    resistances: new Dictionary<DamageModifiers, double>(),
                    weapon: new MeleeWeapon(
                        weaponType: WeaponTypes.Melee,
                        baseDamage: 5,
                        damageType: DamageTypes.Physical,
                        weight: 1.5,
                        attackSpeed: 1.2,
                        damageModifiers: new Dictionary<DamageModifiers, double>(),
                        iLevel: 1,
                        name: "GoblinDagger",
                        meleeWeaponType: MeleeWeaponTypes.Dagger
                    ),
                    level: 1,
                    beingClass: BeingClass.Goblin
                    ),
            };
        }

        public static List<BaseBeing> CreateTestMultipleEnemies()
        {
            return new List<BaseBeing>
            {
                new Goblin(
                    health: 20,
                    mana: 20,
                    name: "Goblin1",
                    armor: 5,
                    stamina: 5,
                    strength: 8,
                    intelligence: 5,
                    dexterity: 12,
                    resistances: new Dictionary<DamageModifiers, double>(),
                    weapon: new MeleeWeapon(
                        weaponType: WeaponTypes.Melee,
                        baseDamage: 15,
                        damageType: DamageTypes.Physical,
                        weight: 1.5,
                        attackSpeed: 1.2,
                        damageModifiers: new Dictionary<DamageModifiers, double>(),
                        iLevel: 1,
                        name: "GoblinDagger",
                        meleeWeaponType: MeleeWeaponTypes.Dagger
                    ),
                    level: 1,
                    beingClass: BeingClass.Goblin
                    ),
                new Goblin(
                    health: 20,
                    mana: 20,
                    name: "Goblin2",
                    armor: 5,
                    stamina: 5,
                    strength: 8,
                    intelligence: 5,
                    dexterity: 12,
                    resistances: new Dictionary<DamageModifiers, double>(),
                    weapon: new MeleeWeapon(
                        weaponType: WeaponTypes.Melee,
                        baseDamage: 15,
                        damageType: DamageTypes.Physical,
                        weight: 1.5,
                        attackSpeed: 1.2,
                        damageModifiers: new Dictionary<DamageModifiers, double>(),
                        iLevel: 1,
                        name: "GoblinDagger",
                        meleeWeaponType: MeleeWeaponTypes.Dagger
                    ),
                    level: 1,
                    beingClass: BeingClass.Goblin
                    ),
                new Goblin(
                    health: 20,
                    mana: 20,
                    name: "Goblin3",
                    armor: 5,
                    stamina: 5,
                    strength: 8,
                    intelligence: 5,
                    dexterity: 12,
                    resistances: new Dictionary<DamageModifiers, double>(),
                    weapon: new MeleeWeapon(
                        weaponType: WeaponTypes.Melee,
                        baseDamage: 15,
                        damageType: DamageTypes.Physical,
                        weight: 1.5,
                        attackSpeed: 1.2,
                        damageModifiers: new Dictionary<DamageModifiers, double>(),
                        iLevel: 1,
                        name: "GoblinDagger",
                        meleeWeaponType: MeleeWeaponTypes.Dagger
                    ),
                    level: 1,
                    beingClass: BeingClass.Goblin
                    ),
                new Goblin(
                    health: 20,
                    mana: 20,
                    name: "Goblin4",
                    armor: 5,
                    stamina: 5,
                    strength: 8,
                    intelligence: 5,
                    dexterity: 12,
                    resistances: new Dictionary<DamageModifiers, double>(),
                    weapon: new MeleeWeapon(
                        weaponType: WeaponTypes.Melee,
                        baseDamage: 15,
                        damageType: DamageTypes.Physical,
                        weight: 1.5,
                        attackSpeed: 1.2,
                        damageModifiers: new Dictionary<DamageModifiers, double>(),
                        iLevel: 1,
                        name: "GoblinDagger",
                        meleeWeaponType: MeleeWeaponTypes.Dagger
                    ),
                    level: 1,
                    beingClass: BeingClass.Goblin
                    ),
                new Goblin(
                    health: 20,
                    mana: 20,
                    name: "Goblin5",
                    armor: 5,
                    stamina: 5,
                    strength: 8,
                    intelligence: 5,
                    dexterity: 12,
                    resistances: new Dictionary<DamageModifiers, double>(),
                    weapon: new MeleeWeapon(
                        weaponType: WeaponTypes.Melee,
                        baseDamage: 15,
                        damageType: DamageTypes.Physical,
                        weight: 1.5,
                        attackSpeed: 1.2,
                        damageModifiers: new Dictionary<DamageModifiers, double>(),
                        iLevel: 1,
                        name: "GoblinDagger",
                        meleeWeaponType: MeleeWeaponTypes.Dagger
                    ),
                    level: 1,
                    beingClass: BeingClass.Goblin
                    ),
            };
        }
    }
}
