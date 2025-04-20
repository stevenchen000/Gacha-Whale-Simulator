using CombatSystem;
using Godot;
using System;

namespace SkillSystem
{
    [GlobalClass]
    public partial class _TemplateEffect : BaseEffect
    {
        [Export(PropertyHint.MultilineText)] private string description;

        public override void ActivateEffect(BattleCharacter caster,
                                            BattleCharacter target,
                                            EffectContainer container)
        {
            
        }

        public override void DeactivateEffect(BattleCharacter caster,
                                              BattleCharacter target,
                                              EffectContainer container)
        {
            
        }
    }
}