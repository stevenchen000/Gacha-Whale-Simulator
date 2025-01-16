using Godot;
using System;
using EventSystem;
using CombatSystem;

public partial class CustomGameCharacter : GameCharacter
{
	public string portraitFilename { get; private set; } = "";

	public override CharacterPortrait GetPortrait()
    {
        var result = FileManager.GetCustomPortrait(portraitFilename);
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
    public void SetRole(CharacterRole newRole)
    {
        Role = newRole;
    }

    public override CharacterSkill GetBasicAttack() { return null; }
    public override CharacterSkill GetSkill1() { return null; }
    public override CharacterSkill GetSkill2() { return null; }
    public override CharacterSkill GetUltimateSkill() { return null; }


}
