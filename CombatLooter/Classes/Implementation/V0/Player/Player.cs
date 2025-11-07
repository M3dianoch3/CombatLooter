using CombatLooter.Classes.Implementation.V0.Weapon;
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

        #region Increase level methods

        /// <summary>
        /// Increase stats based on weapon equipped.
        /// </summary>
        public void IncreaseLevel_basedOnWeapon()
        {
            switch (this.EquippedWeapon?.GetWeaponType())
            {   
                case WeaponTypes.Melee:
                    this.Strength += 5;
                    this.Dexterity += 2;
                    this.Intelligence += 2;
                    break;
                case WeaponTypes.Ranged:
                    if (this.EquippedWeapon is RangedWeapon rangedWeapon && rangedWeapon.RangedWeaponType == RangedWeaponTypes.Wand)
                    {
                        this.Intelligence += 5;
                        this.Dexterity += 2;
                        this.Strength += 2;
                    }
                    else
                    {
                        this.Dexterity += 5;
                        this.Strength += 2;
                        this.Intelligence += 2;
                    }
                    break;
                default:
                    // Default stat increase if no weapon is equipped
                    this.Strength += 2;
                    this.Dexterity += 2;
                    this.Intelligence += 2;
                    break;
            }

            this.MaxHealth += 20;
            this.MaxMana += 20;

            // Restore health and mana to full upon leveling up
            this.CurrentHealth = this.MaxHealth;
            this.CurrentMana = this.MaxMana;

            // Increase level by 1
            this.Level += 1;
        }

        #endregion
    }
}
