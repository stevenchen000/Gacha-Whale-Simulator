using Godot;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class StatType : Resource
    {
        [Export] public string StatName { get; private set; }
        [Export] public int BaseAmount { get; private set; } = 1;
        /// <summary>
        /// For stats like health
        /// </summary>
        [Export] public bool IsSlidingStat { get; private set; } = false;

        [ExportCategory("Description")]
        [Export(PropertyHint.MultilineText)] private string description;
    }
}