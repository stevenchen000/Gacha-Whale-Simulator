using Godot;
using System;
using Godot.Collections;

namespace CombatSystem
{
    public partial class DirectionButtonUI : Control
    {
        [Export] private Array<DirectionSelectionButton> buttons;
        [Export] private Button cancelButton;

        private SimpleWeakRef<BattleManager> battle;

        public override void _Ready()
        {
            var tempBattle = Utils.FindParentOfType<BattleManager>(this);
            battle = new SimpleWeakRef<BattleManager>(tempBattle);
        }

        public void RevealButtons(TargetingData data)
        {
            var validDirections = data.GetValidDirections(battle.Value.Grid);
            foreach (var button in buttons)
            {
                button.RevealButton(validDirections);
            }

            cancelButton.Disabled = false;
        }

        public void HideButtons()
        {
            Position = new Vector2(99999, 99999);

            foreach(var button in buttons)
            {
                button.HideButton();
            }

            cancelButton.Disabled = true;
        }

        public void CancelMenu()
        {
            HideButtons();
            battle.Value.SetSelectedSkill(null);
            battle.Value.HideConfirmationAndShowSkipButton();
        }
    }
}