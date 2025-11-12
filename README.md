# CombatLooter

A turn-based combat roguelike game built with .NET 8, featuring dynamic enemy encounters, weapon systems, and progression mechanics.

## 🎮 Overview

CombatLooter is a combat-focused roguelike where players face increasingly difficult enemies, collect powerful weapons, and progress through levels. The game features a sophisticated combat system with weapon-based attack timing, damage resistances, and varied enemy types.

## 🏗️ Architecture

### Core Classes

#### Game Systems
- **`Game`** - Main game loop controller, manages combat flow and player progression
- **`Combat`** - Handles turn-based combat mechanics with weapon speed-based timing
- **`Player`** - Player character with equipment slots and progression

#### Base Classes
- **`BaseBeing`** - Abstract base class for all combat entities (players and enemies)
- **`BaseWeapon`** - Abstract base class for all weapon types
- **`BaseItem`** - Base class for all items in the game
- **`BaseArmor`** - Base class for armor pieces

### Technologies Used
- **.NET 8** - Latest .NET framework
- **Serilog** - Structured logging
- **Dependency Injection** - Microsoft.Extensions.DependencyInjection
- **xUnit** - Unit testing framework

---

## ⚔️ Weapons System

### Weapon Types

#### Base Attributes
All weapons share these core attributes:

| Attribute | Type | Description |
|-----------|------|-------------|
| **WeaponType** | `WeaponTypes` | Melee or Ranged |
| **BaseDamage** | `double` | Base damage before modifiers |
| **DamageType** | `DamageTypes` | Physical or Magical |
| **Weight** | `double` | Weapon weight (affects carry capacity) |
| **AttackSpeed** | `double` | Seconds between attacks (lower = faster) |
| **DamageModifiers** | `Dictionary<DamageModifiers, double>` | Elemental/special damage bonuses |
| **ItemLevel** | `int` | Level requirement and power indicator |
| **Name** | `string` | Weapon name |

---

### Melee Weapons

Melee weapons scale with **Strength** attribute.

#### Melee Weapon Types

| Type | Speed Range | Damage Range | DPS Range | Description |
|------|-------------|--------------|-----------|-------------|
| **Sword** | 1.1 - 1.5s | 13 - 18 | 8.7 - 16.4 | Fast, balanced damage |
| **Axe** | 1.8 - 2.5s | 27 - 35 | 10.8 - 19.4 | Slow, high damage |
| **Hammer** | 2.1 - 3.0s | 32 - 40 | 10.7 - 19.1 | Very slow, devastating damage |
| **Dagger** | 0.8 - 1.0s | 10 - 15 | 10.0 - 18.8 | Very fast, low damage |
| **Spear** | 1.3 - 1.8s | 15 - 20 | 8.3 - 15.4 | Balanced speed and damage |
| **Staff** | 1.3 - 1.8s | 15 - 20 | 8.3 - 15.4 | Balanced, can deal magic damage |

**Damage Calculation (Physical):** `BaseDamage + (BaseDamage * Strength / 100)`   
**Damage Calculation (Magical):** `BaseDamage + (BaseDamage * Intelligence / 100)`

---

### Ranged Weapons

Ranged weapons scale with **Dexterity** or **Intelligence** (for wands).

#### Ranged Weapon Types

| Type | Speed Range | Damage Range | DPS Range | Description |
|------|-------------|--------------|-----------|-------------|
| **Bow** | 1.0 - 1.5s | 12 - 18 | 8.0 - 18.0 | Balanced physical damage |
| **Crossbow** | 1.5 - 2.0s | 18 - 25 | 9.0 - 16.7 | Slow, high physical damage |
| **Wand** | 0.8 - 1.2s | 10 - 15 | 8.3 - 18.8 | Fast, magical damage |

**Damage Calculation (Bow/Crossbow):** `BaseDamage + (BaseDamage * Dexterity / 100)`  
**Damage Calculation (Wand):** `BaseDamage + (BaseDamage * Intelligence / 100)`

---

### Damage Types & Modifiers

#### Primary Damage Types
- **Physical** - Standard physical damage
- **Magical** - Magical damage (typically from staves and wands)

#### Damage Modifiers (Elemental)
Weapons can have additional elemental damage:

| Modifier | Effect | Typical Resistance |
|----------|--------|-------------------|
| **Fire** | Burning damage | Dragons (50%), Beasts (20%), Ice Elementals (-20%) |
| **Ice** | Freezing damage | Ice Elementals (50%), Fire Elementals (-20%) |
| **Lightning** | Shock damage | Lightning Elementals (50%), Dragons (-20%) |
| **Poison** | Toxic damage | Beasts (20%), Dragons (30%) |
| **Shadow** | Dark magic damage | Varies by enemy |
| **True** | Ignores resistances | None |

---

## 👹 Enemy System

### Enemy Classes

All enemies inherit from `BaseBeing` and have these core stats:

| Attribute | Type | Description |
|-----------|------|-------------|
| **Name** | `string` | Enemy variant name |
| **Class** | `BeingClass` | Enemy class type |
| **Level** | `int` | Enemy power level |
| **MaxHealth** | `double` | Maximum health points |
| **CurrentHealth** | `double` | Current health points |
| **MaxMana** | `double` | Maximum mana points |
| **CurrentMana** | `double` | Current mana points |
| **Armor** | `double` | Damage reduction |
| **Stamina** | `int` | Physical endurance |
| **Strength** | `int` | Physical damage bonus |
| **Intelligence** | `int` | Magical damage bonus |
| **Dexterity** | `int` | Attack order (first turn), ranged damage bonus |
| **Resistances** | `Dictionary<DamageModifiers, double>` | % damage reduction by type |
| **EquippedWeapon** | `BaseWeapon?` | Weapon used in combat (null = unarmed) |

---

### Enemy Types & Stats

#### 1. **Goblin** 🗡️
*Common at low levels (1-5), rare at high levels*

| Variant | Health (Lv1) | Mana | Armor | STR | INT | DEX | Weapon | Special |
|---------|--------------|------|-------|-----|-----|-----|--------|---------|
| **Goblin** | 50 + (12×Lv) | 10 + (3×Lv) | 3 + (1.5×Lv) | 8+Lv | 4 | 12+Lv | None | - |

**Class:** `BeingClass.Goblin`  
**Resistances:** None

---

#### 2. **Humanoid** 👤
*Common at low-mid levels (1-10)*

| Variant | Health (Lv1) | Mana | Armor | STR | INT | DEX | Weapon | Notes |
|---------|--------------|------|-------|-----|-----|-----|--------|-------|
| **Bandit** | 70 + (14×Lv) | 20 + (5×Lv) | 5 + (2×Lv) | 12+Lv | 6 | 16+Lv | Scimitar (Sword) | Fast, agile |
| **Barbarian** | 90 + (18×Lv) | 15 + (4×Lv) | 7 + (2.5×Lv) | 16+Lv | 5 | 12+Lv | Axe | High damage, tanky |
| **Assassin** | 65 + (13×Lv) | 25 + (6×Lv) | 4 + (1.8×Lv) | 10+Lv | 8 | 18+Lv | Dagger | Very fast attacks |

**Class:** `BeingClass.Humanoid`  
**Resistances:** None

---

#### 3. **Beast** 🐺
*Moderate frequency throughout, slightly favored mid-game*

| Variant | Health (Lv1) | Mana | Armor | STR | INT | DEX | Weapon | Notes |
|---------|--------------|------|-------|-----|-----|-----|--------|-------|
| **Wolf** | 60 + (12×Lv) | 10 + (3×Lv) | 4 + (1.5×Lv) | 10+Lv | 4 | 14+Lv | None | Fast, pack hunter |
| **Bear** | 80 + (15×Lv) | 15 + (4×Lv) | 6 + (2×Lv) | 14+Lv | 5 | 10+Lv | None | High health, strong |
| **Tiger** | 70 + (13×Lv) | 12 + (3×Lv) | 5 + (1.7×Lv) | 12+Lv | 4 | 16+Lv | None | Balanced, agile |

**Class:** `BeingClass.Beast`  
**Resistances:** Fire (20%), Ice (20%), Lightning (20%), Poison (20%)

---

#### 4. **Undead** 💀
*Moderate frequency, increases mid-game*

| Variant | Health (Lv1) | Mana | Armor | STR | INT | DEX | Weapon | Notes |
|---------|--------------|------|-------|-----|-----|-----|--------|-------|
| **Skeleton** | 50 + (10×Lv) | 15 + (4×Lv) | 3 + (1.5×Lv) | 10+Lv | 6 | 12+Lv | Rusty Sword | Fragile but numerous |
| **Zombie** | 75 + (14×Lv) | 10 + (2×Lv) | 5 + (2×Lv) | 12+Lv | 4 | 8+Lv | None | Slow but tanky |
| **Lich** | 70 + (12×Lv) | 60 + (15×Lv) | 4 + (2×Lv) | 8+Lv | 16 | 10+Lv | Shadow Staff | Powerful magic |

**Class:** `BeingClass.Undead`  
**Resistances:** Shadow (40%), Poison (50%)

---

#### 5. **Elemental** ⚡
*Uncommon early, moderate mid-late*

| Variant | Health (Lv1) | Mana | Armor | STR | INT | DEX | Weapon | Notes |
|---------|--------------|------|-------|-----|-----|-----|--------|-------|
| **Fire Elemental** | 90 + (18×Lv) | 80 + (15×Lv) | 7 + (2.5×Lv) | 12+Lv | 16 | 10+Lv | None | Fire immune, Ice weak |
| **Ice Elemental** | 85 + (16×Lv) | 90 + (18×Lv) | 6 + (2×Lv) | 10+Lv | 18 | 12+Lv | None | Ice immune, Fire weak |
| **Lightning Elemental** | 100 + (20×Lv) | 70 + (12×Lv) | 8 + (3×Lv) | 14+Lv | 12 | 8+Lv | None | Lightning immune |

**Class:** `BeingClass.Elemental`  
**Resistances:**
- Fire Elemental: Fire (50%), Ice (-20%)
- Ice Elemental: Ice (50%), Fire (-20%)
- Lightning Elemental: Lightning (50%)

---

#### 6. **Giant** 🏔️
*Rare early, common late (levels 10+)*

| Variant | Health (Lv1) | Mana | Armor | STR | INT | DEX | Weapon | Notes |
|---------|--------------|------|-------|-----|-----|-----|--------|-------|
| **Hill Giant** | 120 + (22×Lv) | 20 + (5×Lv) | 10 + (3×Lv) | 20+Lv | 6 | 8+Lv | Boulder | Massive HP, slow |
| **Frost Giant** | 130 + (24×Lv) | 30 + (8×Lv) | 12 + (3.5×Lv) | 22+Lv | 8 | 10+Lv | Ice Hammer | Ice attacks |
| **Fire Giant** | 140 + (25×Lv) | 25 + (7×Lv) | 11 + (3×Lv) | 24+Lv | 10 | 9+Lv | Flaming Sword | Fire attacks |

**Class:** `BeingClass.Giant`  
**Resistances:** Physical (20%), varies by element for elemental giants

---

#### 7. **Demon** 😈
*Very rare early, increasingly common late (levels 10+)*

| Variant | Health (Lv1) | Mana | Armor | STR | INT | DEX | Weapon | Notes |
|---------|--------------|------|-------|-----|-----|-----|--------|-------|
| **Imp** | 60 + (12×Lv) | 30 + (8×Lv) | 4 + (2×Lv) | 10+Lv | 8 | 14+Lv | None | Fast, magical |
| **Hellhound** | 80 + (15×Lv) | 20 + (5×Lv) | 6 + (2.5×Lv) | 14+Lv | 6 | 16+Lv | None | Fire attacks, fast |
| **Succubus** | 70 + (10×Lv) | 50 + (12×Lv) | 5 + (2×Lv) | 8+Lv | 12 | 14+Lv | Shadow Bow | Shadow damage |

**Class:** `BeingClass.Demon`  
**Resistances:** Fire (30%), Shadow (40%)

---

#### 8. **Dragon** 🐉
*Extremely rare early, very common at high levels (15-20+)*

| Variant | Health (Lv1) | Mana | Armor | STR | INT | DEX | Weapon | Notes |
|---------|--------------|------|-------|-----|-----|-----|--------|-------|
| **Black Dragon** | 200 + (30×Lv) | 100 + (20×Lv) | 20 + (5×Lv) | 30+Lv | 15 | 10+Lv | None | Balanced, powerful |
| **Red Dragon** | 220 + (35×Lv) | 80 + (15×Lv) | 22 + (4×Lv) | 32+Lv | 12 | 12+Lv | None | Highest HP/STR |
| **Green Dragon** | 180 + (25×Lv) | 120 + (25×Lv) | 18 + (6×Lv) | 28+Lv | 18 | 14+Lv | None | High magic power |

**Class:** `BeingClass.Dragon`  
**Resistances:** Fire (50%), Ice (20%), Poison (30%), Lightning (-20%)

---

## ⚙️ Combat System

### Turn Order
1. **First Round:** Ordered by **Dexterity** (highest goes first)
2. **Subsequent Rounds:** Based on **weapon attack speed**
   - Entities attack when their cooldown expires
   - Faster weapons attack more frequently
   - Example: 1.0 speed = 1 attack/second, 2.5 speed = 1 attack/2.5 seconds

### Damage Calculation
Base Attack = Weapon BaseDamage + Stat Scaling Total Damage = Base Attack + Σ(Elemental Modifiers after Resistances) Final Health = Current Health - Total Damage


### Targeting
- **Enemies:** Always target the player
- **Player:** Targets enemy with lowest health (random if tied)

### Combat End Conditions
- Player health reaches 0 → Game Over
- All enemies defeated → Victory, proceed to loot

---

## 🎯 Features

### ✅ Implemented
- Turn-based combat with weapon speed mechanics
- 8 enemy classes with 2-3 variants each (24 total enemy types)
- Melee and ranged weapon systems
- Damage type and resistance mechanics
- Level-based enemy scaling
- Property-based modern C# API
- Comprehensive unit tests
- Structured logging with Serilog

### 🚧 In Development
- Loot system
- Equipment management
- Player progression beyond level 1
- Weighted enemy spawning based on level
- Weapon generation system

### 📋 Planned
- Armor system
- Skill/ability system
- Boss encounters
- Multiple difficulty modes
- Save/load game state

---

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or VS Code with C# extension

### Running the Game
dotnet run --project CombatLooter

### Running Tests
dotnet test --project CombatLooter.Tests

## 🧪 Testing

The project includes comprehensive unit tests for:
- `BaseBeing` - Health, mana, damage, healing, resistances
- `BaseWeapon` - Damage modifiers, calculations
- `Combat` - Turn order, damage application, victory/defeat conditions

Run tests with: dotnet test --logger "console;verbosity=detailed"

---

## 📚 Project Structure

```
CombatLooter/ 
├── Classes/ 
│   ├── Implementation/ 
│   │   ├── BaseBeing.cs                    # Abstract entity class for all beings 
│   │   ├── BaseWeapon.cs                   # Abstract weapon class 
│   │   ├── BaseItem.cs                     # Base item class 
│   │   ├── BaseArmor.cs                    # Base armor class 
│   │   └── V0/                             # Version 0 implementations 
│   │       ├── Player/ 
│   │       │   └── Player.cs               # Player character 
│   │       ├── Enemy/                      # Enemy implementations 
│   │       │   ├── Beast.cs                # Beast enemy types 
│   │       │   ├── Demon.cs                # Demon enemy types 
│   │       │   ├── Dragon.cs               # Dragon enemy types 
│   │       │   ├── Elemental.cs            # Elemental enemy types 
│   │       │   ├── Giant.cs                # Giant enemy types 
│   │       │   ├── Goblin.cs               # Goblin enemy type 
│   │       │   ├── Humanoid.cs             # Humanoid enemy types 
│   │       │   └── Undead.cs               # Undead enemy types 
│   │       └── Weapon/                     # Weapon implementations 
│   │           ├── MeleeWeapon.cs          # Melee weapon class 
│   │           └── RangedWeapon.cs         # Ranged weapon class 
│   └── Interface/ 
│       ├── IBeing.cs                       # Entity interface 
│       ├── IWeapon.cs                      # Weapon interface 
│       └── IItem.cs                        # Item interface 
├── Enum/ 
│   ├── BeingClass.cs                       # Enemy class types enum 
│   ├── DamageTypes.cs                      # Damage types and modifiers enums 
│   └── WeaponTypes.cs                      # Weapon types and subtypes enums 
├── Services/ 
│   ├── Implementation/ 
│   │   ├── Combat.cs                       # Combat system logic 
│   │   └── Game.cs                         # Main game loop controller 
│   └── Interface/ 
│       └── ICombat.cs                      # Combat service interface 
├── Program.cs                              # Application entry point 
└── CombatLooter.csproj                     # Project file

CombatLooter.UnitTests/ 
├── Units/ 
│   └── BaseBeingTests.cs                   # Tests for BaseBeing class 
├── Weapon/ 
│   └── WeaponUnitTest.cs                   # Tests for weapon classes 
├── Services/ 
│   └── CombatTest.cs                       # Tests for combat system 
└── CombatLooter.UnitTests.csproj           # Test project file
```

---

## 🤝 Contributing

This is a personal learning project, but suggestions and feedback are welcome!

---

## 📝 License

This project is for educational purposes.

---

## 📧 Contact

GitHub: [@M3dianoch3](https://github.com/M3dianoch3)

---

**Game Status:** 🔨 Active Development  
**Version:** 0.1.0 (Early Alpha)