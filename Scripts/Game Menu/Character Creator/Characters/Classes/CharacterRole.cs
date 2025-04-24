using Godot;
using System;
using EventSystem;
using CombatSystem;

[Tool]
[GlobalClass]
public partial class CharacterRole : Resource
{
	[Export] public string Name { get; private set; }
	[Export] public Texture2D Texture { get; private set; }
	[Export(PropertyHint.MultilineText)] public string Description { get; private set; }

	[Export] public RoleStatGrowth Stats { get; private set; }
	[Export] public SkillBranch AmpAttackBranch { get; private set; }
	[Export] public SkillBranch HpAttackBranch { get; private set; }

}
