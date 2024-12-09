using System;
using Godot;
using StateSystem;

namespace DialogueSystem
{
    [GlobalClass]
    public partial class DialogueDisplayingTextStateResource : DialogueStateResource
    {
        [Export] private float charactersPerSecond = 30;


        public override void OnStateActivate(DialogueStateData data)
        {

        }

        public override void RunState(double delta, DialogueStateData data)
        {

        }

        public override void OnStateDeactivate(DialogueStateData data)
        {

        }




        /******************
         * Helpers
         * ***************/

        private void SetText(string text)
        {
            //dialogueManager.SetText(text);
        }
    }
}
