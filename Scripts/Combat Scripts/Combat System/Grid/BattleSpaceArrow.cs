using CombatSystem;
using Godot;
using System;

public partial class BattleSpaceArrow : Node2D
{
    [Export] private GridSpace space;

    public override void _Process(double delta)
    {
        Visible = space.SelectionHasDirection;
        Rotation = Mathf.DegToRad((int)space.DirectionSelection * -90 - 90);
    }
}
