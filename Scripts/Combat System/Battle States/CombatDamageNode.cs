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
            
        }

        protected override void RunState(double delta)
        {
            //run damage animations
            if (timeInState < 2) return;

            if (battle.IsPlayerPartyDead() || battle.IsEnemyPartyDead())
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
