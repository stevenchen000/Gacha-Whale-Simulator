using Godot;
using Godot.Collections;
using System;
using EventSystem;
using CombatSystem;

public partial class CustomGameCharacter : GameCharacter
{
	public string portraitFilename { get; private set; } = "";

	public override CharacterPortrait GetPortrait()
    {
        var result = CustomCharacterManager.GetCustomPortrait(portraitFilename);
        return result;
    }
    public void SetPortrait(CharacterPortrait portrait)
    {
        portraitFilename = portrait.ResourcePath;
    }
    public void SetCharacterName(string newName) { Name = newName; }
    public void SetTitle(string newTitle) { Title = newTitle; }
    public void SetDescription(string newDescription) { Description = newDescription; }
    public void SetWorld(string newWorld) { World = newWorld; }
    public void SetElement(Element newElement)
    {
        Element = newElement;
    }
    public void SetClass(CharacterClass newClass)
    {
        Class = newClass;
    }

    /*
    public override Array<CharacterSkill> GetCharacterSkills()
    {
        return 
    }*/
}
