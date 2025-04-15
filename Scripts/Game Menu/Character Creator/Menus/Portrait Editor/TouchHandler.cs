using System;
using GachaSystem;
using Godot;
using Godot.Collections;


public partial class TouchHandler : Node
{
    private static TouchHandler _instance;


    public static int NumOfTouches { get { return TouchPositions.Count; } }

    /// <summary>
    /// Positions on screen where all touches are
    /// </summary>
    public static Dictionary<int, Vector2> TouchPositions { get; private set; }
    private static Dictionary<int, Vector2> _PrevTouchPositions;
    /// <summary>
    /// Distance touch traveled since previous touch
    /// </summary>
    public static Dictionary<int, Vector2> DragDistance { get; private set; }

    
    /// <summary>
    /// Number of pixels between first two touches
    /// 0 if not pinching
    /// </summary>
    public static float PinchDistance { get; private set; } = 0;
    /// <summary>
    /// Size change of distance between two touches since last frame
    /// 0 if not pinching
    /// </summary>
    public static float PinchDelta { get; private set; } = 0;
    /// <summary>
    /// Location of the center of the current pinch
    /// </summary>
    public static Vector2 PinchPosition { get; private set; }
    /// <summary>
    /// How much the center of the pinch moved
    /// </summary>
    public static Vector2 PinchMovement { get; private set; }

    public override void _Ready()
    {
        if (_instance == null)
        {
            _instance = this;
            _PrevTouchPositions = new Dictionary<int, Vector2>();
            TouchPositions = new Dictionary<int, Vector2>();
            DragDistance = new Dictionary<int, Vector2>();
        }
        else
        {
            QueueFree();
        }
    }

    public override void _Process(double delta)
    {
        //handles pinch variables
        if(TouchPositions.ContainsKey(0) && TouchPositions.ContainsKey(1))
        {
            var touch1 = TouchPositions[0];
            var touch2 = TouchPositions[1];
            var pinchCenter = (touch1 + touch2) / 2;
            float distance = (touch1 - touch2).Length();
            if (PinchDistance != 0)
            {
                PinchDelta = distance - PinchDistance;
            }

            if(PinchPosition != Vector2.Zero)
            {
                PinchMovement = pinchCenter - PinchPosition;
            }
            PinchDistance = distance;
            PinchPosition = pinchCenter;
        }
        else
        {
            PinchDistance = 0;
            PinchDelta = 0;
            PinchPosition = new Vector2(0, 0);
            PinchMovement = new Vector2(0, 0);
        }

        if (TouchPositions.ContainsKey(0))
        {
            if (_PrevTouchPositions.ContainsKey(0))
            {
                var currTouch = TouchPositions[0];
                var prevTouch = _PrevTouchPositions[0];
                DragDistance[0] = currTouch - prevTouch;
            }
            else
            {
                DragDistance[0] = Vector2.Zero;
            }
        }
        _PrevTouchPositions = TouchPositions.Duplicate();
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
                TouchPositions[index] = touchPos;
            }
            else
            {
                TouchPositions.Remove(index);
                DragDistance.Remove(index);
            }
        }
        else if(@event is InputEventScreenDrag)
        {
            var dragEvent = @event as InputEventScreenDrag;
            int index = dragEvent.Index;
            var touchPos = dragEvent.Position;
            var distance = touchPos - TouchPositions[index];

            TouchPositions[index] = touchPos;
        }
    }
}

