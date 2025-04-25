using CombatSystem;
using Godot;
using System;

namespace SkillSystem
{
    [Tool]
    [GlobalClass]
    public partial class AddSlidingStatEffect : BaseEffect
    {
        [Export] private StatType stat;
        [Export] private float amount = 100f;

        [Export(PropertyHint.MultilineText)] private string description;

        public override void ActivateEffect(BattleCharacter caster,
                                            BattleCharacter target,
                                            EffectContainer container)
        {
            target.Stats.AddSlidingStat(stat, (int)amount);
        }

        public override void DeactivateEffect(BattleCharacter caster,
                                              BattleCharacter target,
                                              EffectContainer container)
        {
        }
    }
}