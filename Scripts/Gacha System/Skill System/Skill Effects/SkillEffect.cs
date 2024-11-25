using Godot;
using System;

[GlobalClass]
public partial class SkillEffect : Resource
{
    [Export] public float delay = 0f;
    public virtual void RunEffect() { }
}
