using Godot;
using System;
using Godot.Collections;

namespace CombatSystem {
    [GlobalClass]
    public partial class CharacterSkill : Resource
    {
        [Export] public Array<SkillEffect> effects;
        [Export] public float duration = 2f;
        [Export] public int potency = 100;
        [Export] public GridShape attackArea;

        public void CastSkill(BattleCharacter target, BattleCharacter caster)
        {
            var targetStats = target.stats;
            var casterStats = caster.stats;
            targetStats.TakeDamage(casterStats, potency);
        }
    }
}