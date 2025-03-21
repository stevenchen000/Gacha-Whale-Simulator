using Godot;
using Godot.Collections;
using System;
using EventSystem;
using CombatSystem;

[GlobalClass]
public partial class GameCharacter : Resource
{
    [Export] public int ID { get; protected set; }
	[Export] private CharacterPortrait portrait { get; set; }
    [Export] public CharacterRarity BaseRarity { get; protected set; }
    [Export] public string Name { get; protected set; }
    [Export] public string Title { get; protected set; }
    [Export] public string Description { get; protected set; }
    [Export] public string World { get; protected set; }
    [Export] public Element Element { get; protected set; }
    [Export] public CharacterClass Class { get; protected set; }
    [Export] public SkillLoadout SkillSet { get; protected set; }
    [Export] public CombatAI EnemyAI { get; protected set; }
    
    public WeaponType Weapon 
    { 
        get
        {
            return Class.Weapon;
        }
    }
    public CharacterRole Role
    {
        get
        {
            return Class.Role;
        }
    }

    [Export] private int movement;


    public virtual CharacterPortrait GetPortrait()
    {
        return portrait;
    }

    public int GetMovement() { return movement; }



    public virtual Array<CharacterSkill> GetSkills()
    {
        if (SkillSet != null)
            return SkillSet.GetSkillsAtLevel(0);
        else
            return null;
    }

    public virtual Array<CharacterSkill> GetSkills(CharacterRarity baseRarity,
                                                   CharacterRarity currRarity,
                                                   int stars)
    {
        return SkillSet.GetSkillsAtLB(baseRarity, currRarity, stars);
    }
}
