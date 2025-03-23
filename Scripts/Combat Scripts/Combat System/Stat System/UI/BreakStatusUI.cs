using Godot;
using System;

namespace CombatSystem
{
    public partial class BreakStatusUI : Node
    {
        [Export] private Control ampNode;
        [Export] private Label breakTurns;
        [Export] private Color defaultColor;
        [Export] private Color breakColor;
        [Export] private BattleCharacter character;
        [Export] private AnimationPlayer anim;

        private int prevValue = 0;

        public void UpdateStatus()
        {
            if(ampNode != null) UpdateAmpNode();
            if (breakTurns != null) UpdateTurnsLeft();
        }

        private void UpdateAmpNode()
        {
            if (character.IsBroken())
            {
                ampNode.Modulate = breakColor;
            }
            else
            {
                ampNode.Modulate = defaultColor;
            }
        }

        private void UpdateTurnsLeft()
        {
            bool isBroken = character.IsBroken();
            int turnsLeft = character.GetTurnsToUnbreak();

            if (isBroken)
            {
                breakTurns.Visible = true;
                breakTurns.Text = turnsLeft.ToString();
                if(turnsLeft != prevValue)
                {
                    prevValue = turnsLeft;
                    anim.Play("transition");
                }
            }
            else
            {
                breakTurns.Visible = false;
            }
        }
    }
}