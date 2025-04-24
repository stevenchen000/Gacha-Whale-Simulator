using CombatSystem;
using Godot;
using Godot.Collections;
using System;

[Tool]
public partial class EnemyPartyCreator : Node
{
    [Export] private string saveLocation;
    [Export] private int enemyLevel = 1;
    [Export] private int gloryLevel = 1;
    [Export] private int minCharacters = 2;
    [Export] private int maxCharacters = 5;
    [Export] private Array<GameCharacter> characterPool = new();
    private Random random = new Random(Guid.NewGuid().GetHashCode());

    [ExportCategory("Save")]
    [Export] private bool save = false;

    public override void _Process(double delta)
    {
        if (Engine.IsEditorHint())
        {
            if (save)
            {
                save = false;
                CreateParties();
            }
        }
    }

    private void CreateParties()
    {
        var party = CreateParty();
        string filename = $"{saveLocation}lvl_{enemyLevel}_glory_{gloryLevel}_{random.Next()}.tres";
        party.ResourcePath = filename;
        Utils.Print(this, filename);
        ResourceSaver.Save(party, filename);
    }

    private StageParty CreateParty()
    {
        int diff = maxCharacters - minCharacters;
        int rand = random.Next(diff + 1) + minCharacters;
        var party = new Array<CharacterData>();

        for(int i = 0; i < rand; i++)
        {
            var character = GetRandomCharacter();
            var charData = new CharacterData(character, enemyLevel);
            party.Add(charData);
        }

        return new StageParty(party, gloryLevel);
    }

    private GameCharacter GetRandomCharacter()
    {
        int rand = random.Next(characterPool.Count);
        return characterPool[rand];
    }
}
