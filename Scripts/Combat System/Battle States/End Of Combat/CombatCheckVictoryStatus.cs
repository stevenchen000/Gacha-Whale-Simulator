using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatCheckVictoryStatus : CombatStateNode
    {
        [Export] private StateNode turnNode;
        [Export] private StateNode victoryNode;
        [Export] private StateNode lossNode;

        protected override void OnStateActivated()
        {
            base.OnStateActivated();
        }

        protected override void RunState(double delta)
        {
            
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;

            if (battle.IsPartyDead(0))
            {
                //player party is dead
                result = lossNode;
            }
            else if (battle.IsPartyDead(1))
            {
                //enemy party is dead
                result = victoryNode;
            }
            else
            {
                //no one is dead
                result = turnNode;
            }

            return result;
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
