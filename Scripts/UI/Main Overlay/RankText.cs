using Godot;
using System;

public partial class RankText : Node
{
    [Export] private Label rankText;


    public override void _Ready()
    {
        CallDeferred(MethodName.UpdateText, 0);
    }

    public void UpdateText(int ranksGained)
    {
        int rank = GameState.GetRankManager().Rank;
        rankText.Text = $"{rank}";
    }
}
