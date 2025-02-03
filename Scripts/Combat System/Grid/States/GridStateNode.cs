using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class GridStateNode : StateNode
    {
        protected WeakReference spaceRef;
        protected GridSpace Space 
        {
            get 
            {
                return (GridSpace)spaceRef.Target;
            }
        }

        public override void _Ready()
        {
            base._Ready();
            var space = Utils.FindParentOfType<GridSpace>(this);
            spaceRef = new WeakReference(space);
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
