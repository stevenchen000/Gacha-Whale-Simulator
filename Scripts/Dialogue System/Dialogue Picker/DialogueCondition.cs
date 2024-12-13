using Godot;
using Godot.Collections;
using System;

namespace DialogueSystem
{
    public partial class DialogueCondition : Resource
    {
        [Export] protected bool oppositeCondition = false;
        public bool ConditionMet() {
            bool result = _ConditionMet();
            if (oppositeCondition) result = !result;
            return result; 
        }
        protected virtual bool _ConditionMet() { return true; }
    }
}