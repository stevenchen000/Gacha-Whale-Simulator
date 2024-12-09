using Godot;
using System;
using Godot.Collections;

namespace StateSystem
{
    public partial class StateObject<T> : Node
    {
        [Export] protected StateResource<T> initialState;

        protected StateResource<T> currState;
        protected T stateData;
        protected bool stateChanged = false;


        public override void _Process(double delta)
        {
            stateChanged = false;
            if (currState != null)
            {
                currState.RunState(delta, stateData);
                GoToNextStateIfPossible();
            }
        }

        public void InitState(T stateData)
        {
            this.stateData = stateData;
            currState = initialState;
            currState.OnStateActivate(stateData);
        }


        private void GoToNextStateIfPossible()
        {
            var nextState = currState.GetNextState(stateData);
            if (nextState != null)
            {
                currState.OnStateDeactivate(stateData);
                nextState.OnStateActivate(stateData);
                currState = nextState;
                stateChanged = true;
            }
        }

        
    }
}