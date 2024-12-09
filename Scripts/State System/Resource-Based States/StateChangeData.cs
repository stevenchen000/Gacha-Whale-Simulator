using System;
using Godot;
using Godot.Collections;

namespace StateSystem
{
    public partial class StateChangeData<T> : Resource
    {
        [Export] public StateResource<T> stateToChangeTo { get; set; }
        [Export] protected Array<StateChangeCondition<T>> conditions;

        public bool ConditionMet(T stateData)
        {
            bool result = true;

            foreach(var condition in conditions)
            {
                if (!condition.ConditionMet(stateData))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

    }
}
