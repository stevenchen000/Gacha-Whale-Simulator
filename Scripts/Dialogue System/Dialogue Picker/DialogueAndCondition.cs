using Godot;
using Godot.Collections;
using System;

namespace DialogueSystem
{
    [GlobalClass]
    public partial class DialogueAndCondition : Resource
    {
        [Export] private DialogueTree dialogue;
        [Export] private Array<DialogueCondition> conditions;

        public bool ConditionsMet()
        {
            bool result = true;
            foreach(var condition in conditions)
            {
                if (!condition.ConditionMet())
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        public DialogueTree GetDialogue()
        {
            return dialogue;
        }
    }
}