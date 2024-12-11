using Godot;
using System;

[GlobalClass]
public partial class DeleteObjectTriggerEffect : TriggerEffect
{
    public override void ActivateEffect(TriggerDetector activator, Trigger trigger)
    {
        var parent = trigger.GetParent();
        parent.QueueFree();
    }
}
