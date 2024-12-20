using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatDamageNode : CombatStateNode
    {
        [Export] private CombatStateNode turnNode;
        [Export] private CombatStateNode finishedNode;

        protected override void OnStateActivated()
        {
            GD.Print("Dealt damage");
        }

        protected override void RunState(double delta)
        {
            //run damage animations
            if (timeInState < 2) return;
            ChangeState(turnNode);

            if (battle.IsPartyDead())
            {
                ChangeState(finishedNode);
            }
            else
            {
                ChangeState(turnNode);
            }
        }

        protected override void OnStateDeactivated()
        {
            battle.ProgressTurn();
            
        }
    }
}
