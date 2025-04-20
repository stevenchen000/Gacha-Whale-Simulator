using Godot;
using System;

namespace StateSystem
{
    public partial class StateNode : Node2D
    {
        [Export] protected bool debug = false;
        [Export(PropertyHint.MultilineText)] protected string Description;

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
                CallDeferred(MethodName._InitNode);
            }
        }

        private void _InitNode()
        {
            active = true;
        }

        public override void _Process(double delta)
        {
            if (_active)
            {
                Tick(delta);
                RunState(delta);
                var newState = CheckStateChange();
                if (newState != null) 
                {
                    if (debug) Utils.Print(this, $"Exited state: {Name}");
                    ChangeState(newState);
                    if (debug) Utils.Print(this, $"Entered state: {newState.Name}");
                }
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

        /// <summary>
        /// Override to change state after process call
        /// </summary>
        /// <returns></returns>
        protected virtual StateNode CheckStateChange() { return null; }

        protected void ChangeState(StateNode node)
        {
            CallDeferred("_ChangeState", node);
        }

        private void _ChangeState(StateNode node)
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