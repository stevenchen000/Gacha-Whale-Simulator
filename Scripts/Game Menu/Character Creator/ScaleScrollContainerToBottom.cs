using System;
using Godot;


public partial class ScaleScrollContainerToBottom : Node
{
    [Export] private Control element;
    public override void _Ready()
    {
        var viewport = GetViewport();
        var viewportSize = viewport.GetVisibleRect().Size;
        var newSize = viewportSize.Y - element.Position.Y;
        element.Size = new Vector2(element.Size.X, newSize);
    }



}

