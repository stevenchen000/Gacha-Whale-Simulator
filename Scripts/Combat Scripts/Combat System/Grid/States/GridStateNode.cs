using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class GridStateNode : StateNode
    {
        private SimpleWeakRef<GridSpace> _space;
        protected GridSpace Space 
        {
            get 
            {
                return _space.Value;
            }
            set
            {
                _space = new SimpleWeakRef<GridSpace> (value);
            }
        }

        private SimpleWeakRef<BattleGrid> _grid;
        protected BattleGrid Grid
        {
            get
            {
                return _grid.Value;
            }
            set
            {
                _grid = new SimpleWeakRef<BattleGrid> (value);
            }
        }

        private SimpleWeakRef<BattleManager> _battle;
        protected BattleManager Battle
        {
            get
            {
                return _battle.Value;
            }
            set
            {
                _battle = new SimpleWeakRef<BattleManager>(value);
            }
        }

        public override void _Ready()
        {
            base._Ready();
            Space = Utils.FindParentOfType<GridSpace>(this);
            Grid = Utils.FindParentOfType<BattleGrid>(this);
        }

        protected override void OnStateActivated()
        {
            Space.SetState(this);
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
            Utils.Print(this, $"{Space.CanSelect}");
        }
    }
}
