using System;
using GachaSystem;
using Godot;
using Godot.Collections;


public partial class TouchHandler : Node
{
    public Dictionary<int, Vector2> touchPositions { get; private set; }
    private Dictionary<int, Vector2> _prevTouchPositions;
    public Dictionary<int, Vector2> dragDistance { get; private set; }
    

    public float pinchDistance { get; private set; } = 0;
    public float pinchDelta { get; private set; } = 0;
    public Vector2 pinchPosition { get; private set; }
    public Vector2 pinchMovement { get; private set; }

    public override void _Ready()
    {
        _prevTouchPositions = new Dictionary<int, Vector2>();
        touchPositions = new Dictionary<int, Vector2>();
        dragDistance = new Dictionary<int, Vector2>();
    }

    public override void _Process(double delta)
    {
        //handles pinch variables
        if(touchPositions.ContainsKey(0) && touchPositions.ContainsKey(1))
        {
            var touch1 = touchPositions[0];
            var touch2 = touchPositions[1];
            var pinchCenter = (touch1 + touch2) / 2;
            float distance = (touch1 - touch2).Length();
            if (pinchDistance != 0)
            {
                pinchDelta = distance - pinchDistance;
            }

            if(pinchPosition != Vector2.Zero)
            {
                pinchMovement = pinchCenter - pinchPosition;
            }
            pinchDistance = distance;
            pinchPosition = pinchCenter;
        }
        else
        {
            pinchDistance = 0;
            pinchDelta = 0;
            pinchPosition = new Vector2(0, 0);
            pinchMovement = new Vector2(0, 0);
        }

        if (touchPositions.ContainsKey(0))
        {
            if (_prevTouchPositions.ContainsKey(0))
            {
                var currTouch = touchPositions[0];
                var prevTouch = _prevTouchPositions[0];
                dragDistance[0] = currTouch - prevTouch;
            }
            else
            {
                dragDistance[0] = Vector2.Zero;
            }
        }
        _prevTouchPositions = touchPositions.Duplicate();
    }

    public override void _Input(InputEvent @event)
    {
        if(@event is InputEventScreenTouch)
        {
            var touchEvent = @event as InputEventScreenTouch;
            int index = touchEvent.Index;
            bool pressed = touchEvent.Pressed;
            var touchPos = touchEvent.Position;

            if (pressed)
            {
                touchPositions[index] = touchPos;
            }
            else
            {
                touchPositions.Remove(index);
                dragDistance.Remove(index);
            }
        }
        else if(@event is InputEventScreenDrag)
        {
            var dragEvent = @event as InputEventScreenDrag;
            int index = dragEvent.Index;
            var touchPos = dragEvent.Position;
            var distance = touchPos - touchPositions[index];

            touchPositions[index] = touchPos;
        }
    }
}

