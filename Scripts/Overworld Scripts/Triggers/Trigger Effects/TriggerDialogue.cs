using Godot;
using System;
using DialogueSystem;

[GlobalClass]
public partial class TriggerDialogue : TriggerEffect
{
    [Export] private DialogueTree dialogue;
    
    public override void ActivateEffect(TriggerDetector activator, Trigger trigger)
    {
        DialogueManager.PlayDialogue(dialogue);
    }
}
