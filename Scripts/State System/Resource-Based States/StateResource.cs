using System;
using Godot;
using Godot.Collections;

namespace StateSystem
{
    public partial class StateResource<T> : Resource
    {
        [Export] protected Array<StateChangeData<T>> stateConditions;

        public virtual void OnStateActivate(T stateData) { }
        public virtual void RunState(double delta, T stateData) { }
        public virtual void OnStateDeactivate(T stateData) { }

        public StateResource<T> GetNextState(T stateData)
        {
            StateResource<T> result = null;

            foreach(var condition in stateConditions)
            {
                if (condition.ConditionMet(stateData))
                {
                    result = condition.stateToChangeTo;
                    break;
                }
            }

            return result;
        }

    }
}
