using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatStateNode : StateNode
    {
        protected BattleManager battle;

        public override void _Ready()
        {
            base._Ready();
            battle = (BattleManager)FindParent("Battle Manager");
        }

        protected override void OnStateActivated()
        {
            
        }

        protected override void RunState(double delta)
        {
            
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
