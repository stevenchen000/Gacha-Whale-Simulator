using Godot;
using Godot.Collections;
using System;

namespace DialogueSystem
{
    [GlobalClass]
    public partial class DialogueConditionFlagSet : DialogueCondition
    {
        [Export] private string flag;
        protected override bool _ConditionMet() {
            return GameState.GetFlag(flag);
        }
    }
}