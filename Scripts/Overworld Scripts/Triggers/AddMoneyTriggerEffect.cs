using Godot;
using System;

[GlobalClass]
public partial class AddMoneyTriggerEffect : TriggerEffect
{
    [Export] private int money;

    public override void ActivateEffect(TriggerDetector activator, Trigger trigger)
    {
        GameState.AddMoney(money);
    }
}
