using System;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    public class StageCompletionData
    {
        public int PrevStars { get; private set; }
        public int NewStars { get; private set; }
        public Array<StageReward> Rewards { get; private set; }
        public int Exp { get; private set; }


        public StageCompletionData(int prevStars, int newStars, Array<StageReward> rewards, int exp) 
        { 
            PrevStars = prevStars;
            NewStars = newStars;
            Rewards = new Array<StageReward>();
            Rewards.AddRange(rewards);
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
