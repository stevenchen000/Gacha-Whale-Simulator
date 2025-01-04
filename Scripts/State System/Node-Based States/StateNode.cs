using Godot;
using System;

namespace StateSystem
{
    public partial class StateNode : Node2D
    {
        public bool active {
            get { return _active; } 
            set {
                _active = value;
                if (value)
                {
                    OnStateActivated();
                    ResetTime();
                }
                else
                {
                    OnStateDeactivated();
                }
            } 
        }
        private bool _active = false;
        [Export] private bool IsInitialState = false;
        protected double timeInState = 0;

        public override void _Ready()
        {
            if (IsInitialState)
            {
                active = true;
            }
        }

        public override void _Process(double delta)
        {
            if (_active)
            {
                Tick(delta);
                RunState(delta);
            }
        }

        protected virtual void RunState(double delta)
        {

        }

        protected virtual void OnStateActivated()
        {

        }

        protected virtual void OnStateDeactivated()
        {

        }

        protected void ChangeState(StateNode node)
        {
            active = false;
            node.active = true;
        }

        private void Tick(double delta)
        {
            timeInState += delta;
        }

        private void ResetTime()
        {
            timeInState = 0;
        }
    }
}