using Godot;
using Godot;
using InventorySystem;
using System;
using System.Collections.Generic;

public partial class CombatRewardDisplay : Node
{
    private List<StageReward> earnedRewards = new List<StageReward>();
    [Export] private PackedScene itemDisplay;
    [Export] private Node displayParent;
    private List<RewardItemDisplay> displays = new List<RewardItemDisplay>();
    [Export] private Button skipButton;

    private bool isDisplaying = false;
    private bool finishedDisplaying = false;
    [Export] private double itemPopInPeriod = 0.2;
    private int displayIndex = 0;
    private TimeHandler time = new TimeHandler();

    public void Init(List<StageReward> rewards)
    {
        RemoveOldDisplays();
        SetupDisplays(rewards);
        DelayedCalls.AddCall(0.5, () => isDisplaying = true);
    }

    public override void _Process(double delta)
    {
        if (isDisplaying)
        {
            time.Tick(delta);
            bool intervalReached = time.IntervalsBetween(itemPopInPeriod) > 0;
            if (intervalReached)
            {
                displays[displayIndex].PopIn();
                displayIndex++;
            }
            if (displayIndex >= displays.Count)
            {
                isDisplaying = false;
                finishedDisplaying = true;
            }
        }
    }

    public void OnClick()
    {
        if (isDisplaying)
        {
            isDisplaying = false;
            finishedDisplaying = true;
            for(int i = displayIndex; i < displays.Count; i++)
            {
                var display = displays[i];
                display.PopIn();
            }
        }else if (finishedDisplaying)
        {
            QueueFree();
        }
    }

    private void DisableSkipButton()
    {
        skipButton.MouseFilter = Control.MouseFilterEnum.Ignore;
    }



    private void SetupDisplays(List<StageReward> rewards)
    {
        foreach(var reward in rewards)
        {
            var newDisplay = Utils.InstantiateCopy<RewardItemDisplay>(itemDisplay);
            displayParent.AddChild(newDisplay);
            displays.Add(newDisplay);
            newDisplay.SetAmount(reward.GetAmount());
            newDisplay.SetItemIcon(reward.GetTexture());
            newDisplay.HideIcon();
        }
    }

    private void RemoveOldDisplays()
    {
        var children = displayParent.GetChildren();
        foreach(var child in children)
        {
            child.QueueFree();
        }
    }
}
