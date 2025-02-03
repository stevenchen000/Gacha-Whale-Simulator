using Godot;
using Godot.Collections;
using System;

namespace DialogueSystem
{
    [GlobalClass]
    public partial class DialoguePicker : Resource
    {
        [Export] private Array<DialogueAndCondition> dialogueList;

        public virtual DialogueTree GetDialogue()
        {
            return FindFirstViableDialogue();
        }

        private DialogueTree FindFirstViableDialogue()
        {
            DialogueTree result = null;
            bool broken = false;

            for(int i = 0; i < dialogueList.Count; i++)
            {
                var dialogueCondition = dialogueList[i];
                Utils.Print(this, "Checking conditions");
                Utils.Print(this, $"Broken?: {broken} {i}");
                if (dialogueCondition.ConditionsMet())
                {
                    result = dialogueCondition.GetDialogue();
                    Utils.Print(this, "Break!");
                    broken = true;
                    break;
                }
                Utils.Print(this, "Should not see this after a break");
            }

            return result;
        }
    }
}