using System;
using Godot;
using DialogueSystem;

[GlobalClass]
public partial class WorldEventDialogueEvent : Resource
{
    [Export] public DialogueTree afterDialogue;
    [Export] public WorldEventEffect effect;
    [Export] private string description;
}

