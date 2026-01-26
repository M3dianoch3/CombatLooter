using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatLooter.Enum
{
    /// <summary>
    /// Damage types available in the game.
    /// </summary>
    public enum DamageTypes
    {
        Physical,
        Magical
    }

    /// <summary>
    /// Damage modifiers available in the game (flag).
    /// </summary>
    [Flags]
    public enum DamageModifiers
    {
        Fire = 0,
        Ice = 2,
        Lightning = 4,
        Poison = 8,
        True = 16,
        Shadow = 32,
    }
}
