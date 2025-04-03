using Godot;
using Godot.Collections;
using System;

namespace CombatSystem
{
    public partial class DirectionSelectionButton : Button
    {
        [Export] private CharacterDirection direction;
        [Export] private bool Debug = false;
        private SimpleWeakRef<BattleManager> _battleRef;

        private TargetingSelection targetSelection;


        public override void _Ready()
        {
            var battle = Utils.FindParentOfType<BattleManager>(this);
            _battleRef = new SimpleWeakRef<BattleManager>(battle);
            targetSelection = new TargetingSelection(direction);
            HideButton();
        }

        public void OnClick()
        {
            var battle = _battleRef.Value;
            var currTargetSelection = battle.Grid.CurrentSelection;

            if (currTargetSelection == targetSelection)
            {
                battle.ConfirmAction();
            }
            else
            {
                battle.SetTargetSelection(targetSelection);
            }
        }


        private bool SelectionIsValid(TargetingSelection selection)
        {
            return selection != null && selection.Style == TargetSelectionStyle.SelectDirection;
        }



        public void RevealButton(Array<CharacterDirection> directions)
        {
            if (directions.Contains(direction))
            {
                MouseFilter = MouseFilterEnum.Stop;
                Visible = true;
            }
        }

        public void HideButton()
        {
            MouseFilter = MouseFilterEnum.Ignore;
            Visible = false;
        }
    }
}