using Godot;
using System;

namespace CombatSystem
{
    public partial class CharacterHealth : Label
    {
        private WeakReference<BattleCharacter> _character;
        private BattleCharacter character
        {
            get
            {
                BattleCharacter result = null;
                _character.TryGetTarget(out result);
                return result;
            }
            set
            {
                _character = new WeakReference<BattleCharacter>(value);
            }
        }

        public override void _Ready()
        {
            character = Utils.FindParentOfType<BattleCharacter>(this);
            
        }

        public override void _Process(double delta)
        {
            UpdateHealth();
        }

        private void UpdateHealth()
        {
            var stats = character.stats;
            int maxHealth = stats.maxHealth;
            int currHealth = stats.currentHealth;
            Text = $"{currHealth} / {maxHealth}";
        }
    }
}