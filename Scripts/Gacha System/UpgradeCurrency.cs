using Godot;
using System;

namespace GachaSystem{
    [GlobalClass]
    public partial class UpgradeCurrency : Resource
    {
        [Export] private string currencyName;
        [Export] private Texture2D currencyIcon;
        [Export] private string description;

    }
}