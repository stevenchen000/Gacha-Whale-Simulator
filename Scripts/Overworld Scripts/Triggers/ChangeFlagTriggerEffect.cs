using Godot;
using System;

[GlobalClass]
public partial class ChangeFlagTriggerEffect : TriggerEffect
{
    [Export] private string flagName;
    [Export] private int changeAmount;
    [Export] private bool setValue;

    public override void ActivateEffect(TriggerDetector activator, Trigger trigger)
    {
        if (setValue)
        {
            GameState.SetFlag(flagName, changeAmount);
        }
        else
        {
            GameState.AddFlag(flagName, changeAmount);
        }
        //GD.Print($"{flagName} = {GameState.GetFlagAmount(flagName)}");
    }
}
