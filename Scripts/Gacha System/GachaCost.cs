using Godot;
using System;

namespace GachaSystem
{
    [GlobalClass]
    public partial class GachaCost : Resource
    {
        [Export] public UpgradeCurrency currency;
        [Export] public int cost;
    }
}