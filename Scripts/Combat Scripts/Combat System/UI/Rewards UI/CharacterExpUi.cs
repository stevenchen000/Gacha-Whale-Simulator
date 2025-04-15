using Godot;
using System;

namespace CombatSystem
{
    public partial class CharacterExpUi : Node
    {
        [Export] private CharacterPortraitDisplay characterDisplay;
        [Export] private ProgressBar expBar;
        [Export] private Label levelText;
        private LevelUpData data;
        private double previousPercent;
        private double percentDiff;
        private double currDiffAddition = 0;


        private bool isUpdating = false;
        private bool hasFinished = false;


        public void Init(LevelUpData data)
        {
            this.data = data;
            CalculatePercentageDifference();
        }

        public override void _Process(double delta)
        {
            if (isUpdating)
            {
                currDiffAddition += percentDiff / 5 * delta;
                currDiffAddition = Math.Min(currDiffAddition, percentDiff);
                expBar.Value = (previousPercent + currDiffAddition) % 100;

                if(currDiffAddition == percentDiff)
                {
                    isUpdating = false;
                    hasFinished = true;
                }
            }
        }

        private void CalculatePercentageDifference()
        {
            int prevLevel = data.PreviousLevel;
            int newLevel = data.NewLevel;
            double prevExp = data.PreviousExpPercent + 100 * prevLevel;
            double newExp = data.NewExpPercent + 100 * newLevel;
            double prevValue = data.PreviousExpPercent;

            expBar.Value = prevValue;
            previousPercent = prevValue;
            percentDiff = newExp - prevExp;
        }
    }
}