using EventSystem;
using Godot;
using Godot.Collections;
using System;

public partial class PlayerRankManager : Node, ISavable
{
    public int Rank { get; private set; } = 1;
    public int RankExp { get; private set; }
    [Export] private Array<int> ExpToNextRank;

    [Export] private GameEvent<int> OnRankUp;

    public void AddRankExp(int exp)
    {
        RankExp += exp;
        int levelsGained = UpdateRank();

        OnRankUp.RaiseEvent(this, levelsGained);
    }

    private int UpdateRank()
    {
        int expToLevel = GetExpToNextRank();
        int levelsGained = 0;

        while(RankExp >= expToLevel)
        {
            RankExp -= expToLevel;
            levelsGained++;
        }

        return levelsGained;
    }

    public int GetExpToNextRank()
    {
        int index = Rank;
        int result = 0;

        if(ExpToNextRank.Count > index)
        {
            result = ExpToNextRank[index];
        }

        return result;
    }



    /**********************
     * Save and Load
     * *********************/

    private string rankFlagName = "player_rank";
    private string rankExpFlagName = "rank_exp";


    public void Save()
    {
        GameState.SetFlag(rankFlagName, Rank);
        GameState.SetFlag(rankExpFlagName, RankExp);
    }

    public void Load()
    {
        int newRank = GameState.GetFlagAmount(rankFlagName);

        if(newRank > 0)
        {
            Rank = newRank;
            RankExp = GameState.GetFlagAmount(rankExpFlagName);
        }
        else
        {
            Rank = 1;
            RankExp = 0;
        }
    }
}
