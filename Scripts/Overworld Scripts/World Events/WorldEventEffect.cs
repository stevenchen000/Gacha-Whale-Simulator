using Godot;
using System;


public partial class WorldEventEffect : Resource
{
    [Export] private string description;

    public virtual void ActivateEffect() { }
}
