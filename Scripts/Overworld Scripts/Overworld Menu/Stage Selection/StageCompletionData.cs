using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    public class StageCompletionData
    {
        public int PrevStars { get; private set; }
        public int NewStars { get; private set; }
        public List<StageReward> Rewards { get; private set; } = new List<StageReward>();
        public int Exp { get; private set; }
        public List<LevelUpData> LevelUps { get; private set; } = new List<LevelUpData>();


        public StageCompletionData(int prevStars, int newStars, 
                                   Array<StageReward> rewards, 
                                   List<LevelUpData> levelUps, int exp) 
        { 
            PrevStars = prevStars;
            NewStars = newStars;
            Rewards.AddRange(rewards);
            LevelUps.AddRange(levelUps);
            Exp = exp;
        }

        public void ReceiveRewards()
        {
            foreach (var item in Rewards)
            {
                item.ReceiveReward();
            }
        }
    }
}
