using System;
using Godot;

namespace StateSystem
{
    public partial class StateChangeCondition<T> : Resource
    {
        public virtual bool ConditionMet(T stateData)
        {
            return true;
        }

    }
}
