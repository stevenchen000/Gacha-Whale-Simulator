using Godot;
using System;
using GachaSystem;

namespace EventSystem
{
    [GlobalClass]
    public partial class GachaCharacterEvent : GameEvent<GachaCharacter>
    {
        [Export] private string description;
    }
}