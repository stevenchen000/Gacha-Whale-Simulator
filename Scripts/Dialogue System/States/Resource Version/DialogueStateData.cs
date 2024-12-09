using System;
using Godot;
using StateSystem;

namespace DialogueSystem
{
    public class DialogueStateData
    {
        public DialogueManager manager { get; set; }
        public DialogueTree dialogue { get; set; } = null;
        public DialogueScene currDialogue { get; set; } = null;
        public float numOfCharsToDisplay = 0;
        public string currText { get; set; }
        public double timeInState { get; set; } = 0;

        public DialogueStateData(DialogueManager manager)
        {
            this.manager = manager;
        }

        public void Tick(double delta)
        {
            timeInState += delta;
        }

        public void ResetTime()
        {
            timeInState = 0;
        }
    }
}
