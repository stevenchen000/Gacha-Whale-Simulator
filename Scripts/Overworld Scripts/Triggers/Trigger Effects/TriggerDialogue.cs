using Godot;
using System;
using DialogueSystem;

[GlobalClass]
public partial class TriggerDialogue : TriggerEffect
{
    [Export] private DialoguePicker dialogues;
    
    public override void ActivateEffect(TriggerDetector activator, Trigger trigger)
    {
        var dialogue = dialogues.GetDialogue();
        DialogueManager.PlayDialogue(dialogue);
    }
}
