using Godot;
using System;
using QuestSystem;

[GlobalClass]
public partial class WorldEffectGiveQuest : WorldEventEffect
{
    [Export] private BaseQuest quest;

    public override void ActivateEffect()
    {
        bool hasQuest = GameState.PlayerHasQuest(quest);
        if (!hasQuest)
        {
            GameState.AddQuest(quest);
        }
        else
        {
            GD.Print("You already have that quest!");
        }
    }
}
