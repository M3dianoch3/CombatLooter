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
        /// <summary>
        /// Gets or sets the head armor piece.
        /// </summary>
        public BaseArmor? Head
        {
            get => (BaseArmor?)_head;
            set => _head = value;
        }

        /// <summary>
        /// Gets or sets the chest armor piece.
        /// </summary>
        public BaseArmor? Chest
        {
            get => (BaseArmor?)_chest;
            set => _chest = value;
        }

        /// <summary>
        /// Gets or sets the leg armor piece.
        /// </summary>
        public BaseArmor? Legs
        {
            get => (BaseArmor?)_legs;
            set => _legs = value;
        }

        /// <summary>
        /// Gets or sets the feet armor piece.
        /// </summary>
        public BaseArmor? Feet
        {
            get => (BaseArmor?)_feet;
            set => _feet = value;
        }

        /// <summary>
        /// Gets or sets the hand armor piece.
        /// </summary>
        public BaseArmor? Hands
        {
            get => (BaseArmor?)_hands;
            set => _hands = value;
        }

        /// <summary>
        /// Gets or sets the shoulder armor piece.
        /// </summary>
        public BaseArmor? Shoulders
        {
            get => (BaseArmor?)_shoulders;
            set => _shoulders = value;
        }

        /// <summary>
        /// Gets or sets the waist armor piece.
        /// </summary>
        public BaseArmor? Waist
        {
            get => (BaseArmor?)_waist;
            set => _waist = value;
        }
        #endregion
    }
}
