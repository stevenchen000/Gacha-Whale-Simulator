using System;
using Godot;
using StateSystem;

namespace DialogueSystem
{
    public partial class DialogueStateNode : StateNode
    {
        [Export] protected DialogueManager dialogue;
    }
}
