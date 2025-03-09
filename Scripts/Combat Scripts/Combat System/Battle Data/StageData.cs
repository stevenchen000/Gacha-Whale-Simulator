using Godot;
using Godot.Collections;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class StageData : Resource
    {
        [Export] public int ID { get; private set; }
        [Export] public string StageName { get; private set; }
        [Export] public bool IsUnlocked { get; private set; } = false;
        [Export] public int StaminaCost { get; private set; } = 1;
        [Export] private bool isSkippable = true;


        [ExportCategory("Combat Data")]
        [Export] public int RecommendedLevel { get; private set; } = 1;
        [Export] public Array<CharacterData> EnemyList { get; private set; }
        [Export] public Array<Vector2I> PlayerStartPositions { get; private set; }
        [Export] public Array<Vector2I> EnemyStartPositions { get; private set; }
        [Export] public Vector2I GridSize { get; private set; }

        [ExportCategory("Rewards")]
        [Export] public int ExpGained = 1000;
        [Export] public int MaxStars = 3;
        [Export] public StageStarCondition OneStarCondition;
        [Export] public StageReward OneStarReward;
        [Export] public StageStarCondition TwoStarCondition;
        [Export] public StageReward TwoStarReward;
        [Export] public StageStarCondition ThreeStarCondition;
        [Export] public StageReward ThreeStarReward;
        [Export] public Array<StageReward> Rewards { get; private set; }


        [ExportCategory("Player Party override")]
        [Export] public bool OverridePlayerParty { get; private set; }
        [Export] public Array<CharacterData> PlayerParty { get; private set; }



        //first time rewards
        //recurring rewards



        public void SetupDisplay()
        {
            //Unsure if code should be here or the display itself...
            //Need to find clean way to display combat nodes and story nodes
        }


        public StageCompletionData CheckRewards(BattleState state)
        {
            int currStars = GetStarCount();
            int newStars = CalculateNewStarCount(state);

            var rewards = CalculateRewardsForStars(currStars, newStars);
            SetStarCount(newStars);

            var eventArgs = new StageCompletionData(currStars, newStars, rewards, ExpGained);
            return eventArgs;
        }

        private Array<StageReward> CalculateRewardsForStars(int currStars, int newStars)
        {
            Array<StageReward> result = new Array<StageReward>();

            for (int i = currStars + 1; i <= newStars; i++)
            {
                switch (i)
                {
                    case 1:
                        result.Add(OneStarReward);
                        break;
                    case 2:
                        result.Add(TwoStarReward);
                        break;
                    case 3:
                        result.Add(ThreeStarReward);
                        break;
                }
            }

            return result;
        }

        private int CalculateNewStarCount(BattleState state)
        {
            int starCount = 0;

            if(OneStarCondition.ConditionCleared(state)) starCount++;
            if(TwoStarCondition.ConditionCleared(state)) starCount++;
            if(ThreeStarCondition.ConditionCleared(state)) starCount++;

            return starCount;
        }



        public void SetStarCount(int stars)
        {
            if (stars > 0)
                GameState.SetFlag(GetStageFlagName(), stars);
        }

        public int GetStarCount()
        {
            return GameState.GetFlagAmount(GetStageFlagName());
        }

        public bool PlayerHasEnoughStamina()
        {
            return GameState.HasEnoughStamina(StaminaCost);
        }

        public bool CanBeSkipped()
        {
            int starsEarned = GameState.GetFlagAmount(GetStageFlagName());
            return starsEarned > 0 && isSkippable;
        }

        private string GetStageFlagName()
        {
            return $"battle_stage_{ID}_stars";
        }
    }
}