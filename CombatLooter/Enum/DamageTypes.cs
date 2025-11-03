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
        Fire = 2,
        Ice = 4,
        Lightning = 8,
        Poison = 16,
        True = 32
    }
}
