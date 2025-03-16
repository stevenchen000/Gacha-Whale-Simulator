using CombatSystem;
using Godot;
using System;

namespace SkillSystem
{
    [GlobalClass]
    public partial class BaseEffect : Resource
    {
        [Export] public EffectTiming Timing { get; protected set; }
        [Export] public float Potency { get; protected set; }

        public virtual void ActivateEffect(BattleCharacter caster, 
                                           BattleCharacter target,
                                           EffectContainer container)
        {

        }

        public virtual void DeactivateEffect(BattleCharacter caster,
                                             BattleCharacter target,
                                             EffectContainer container)
        {

        }
    }
}