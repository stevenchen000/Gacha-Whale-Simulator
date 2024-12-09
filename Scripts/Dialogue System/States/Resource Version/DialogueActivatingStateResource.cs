using System;
using Godot;
using StateSystem;
using Godot.Collections;

namespace DialogueSystem
{
    [GlobalClass]
    public partial class DialogueActivatingStateResource : DialogueStateResource
    {

        public override void OnStateActivate(DialogueStateData data)
        {
            data.manager.Activate();
            data.currDialogue = data.dialogue.GetDialogueStart();
        }

        public override void RunState(double delta, DialogueStateData data)
        {
            
        }

        public override void OnStateDeactivate(DialogueStateData data)
        {
            
        }

        
    }
}
