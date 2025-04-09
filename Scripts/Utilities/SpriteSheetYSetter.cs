using Godot;
using System;

public partial class SpriteSheetYSetter : Node
{
    [Export] private Sprite2D Sprite;
    [Export] private int y = 0;
    public override void _Process(double delta)
    {
        CallDeferred("SetCoords");
    }

    private void SetCoords()
    {
        int x = Sprite.FrameCoords.X;
        Sprite.FrameCoords = new Vector2I(x, y);
    }

    public void SetColorIndex(int index)
    {
        y = index;
    }
}
