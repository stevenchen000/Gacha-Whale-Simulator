using CombatSystem;
using Godot;
using Godot.Collections;
using System;

namespace SkillSystem
{
    [GlobalClass]
    public partial class StatusEffect : Resource
    {
        [Export] public int ID { get; private set; } = 1;
        [Export] public string Name { get; private set; }
        [Export] public bool IsUnique { get; private set; } = false;
        [Export] public Texture2D Texture { get; private set; }

        [ExportCategory("Effects")]
        [Export] public Array<BaseEffect> Effects { get; private set; } = new Array<BaseEffect>();
        [Export] public int Duration { get; private set; } = 5;
        [Export] public bool IsBuff { get; private set; } = true;
        /// <summary>
        /// This is for generic buffs/debuffs only
        /// </summary>
        [Export] public int Level { get; private set; } = 1;
    }
}