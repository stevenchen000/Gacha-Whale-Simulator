using Godot;
using System;
using EventSystem;
using DialogueSystem;

public partial class PlayerEventManager : Node
{
    private Player player;
    [Export] private DialogueEvent OnDialogueStart;
    [Export] private DialogueEvent OnDialogueStop;
    [Export] private VoidEvent OnLoadStart;
    [Export] private VoidEvent OnLoadStop;

    public override void _Ready()
    {
        base._Ready();
        Init();
    }

    private void Init()
    {
        player = (Player)GetParent();
        player.LockMovement(); //initial lock because load screen always enabled at start
        OnDialogueStart.SubscribeEvent((DialogueTree dialogue) => player.LockMovement());
        OnDialogueStop.SubscribeEvent((DialogueTree dialogue) => player.UnlockMovement());
        OnLoadStart.SubscribeEvent(player.LockMovement);
        OnLoadStop.SubscribeEvent(player.UnlockMovement);
    }
}
