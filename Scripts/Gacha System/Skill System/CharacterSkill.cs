using Godot;
using System;
using Godot.Collections;

[GlobalClass]
public partial class CharacterSkill : Resource
{
    [Export] public Array<SkillEffect> effects;
}
