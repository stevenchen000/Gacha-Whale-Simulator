using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatPreTurnStartNode : CombatStateNode
    {
        [Export] private CombatStateNode turnStart;
        [Export] private float delay = 1f;
        private BattleCharacter currCharacter;

        protected override void OnStateActivated()
        {
            base.OnStateActivated();
            currCharacter = battle.GetCurrentCharacter();
            currCharacter.Status.RunTurnStartEffects();
        }

        protected override void RunState(double delta)
        {
            
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;

            if(timeInState > delay)
            {
                result = turnStart;
            }

            return result;
        }

        protected override void OnStateDeactivated()
        {
            currCharacter = null;
        }
    }
}
