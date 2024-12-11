using Godot;
using System;

[GlobalClass]
public partial class SkipTimeTriggerEffect : TriggerEffect
{
    [Export] private int hour;
    [Export] private int minute;

    public override void ActivateEffect(TriggerDetector activator, Trigger trigger)
    {
        GameState.SkipToTime(hour, minute);
    }
}
