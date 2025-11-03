using CombatLooter.Enum;

namespace CombatLooter.Classes.Implementation.V0.Player
{
    public class Player : BaseBeing
    {
        private BaseItem? _head;
        private BaseItem? _chest;
        private BaseItem? _legs;
        private BaseItem? _feet;
        private BaseItem? _hands;
        private BaseItem? _shoulders;
        private BaseItem? _waist;

        public Player(double health, double mana, string name, double armor, int stamina, int strength, int intelligence, int dexterity, Dictionary<DamageModifiers, double> resistances, BaseWeapon weapon, int level, BeingClass beingClass) : base(health, mana, name, armor, stamina, strength, intelligence, dexterity, resistances, weapon, level, beingClass)
        {
        }

        #region Item slots
        #endregion
    }
}
