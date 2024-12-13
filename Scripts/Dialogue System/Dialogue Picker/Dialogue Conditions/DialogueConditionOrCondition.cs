using Godot;
using Godot.Collections;
using System;

namespace DialogueSystem
{
    [GlobalClass]
    public partial class DialogueConditionOrCondition : DialogueCondition
    {
        [Export] private Array<DialogueCondition> conditions;
        protected override bool _ConditionMet() {
            bool result = false;

            foreach(var condition in conditions)
            {
                if (condition.ConditionMet())
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
    }
}