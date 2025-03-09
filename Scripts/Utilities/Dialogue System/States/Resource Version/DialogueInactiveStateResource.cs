using System;
using Godot;
using StateSystem;
using EventSystem;

namespace DialogueSystem
{
    [GlobalClass]
    public partial class DialogueInactiveStateResource : DialogueStateResource
    {

        public override void OnStateActivate(DialogueStateData data)
        {
            data.manager.Deactivate();
        }

        public override void RunState(double delta, DialogueStateData data)
        {

        }

        public override void OnStateDeactivate(DialogueStateData data)
        {

        }


    }
}
