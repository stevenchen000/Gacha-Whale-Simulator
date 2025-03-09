using CombatSystem;
using Godot;
using System;

public partial class StageSelectButton : Node
{
    [Export] private StageData data;
    private SimpleWeakRef<MainGame> game;
    

    public override void _Ready()
    {
        base._Ready();
        var tempGame = Utils.FindParentOfType<MainGame>(this);
        game = new SimpleWeakRef<MainGame>(tempGame);
    }

    public void SetStageData(StageData data)
    {
        this.data = data;
    }

    public void OnClick()
    {
        if (data.PlayerHasEnoughStamina())
        {
            SelectStage();
        }
        else
        {
            Utils.Print(this, "Not enough stamina");
        }
    }

    private void SelectStage()
    {
        game.Value.SetStageData(data);
        game.Value.OpenBattleMenu();
    }
}
