using System;
using Godot;

[GlobalClass]
public partial class GemReward : StageReward
{
    [Export] private Texture2D gemTexture;
    [Export] private int amount = 100;

    public override void ReceiveReward() 
    {
        GameState.AddPremiumCurrency(amount);
    }

    public override Texture2D GetTexture() 
    { 
        return gemTexture;  
    }
}
