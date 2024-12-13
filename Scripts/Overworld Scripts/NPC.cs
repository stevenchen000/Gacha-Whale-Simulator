using Godot;
using Godot.Collections;
using System;
using DialogueSystem;

public partial class NPC : CharacterBody2D
{
    public const float Speed = 300.0f;
    public const float JumpVelocity = -400.0f;
    private DialoguePicker dialogues;


    public override void _PhysicsProcess(double delta)
    {
        
    }

    public DialogueTree GetDialogue()
    {
        return dialogues.GetDialogue();
    }

}
