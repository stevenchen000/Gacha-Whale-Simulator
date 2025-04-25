using Godot;
using Godot.Collections;
using System;
using EventSystem;
using CombatSystem;

[Tool]
[GlobalClass]
public partial class GameCharacter : Resource
{
    [Export] public int ID { get; protected set; }
	[Export] private CharacterPortrait portrait { get; set; }
    [Export] public CharacterRarity BaseRarity { get; protected set; }
    [Export] public string Name { get; protected set; }
    [Export] public string Title { get; protected set; }
    [Export] public string World { get; protected set; }
    [Export] public Element Element { get; protected set; }
    [Export] public CharacterClass Class { get; protected set; }
    [Export] public CharacterRole Role { get; private set; }
    [Export] public SkillLoadout SkillSet { get; protected set; }
    [Export] public CharacterAI EnemyAI { get; protected set; }
    [Export] public PowercreepLevelData Powercreep { get; protected set; }
    [Export(PropertyHint.MultilineText)] public string Description { get; protected set; }

    public WeaponType Weapon 
    { 
        get
        {
            return Class.Weapon;
        }
    }



    public virtual CharacterPortrait GetPortrait()
    {
        return portrait;
    }



    public virtual Array<CharacterSkill> GetSkills(CharacterRarity currRarity,
                                                   int stars)
    {
        return SkillSet.GetSkillsAtLB(currRarity, stars);
    }
}
