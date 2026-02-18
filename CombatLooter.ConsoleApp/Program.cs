using CombatLooter.Classes.Implementation;
using CombatLooter.Classes.Implementation.V0.Armor;
using CombatLooter.Classes.Implementation.V0.Player;
using CombatLooter.Classes.Implementation.V0.Weapon;
using CombatLooter.Enum;
using CombatLooter.Events.Implementation;
using CombatLooter.Services.Combat.Implementation;
using CombatLooter.Services.Game.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CombatLooter.ConsoleApp;

class Program
{
    // ── Colour helpers ──────────────────────────────────────────
    static void WriteColour(string text, ConsoleColor colour)
    {
        var prev = Console.ForegroundColor;
        Console.ForegroundColor = colour;
        Console.Write(text);
        Console.ForegroundColor = prev;
    }

    static void WriteLineColour(string text, ConsoleColor colour)
    {
        WriteColour(text + Environment.NewLine, colour);
    }

    static void WriteSeparator(char c = '─', int width = 60)
    {
        WriteLineColour(new string(c, width), ConsoleColor.DarkGray);
    }

    static void WriteHeader(string title)
    {
        Console.WriteLine();
        WriteSeparator('═');
        WriteLineColour($"  {title}", ConsoleColor.Cyan);
        WriteSeparator('═');
    }

    // ── Health bar ──────────────────────────────────────────────
    static void DrawHealthBar(string label, double current, double max, int barWidth = 30)
    {
        double pct = max > 0 ? current / max : 0;
        int filled = (int)(pct * barWidth);
        var colour = pct > 0.5 ? ConsoleColor.Green : pct > 0.25 ? ConsoleColor.Yellow : ConsoleColor.Red;

        Console.Write($"  {label,-12} ");
        WriteColour("[", ConsoleColor.DarkGray);
        WriteColour(new string('█', filled), colour);
        WriteColour(new string('░', barWidth - filled), ConsoleColor.DarkGray);
        WriteColour("]", ConsoleColor.DarkGray);
        Console.WriteLine($" {current:F0}/{max:F0}");
    }

    // ── Main ────────────────────────────────────────────────────
    static void Main(string[] args)
    {
        // ── Serilog setup ───────────────────────────────────────
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File("logs/combatlooter-console.log",
                rollingInterval: RollingInterval.Day,
                outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
            .Enrich.FromLogContext()
            .CreateLogger();

        var services = new ServiceCollection()
            .AddLogging(builder => builder.AddSerilog())
            .BuildServiceProvider();

        var loggerFactory = services.GetRequiredService<ILoggerFactory>();

        // ── Banner ──────────────────────────────────────────────
        WriteLineColour(@"
   ██████╗ ██████╗ ███╗   ███╗██████╗  █████╗ ████████╗
  ██╔════╝██╔═══██╗████╗ ████║██╔══██╗██╔══██╗╚══██╔══╝
  ██║     ██║   ██║██╔████╔██║██████╔╝███████║   ██║   
  ██║     ██║   ██║██║╚██╔╝██║██╔══██╗██╔══██║   ██║   
  ╚██████╗╚██████╔╝██║ ╚═╝ ██║██████╔╝██║  ██║   ██║   
   ╚═════╝ ╚═════╝ ╚═╝     ╚═╝╚═════╝ ╚═╝  ╚═╝   ╚═╝   
  ██╗      ██████╗  ██████╗ ████████╗███████╗██████╗ 
  ██║     ██╔═══██╗██╔═══██╗╚══██╔══╝██╔════╝██╔══██╗
  ██║     ██║   ██║██║   ██║   ██║   █████╗  ██████╔╝
  ██║     ██║   ██║██║   ██║   ██║   ██╔══╝  ██╔══██╗
  ███████╗╚██████╔╝╚██████╔╝   ██║   ███████╗██║  ██║
  ╚══════╝ ╚═════╝  ╚═════╝    ╚═╝   ╚══════╝╚═╝  ╚═╝", ConsoleColor.DarkYellow);
        Console.WriteLine();

        bool running = true;
        while (running)
        {
            WriteHeader("MAIN MENU");
            Console.WriteLine("  1. Create & inspect items (Weapons & Armor)");
            Console.WriteLine("  2. Create a Player and enemies");
            Console.WriteLine("  3. Run a single combat");
            Console.WriteLine("  4. Run a full game (auto-combat loop)");
            Console.WriteLine("  5. Item generation showcase (random loot)");
            Console.WriteLine("  0. Exit");
            WriteSeparator();
            Console.Write("  Choose an option: ");

            var choice = Console.ReadLine()?.Trim();
            switch (choice)
            {
                case "1":
                    DemoItems();
                    break;
                case "2":
                    DemoBeings();
                    break;
                case "3":
                    DemoSingleCombat(loggerFactory);
                    break;
                case "4":
                    DemoFullGame(loggerFactory);
                    break;
                case "5":
                    DemoRandomLoot();
                    break;
                case "0":
                    running = false;
                    break;
                default:
                    WriteLineColour("  Invalid option. Try again.", ConsoleColor.Red);
                    break;
            }
        }

        WriteLineColour("\n  Thanks for playing Combat Looter! Goodbye.\n", ConsoleColor.Green);
        Log.CloseAndFlush();
    }

    // ═══════════════════════════════════════════════════════════
    //  1. CREATE & INSPECT ITEMS
    // ═══════════════════════════════════════════════════════════
    static void DemoItems()
    {
        WriteHeader("ITEM CREATION DEMO");

        // ── Melee weapon ────────────────────────────────────────
        var meleeModifiers = new Dictionary<DamageModifiers, double>
        {
            { DamageModifiers.Fire, 5.0 },
            { DamageModifiers.Ice, 3.0 }
        };
        var sword = new MeleeWeapon(
            WeaponTypes.Melee, 25.0, DamageTypes.Physical,
            1.3, 3.5, meleeModifiers, 5,
            "Flamebrand Greatsword", MeleeWeaponTypes.Sword);

        WriteLineColour("  [Melee Weapon]", ConsoleColor.Yellow);
        Console.WriteLine($"  {sword}");
        Console.WriteLine($"  Weapon Type : {sword.GetWeaponType()}");
        Console.WriteLine($"  Base Damage : {sword.GetBaseDamage()}");
        Console.WriteLine($"  Damage Type : {sword.GetDamageType()}");
        Console.WriteLine($"  Attack Speed: {sword.GetAttackSpeed()}");
        Console.WriteLine($"  Weight      : {sword.GetWeight()}");
        Console.Write("  Modifiers   : ");
        foreach (var mod in sword.GetDamageModifiers())
            Console.Write($"{mod.Key}={mod.Value} ");
        Console.WriteLine();

        WriteSeparator();

        // ── Ranged weapon ───────────────────────────────────────
        var rangedModifiers = new Dictionary<DamageModifiers, double>
        {
            { DamageModifiers.Lightning, 7.0 }
        };
        var bow = new RangedWeapon(
            WeaponTypes.Ranged, 18.0, DamageTypes.Physical,
            1.2, 2.0, rangedModifiers, 4,
            "Thunderstrike Longbow", RangedWeaponTypes.Bow);

        WriteLineColour("  [Ranged Weapon]", ConsoleColor.Yellow);
        Console.WriteLine($"  {bow}");
        Console.WriteLine($"  Weapon Type : {bow.GetWeaponType()}");
        Console.WriteLine($"  Base Damage : {bow.GetBaseDamage()}");
        Console.WriteLine($"  Damage Type : {bow.GetDamageType()}");
        Console.WriteLine($"  Attack Speed: {bow.GetAttackSpeed()}");

        WriteSeparator();

        // ── Armor ───────────────────────────────────────────────
        var armorResistances = new Dictionary<DamageModifiers, double>
        {
            { DamageModifiers.Fire, 10.0 },
            { DamageModifiers.Shadow, 5.0 }
        };
        var chestplate = new ArmorItem(
            20.0, ArmorSlots.Chest, ArmorTypes.Heavy,
            armorResistances, 8.0, 6,
            "Infernal Chestplate");

        WriteLineColour("  [Armor]", ConsoleColor.Yellow);
        Console.WriteLine($"  {chestplate}");
        Console.WriteLine($"  Armor Type  : {chestplate.GetArmorTypes()}");
        Console.WriteLine($"  Armor Slot  : {chestplate.GetArmorSlot()}");
        Console.WriteLine($"  Armor Value : {chestplate.GetArmorValue()}");

        // Demonstrate mutating an item
        chestplate.ChangeArmorValue(5.0);
        WriteLineColour($"  After +5 upgrade → Armor Value: {chestplate.GetArmorValue()}", ConsoleColor.Green);

        WriteSeparator();
        Console.WriteLine("  Press any key to return...");
        Console.ReadKey(true);
    }

    // ═══════════════════════════════════════════════════════════
    //  2. CREATE PLAYER & ENEMIES
    // ═══════════════════════════════════════════════════════════
    static void DemoBeings()
    {
        WriteHeader("BEINGS DEMO (Player + Enemies)");

        // ── Player ──────────────────────────────────────────────
        var startWeapon = new MeleeWeapon(
            WeaponTypes.Melee, 12, DamageTypes.Physical,
            1.4, 2.5, new Dictionary<DamageModifiers, double>(), 1,
            "Rusty Sword", MeleeWeaponTypes.Sword);

        var player = new Player(
            health: 120, mana: 60, name: "Eldar the Brave",
            armor: 8, stamina: 12, strength: 15,
            intelligence: 10, dexterity: 13,
            resistances: new Dictionary<DamageModifiers, double>
            {
                { DamageModifiers.Fire, 10.0 }
            },
            weapon: startWeapon, level: 1, beingClass: BeingClass.Humanoid);

        WriteLineColour("  [Player]", ConsoleColor.Green);
        Console.WriteLine($"  {player}");
        DrawHealthBar("Health", player.CurrentHealth, player.MaxHealth);
        DrawHealthBar("Mana", player.CurrentMana, player.MaxMana);

        // Equip armor
        var helmet = new ArmorItem(5, ArmorSlots.Head, ArmorTypes.Medium,
            new Dictionary<DamageModifiers, double>(), 1.5, 1, "Iron Helm");
        player.Head = helmet;
        WriteLineColour($"\n  Equipped head slot: {helmet.GetName()}", ConsoleColor.DarkCyan);

        // Level up demo
        WriteLineColour("\n  ── Leveling up (weapon-based) ──", ConsoleColor.Magenta);
        int prevStr = player.Strength, prevDex = player.Dexterity, prevInt = player.Intelligence;
        player.IncreaseLevel_basedOnWeapon();
        Console.WriteLine($"  Level {player.Level - 1} → {player.Level}");
        Console.WriteLine($"  STR {prevStr} → {player.Strength}  |  DEX {prevDex} → {player.Dexterity}  |  INT {prevInt} → {player.Intelligence}");
        DrawHealthBar("Health", player.CurrentHealth, player.MaxHealth);
        DrawHealthBar("Mana", player.CurrentMana, player.MaxMana);

        WriteSeparator();

        // ── Enemies ─────────────────────────────────────────────
        WriteLineColour("  [Enemies for Level 3]", ConsoleColor.Red);
        var enemies = CombatLooter.Helper.Helper.GetEnemiesForLevel(3);
        foreach (var enemy in enemies)
        {
            Console.WriteLine($"  • {enemy}");
        }

        WriteSeparator();
        Console.WriteLine("  Press any key to return...");
        Console.ReadKey(true);
    }

    // ═══════════════════════════════════════════════════════════
    //  3. SINGLE COMBAT
    // ═══════════════════════════════════════════════════════════
    static void DemoSingleCombat(ILoggerFactory loggerFactory)
    {
        WriteHeader("SINGLE COMBAT DEMO");

        var weapon = new MeleeWeapon(
            WeaponTypes.Melee, 15, DamageTypes.Physical,
            1.3, 3.0, new Dictionary<DamageModifiers, double>
            {
                { DamageModifiers.Fire, 4.0 }
            }, 3, "Steel Longsword", MeleeWeaponTypes.Sword);

        var player = new Player(
            health: 150, mana: 60, name: "Player",
            armor: 10, stamina: 12, strength: 18,
            intelligence: 10, dexterity: 14,
            resistances: new Dictionary<DamageModifiers, double>
            {
                { DamageModifiers.Poison, 15.0 }
            },
            weapon: weapon, level: 3, beingClass: BeingClass.Humanoid);

        var enemies = CombatLooter.Helper.Helper.GetEnemiesForLevel(player.Level);

        WriteLineColour("  Player:", ConsoleColor.Green);
        Console.WriteLine($"    {player}");
        DrawHealthBar("Health", player.CurrentHealth, player.MaxHealth);

        WriteLineColour("\n  Enemies:", ConsoleColor.Red);
        foreach (var e in enemies)
        {
            Console.WriteLine($"    {e}");
        }

        WriteSeparator();
        WriteLineColour("  ── Combat Log ──", ConsoleColor.Yellow);

        var combatLogger = loggerFactory.CreateLogger<CombatService>();

        var combatService = new CombatService(player, enemies, combatLogger);
        bool playerWon = combatService.RunCombat(
            logger: msg =>
            {
                // Colourize combat log lines
                if (msg.Contains("died", StringComparison.OrdinalIgnoreCase))
                    WriteLineColour($"    💀 {msg}", ConsoleColor.Red);
                else if (msg.Contains("attacks", StringComparison.OrdinalIgnoreCase))
                    WriteLineColour($"    ⚔  {msg}", ConsoleColor.DarkYellow);
                else
                    Console.WriteLine($"    {msg}");
            },
            onDamage: (sender, dmgArgs) =>
            {
                // Display a mini health bar for the target after each hit
                var colour = dmgArgs.IsPlayerTarget ? ConsoleColor.Red : ConsoleColor.Green;
                double pct = dmgArgs.HealthPercentage * 100;
                WriteColour($"       → {dmgArgs.TargetName} HP: {dmgArgs.RemainingHealth:F0}/{dmgArgs.MaxHealth:F0} ({pct:F0}%)", colour);
                if (dmgArgs.TargetDied)
                    WriteColour(" [DEAD]", ConsoleColor.DarkRed);
                Console.WriteLine();
            });

        WriteSeparator();
        if (playerWon)
            WriteLineColour("  ✓ VICTORY! The player survived.", ConsoleColor.Green);
        else
            WriteLineColour("  ✗ DEFEAT. The player has fallen.", ConsoleColor.Red);

        // Show player post-combat stats
        DrawHealthBar("Health", player.CurrentHealth, player.MaxHealth);

        WriteSeparator();
        Console.WriteLine("  Press any key to return...");
        Console.ReadKey(true);
    }

    // ═══════════════════════════════════════════════════════════
    //  4. FULL GAME (GameService auto-loop)
    // ═══════════════════════════════════════════════════════════
    static void DemoFullGame(ILoggerFactory loggerFactory)
    {
        WriteHeader("FULL GAME RUN (auto-combat loop)");
        Console.WriteLine("  The game will run combats until the player dies.");
        Console.WriteLine("  After each victory the player levels up and loot is shown.");
        WriteSeparator();

        var gameLogger = loggerFactory.CreateLogger<GameService>();
        var game = new GameService(gameLogger);

        // Subscribe to all events
        game.OnGameStarted += (s, e) =>
            WriteLineColour("\n  ▶ Game Started!", ConsoleColor.Cyan);

        game.OnCombatStarted += (s, e) =>
        {
            WriteLineColour($"\n  ── Combat #{e.CombatNumber} ──", ConsoleColor.Yellow);
            Console.Write("  Enemies: ");
            WriteLineColour(string.Join(", ", e.EnemyNames), ConsoleColor.Red);
        };

        game.OnDamage += (s, e) =>
        {
            var colour = e.IsPlayerTarget ? ConsoleColor.Red : ConsoleColor.DarkGreen;
            WriteColour($"    ⚔ {e.AttackerName} → {e.TargetName} for {e.DamageAmount:F1} dmg", colour);
            if (e.TargetDied)
                WriteColour("  💀", ConsoleColor.DarkRed);
            Console.WriteLine();
        };

        game.OnCombatEnded += (s, e) =>
        {
            if (e.PlayerVictory)
                WriteLineColour("  ✓ Combat won!", ConsoleColor.Green);
            else
                WriteLineColour("  ✗ Player defeated.", ConsoleColor.Red);
        };

        game.OnLevelUp += (s, e) =>
        {
            WriteLineColour($"  ↑ Level Up! Now level {e.NewLevel}  " +
                $"(STR +{e.StrengthGained}, DEX +{e.DexterityGained}, INT +{e.IntelligenceGained})",
                ConsoleColor.Magenta);
        };

        game.OnLootAvailable += (s, e) =>
        {
            WriteLineColour("  ── Loot Available ──", ConsoleColor.DarkYellow);
            foreach (var item in e.AvailableItems)
            {
                Console.WriteLine($"    • {item}");
            }
        };

        game.OnLogMessage += (s, e) =>
        {
            // Only show warnings/errors to console; info goes to file via Serilog
            if (e.Level >= LogMessageEventArgs.LogLevel.Warning)
                WriteLineColour($"  [{e.Level}] {e.Message}", ConsoleColor.DarkYellow);
        };

        game.OnGameOver += (s, e) =>
            WriteLineColour("\n  ■ Game Over.", ConsoleColor.DarkRed);

        // Run the game
        game.StartNewRun();

        WriteSeparator();
        Console.WriteLine("  Press any key to return...");
        Console.ReadKey(true);
    }

    // ═══════════════════════════════════════════════════════════
    //  5. RANDOM LOOT SHOWCASE
    // ═══════════════════════════════════════════════════════════
    static void DemoRandomLoot()
    {
        WriteHeader("RANDOM LOOT GENERATOR");

        for (int level = 1; level <= 12; level += 3)
        {
            WriteLineColour($"\n  ── Level {level} Loot ──", ConsoleColor.Cyan);
            var items = CombatLooter.Helper.Helper.GetItemsForLevel(level);
            foreach (var item in items)
            {
                if (item is BaseWeapon w)
                {
                    WriteColour("    [WPN] ", ConsoleColor.Yellow);
                    Console.WriteLine($"{w.GetName()} — Dmg: {w.GetBaseDamage():F1}, Speed: {w.GetAttackSpeed():F2}, Type: {w.GetDamageType()}");
                }
                else if (item is BaseArmor a)
                {
                    WriteColour("    [ARM] ", ConsoleColor.DarkCyan);
                    Console.WriteLine($"{a.GetName()} — Def: {a.GetArmorValue():F1}, Slot: {a.GetArmorSlot()}, Type: {a.GetArmorTypes()}");
                }
                else
                {
                    Console.WriteLine($"    {item}");
                }
            }

            // Also show enemies that would spawn at this level
            WriteLineColour($"  Enemies at level {level}:", ConsoleColor.Red);
            var enemies = CombatLooter.Helper.Helper.GetEnemiesForLevel(level);
            foreach (var enemy in enemies)
            {
                Console.WriteLine($"    • {enemy.Name} (Lv{enemy.Level}) — HP: {enemy.CurrentHealth:F0}, STR: {enemy.Strength}, DEX: {enemy.Dexterity}");
            }
        }

        WriteSeparator();
        Console.WriteLine("  Press any key to return...");
        Console.ReadKey(true);
    }
}
