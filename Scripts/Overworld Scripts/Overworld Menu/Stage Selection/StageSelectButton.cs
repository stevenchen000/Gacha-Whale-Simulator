using CombatSystem;
using Godot;
using Godot.Collections;
using System;

public partial class StageSelectButton : Node
{
    [Export] private StageData data;
    private SimpleWeakRef<MainGame> game;
    [Export] private TextureRect stageIcon;
    [Export] private Label stageName;
    [Export] private Array<RarityStar> clearStars;

    [ExportCategory("Held Button")]
    private double heldTime = 0;
    [Export] private double heldTimeForDescription;
    private bool isHeld = false;

    public override void _Ready()
    {
        base._Ready();
        var tempGame = Utils.FindParentOfType<MainGame>(this);
        game = new SimpleWeakRef<MainGame>(tempGame);
    }

    public override void _Process(double delta)
    {
        if (isHeld)
        {
            heldTime += delta;
            if(heldTime > heldTimeForDescription)
            {
                //Open stage description
            }
        }
    }

    public void SetStageData(StageData data)
    {
        this.data = data;

        if(clearStars != null) UpdateStars();
        if (stageName != null) UpdateName();
    }

    public void StartHolding()
    {
        heldTime = 0;
        isHeld = true;
    }

    public void OnClick()
    {

        if (isHeld && heldTime > heldTimeForDescription)
        {
            Utils.Print(this, "Description should have been shown!");
        }
        else if (data.PlayerHasEnoughStamina())
        {
            SelectStage();
        }
        else
        {
            Utils.Print(this, "Not enough stamina");
        }


        isHeld = false;
        Utils.Print(this, $"isHeld: {isHeld}");
    }

    private void SelectStage()
    {
        game.Value.SetStageData(data);
        game.Value.OpenBattleMenu();
    }

    /****************
     * Helpers
     * *************/

    private void UpdateStars()
    {
        int stars = data.GetStarCount();
        for(int i = 0; i < clearStars.Count; i++)
        {
            var starObj = clearStars[i];
            if (stars > i) starObj.SetActive(true);
            else starObj.SetActive(false);
        }
    }

    private void UpdateName()
    {
        stageName.Text = data.StageName;
    }
}
