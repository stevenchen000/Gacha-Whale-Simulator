using System;
using Godot;
using StateSystem;

namespace DialogueSystem
{
    [GlobalClass]
    public partial class DialogueStateDialogueNotNullCondition : StateChangeCondition<DialogueStateData>
    {

        public override bool ConditionMet(DialogueStateData stateData)
        {
            return stateData.dialogue != null;
        }
    }
}
