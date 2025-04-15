using CombatSystem;
using Godot;
using System;

public partial class ChapterSelectionButton : Control
{
    [Export] public PackedScene Chapter { get; private set; }
    [Export] private AnimationPlayer anim;
    private ChapterSelectionMenu menu;

    public override void _Ready()
    {
        menu = Utils.FindParentOfType<ChapterSelectionMenu>(this);
    }

    public void OnClick()
    {
        menu.OpenChapterMenu(Chapter);
    }

    public bool IsUnlocked()
    {
        return true;
    }

    public bool NeedsToPlayUnlockAnimation()
    {
        return false;
    }

    public void PopIn()
    {
        anim.Play("pop_in");
    }

    public void PopOut()
    {
        anim.Play("pop_out");
    }

    public new void Hide()
    {
        Scale = Vector2.Zero;
    }
}
