using Godot;
using Godot.Collections;
using System;

namespace CombatSystem
{
    public partial class DirectionSelectionButton : Button
    {
        [Export] private CharacterDirection direction;
        [Export] private bool Debug = false;
        private WeakReference<BattleManager> _battleRef;


        public override void _Ready()
        {
            var battle = Utils.FindParentOfType<BattleManager>(this);
            _battleRef = new WeakReference<BattleManager>(battle);
            HideButton();
        }

        public void OnClick()
        {
            BattleManager battle;
            _battleRef.TryGetTarget(out battle);

            if (battle.Direction != direction)
            {
                battle.SelectDirection(direction);
                battle.ShowConfirmationAndHideSkipButton();
            }
            else
            {
                battle.ConfirmAction();
            }
            if (Debug)
                Utils.Print(this, direction);
        }

        public void RevealButton(Dictionary<CharacterDirection, Array<GridSpace>> targets)
        {
            if (targets.ContainsKey(direction))
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