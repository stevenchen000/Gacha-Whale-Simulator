using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class GridStateNode : StateNode
    {
        protected GridSpace space;

        public override void _Ready()
        {
            base._Ready();
            space = Utils.FindParentOfType<GridSpace>(this);
        }

        protected override void OnStateActivated()
        {
            
        }

        protected override void RunState(double delta)
        {
            
        }

        protected override StateNode CheckStateChange()
        {
            return null;
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
