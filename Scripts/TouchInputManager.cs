using Godot;
using System;
using Godot.Collections;

public partial class TouchInputManager : Node
{
	private Dictionary<int, Vector2> _touchStarts;
	private Dictionary<int, bool> _scrolled;

    public override void _Ready()
    {
        _touchStarts = new Dictionary<int, Vector2>();
        _scrolled = new Dictionary<int, bool>();
    }

    public override void _Input(InputEvent @event)
    {
        if(@event is InputEventScreenTouch)
        {
            var touchEvent = (InputEventScreenTouch)@event;
        }else if(@event is InputEventScreenDrag)
        {
            var dragEvent = (InputEventScreenDrag)@event;
            
        }
    }
}
