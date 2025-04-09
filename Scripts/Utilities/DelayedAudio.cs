using Godot;
using System;

public partial class DelayedAudio : AudioStreamPlayer
{
    [Export] private double delay = 0;
    [Export] private float fromPosition = 0;
    private TimeHandler time;


    public override void _Ready()
    {
        time = new TimeHandler();
    }

    public override void _Process(double delta)
    {
        time.Tick(delta);
        if (time.TimeReached(delay))
        {
            Play(fromPosition);
        }
    }
}
