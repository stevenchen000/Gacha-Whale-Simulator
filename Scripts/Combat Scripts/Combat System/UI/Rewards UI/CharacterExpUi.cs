using Godot;
using System;

namespace CombatSystem
{
    public partial class CharacterExpUi : Control
    {
        [Export] private CharacterPortraitDisplay characterDisplay;
        [Export] private ProgressBar expBar;
        [Export] private Label levelText;
        private LevelUpData data;
        private double previousPercent;
        private double percentDiff;
        [Export] private double minFillSpeed = 30;
        private double currDiffAddition = 0;
        private int currLevel;


        private bool isUpdating = false;
        public bool HasFinished { get; private set; } = false;


        public void Init(LevelUpData data)
        {
            this.data = data;
            SetupDisplay();
            UpdateLevelText(data.PreviousLevel);
            CalculatePercentageDifference();
            DelayedCalls.AddCall(0.1, StartAnimation);
        }

        public override void _Process(double delta)
        {
            if (isUpdating)
            {
                currDiffAddition += CalculateAddSpeed(delta);
                currDiffAddition = Math.Min(currDiffAddition, percentDiff);
                expBar.Value = (previousPercent + currDiffAddition) % 100;

                CalculateCurrLevel();
                UpdateLevelText(currLevel);
                if (currDiffAddition == percentDiff)
                {
                    isUpdating = false;
                    HasFinished = true;
                }
            }
        }

        private double CalculateAddSpeed(double delta)
        {
            double minAmount = minFillSpeed * delta;
            double amount = percentDiff / 5 * delta;

            return Math.Max(minAmount, amount);
        }

        private void SkipAnimation()
        {
            if (isUpdating)
            {
                isUpdating = false;
                HasFinished = true;
                expBar.Value = (previousPercent + percentDiff) % 100;
                currLevel = data.NewLevel;
                UpdateLevelText(currLevel);
            }
        }

        private void StartAnimation()
        {
            isUpdating = true;
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

        private void CalculateCurrLevel()
        {
            int prevLevel = data.PreviousLevel;
            int currLevelIncrease = (int)(data.PreviousExpPercent + percentDiff) / 100;
            currLevel = prevLevel + currLevelIncrease;
        }

        private void SetupDisplay()
        {
            var character = data.Character;
            var portrait = character.GetPortrait();
            characterDisplay.UpdatePortrait(portrait);
        }

        private void UpdateLevelText(int level)
        {
            levelText.Text = $"Level {level}";
        }
    }
}