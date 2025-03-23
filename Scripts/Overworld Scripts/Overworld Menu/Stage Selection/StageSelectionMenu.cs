using CombatSystem;
using Godot;
using Godot.Collections;
using System;

public partial class StageSelectionMenu : GameMenu
{
    [Export] private PackedScene stageSelectButtonScene;
    [Export] private Array<StageData> stages;
    [Export] private Node grid;
    private Array<StageSelectButton> StageButtons;

    public override void _Ready()
    {
        base._Ready();
        RemoveButtons();
        InitButtons();
    }

    private void RemoveButtons()
    {
        var children = grid.GetChildren();
        foreach(var child in children)
        {
            child.QueueFree();
        }
    }

    private void InitButtons()
    {
        foreach(var stage in stages)
        {
            var button = Utils.InstantiateCopy<StageSelectButton>(stageSelectButtonScene);
            button.SetStageData(stage);
            grid.AddChild(button);
        }
    }
}
