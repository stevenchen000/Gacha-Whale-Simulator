using Godot;
using System;
using Godot.Collections;
using EventSystem;

public partial class OwnedGachaCharactersMenu : Control
{
	[Export] private Node scrollArea;
	[Export] private Array<CharacterBoxDisplay> boxes;
	[Export] private string rowSceneLocation = "res://Scenes/UI/Gacha Window/gacha_display_row.tscn";

	[Export] private VoidEvent OnCharacterListUpdated;

	public override void _Ready()
	{
		boxes = new Array<CharacterBoxDisplay>();
		AddAllChildrenNodes();
		AddAllMissingRows();
		RevealProperNumberOfBoxes();
		UpdateAllPortraits();
		
		OnCharacterListUpdated.SubscribeEvent(UpdateUI);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateUI()
    {
		AddAllMissingRows();
		RevealProperNumberOfBoxes();
		UpdateAllPortraits();
	}

	private void UpdateAllPortraits(){
		int numOfCharacters = GameState.GetNumberOfCharacters();
		var characters = GameState.GetAllOwnedCharacters();
		for(int i = 0; i < numOfCharacters; i++)
        {
			boxes[i].UpdatePortrait(characters[i]);
        }
	}

	private void RevealProperNumberOfBoxes()
    {
		int numOfCharacters = GameState.GetNumberOfCharacters();
		int numOfBoxes = boxes.Count;

		for(int i = 0; i < numOfBoxes; i++)
        {
			if(i < numOfCharacters)
            {
				boxes[i].Visible = true;
            }
            else
            {
				boxes[i].Visible = false;
            }
        }
	}

	private void AddRow()
    {
		var scene = GD.Load<PackedScene>(rowSceneLocation);
		var row = scene.Instantiate();
		scrollArea.AddChild(row);
		var children = row.GetChildren();

		foreach (var child in children)
		{
			boxes.Add((CharacterBoxDisplay)child);
		}
	}

	private void AddAllMissingRows()
    {
		int numOfCharacters = GameState.GetNumberOfCharacters();
		int numOfBoxes = boxes.Count;
		int numOfMissingBoxes = numOfCharacters - numOfBoxes;

		for(int i = 0; i <= numOfMissingBoxes/4; i++)
        {
			AddRow();
        }
	}

	private void AddAllChildrenNodes()
    {
		if(scrollArea != null)
        {
			var nodes = scrollArea.GetChildren();
			foreach(var row in nodes)
            {
				var boxes = row.GetChildren();
				foreach(var box in boxes)
                {
					this.boxes.Add((CharacterBoxDisplay)box);
                }
            }
        }
    }
}
