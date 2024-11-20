using Godot;
using System;

namespace GachaSystem.Dungeons{
    [GlobalClass]
    public partial class Dungeon : Resource
    {
        [Export] private string dungeonName;
        [Export] private int floorsPerChapter = 20;
        [Export] private int premiumCurrencyPerFloor = 100;
        [Export] private int baseAccountPowerNeeded = 1000;
        [Export] private float accountPowerInflationPerFloor = 0.05f;
        [Export] private float accountPowerInflationPerUpdate = 2.0f;
    }
}