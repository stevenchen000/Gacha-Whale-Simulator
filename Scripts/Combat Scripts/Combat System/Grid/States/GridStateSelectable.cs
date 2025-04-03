using System;
using System.Reflection.Metadata.Ecma335;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class GridStateSelectable : GridStateNode
    {
        [Export] private GridStateNode defaultState;
        [Export] private GridStateNode targetingState;

        public override void _Ready()
        {
            base._Ready();
            debug = true;
        }

        protected override void OnStateActivated()
        {
            Space.SetColor(Space.selectableColor);
        }

        protected override void RunState(double delta)
        {
            
        }




        protected override StateNode CheckStateChange()
        {
            GridStateNode result = null;

            if (Space.HasSelected)
                result = targetingState;
            else if (!Space.CanSelect)
                result = defaultState;

            return result;
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
