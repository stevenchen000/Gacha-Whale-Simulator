using System;
using Godot;
using StateSystem;

namespace DialogueSystem
{
    [GlobalClass]
    public partial class DialogueStateTimePassedCondition : StateChangeCondition<DialogueStateData>
    {
        [Export] private double timePassed = 2;

        public override bool ConditionMet(DialogueStateData stateData)
        {
            double currTime = stateData.timeInState;
            return currTime >= timePassed;
        }
    }
}
