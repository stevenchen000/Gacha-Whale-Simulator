using CombatSystem;
using Godot;
using Godot.Collections;
using System;

public partial class ChapterSelectionMenu : GameMenu
{
    [Export] public Array<ChapterSelectionButton> Buttons { get; private set; }
    [Export] private Camera2D cam;

    private Node chapterMenu;

    public override void _Ready()
    {
        base._Ready();
        PopInChapterButtons();
    }

    public override void _Init()
    {
        
    }


    public void OpenChapterMenu(PackedScene menu)
    {
        chapterMenu = Utils.InstantiateCopy(menu);
        AddChild(chapterMenu);
        MoveChild(chapterMenu, 2);
    }

    private void PopInChapterButtons()
    {
        foreach (var button in Buttons)
        {
            double rand = GameState.GetRandomDouble() % 0.5;
            DelayedCalls.AddCall(rand, button.PopIn);
        }
    }

    private void PopOutChapterButtons()
    {
        foreach (var button in Buttons)
        {
            double rand = GameState.GetRandomDouble() % 0.5;
            DelayedCalls.AddCall(rand, button.PopOut);
        }
    }
}
