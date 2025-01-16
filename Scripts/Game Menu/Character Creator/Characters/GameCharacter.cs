using Godot;
using System;
using EventSystem;
using CombatSystem;

[GlobalClass]
public partial class GameCharacter : Resource
{
	private CharacterPortrait portrait;
    [Export] public string Name { get; protected set; }
    [Export] public string Title { get; protected set; }
    [Export] public string Description { get; protected set; }
    [Export] public string World { get; protected set; }
    [Export] public Element Element { get; protected set; }
    [Export] public CharacterClass Class { get; protected set; }
    [Export] public CharacterRole Role { get; protected set; }
    private CharacterSkill basicAttack;
    private CharacterSkill skill1;
    private CharacterSkill skill2;
    private CharacterSkill ultimateSkill;


    public virtual CharacterPortrait GetPortrait()
    {
        return portrait;
    }

    public virtual CharacterSkill GetBasicAttack() { return basicAttack; }
    public virtual CharacterSkill GetSkill1() { return skill1; }
    public virtual CharacterSkill GetSkill2() { return skill2; }
    public virtual CharacterSkill GetUltimateSkill() { return ultimateSkill; }
}
