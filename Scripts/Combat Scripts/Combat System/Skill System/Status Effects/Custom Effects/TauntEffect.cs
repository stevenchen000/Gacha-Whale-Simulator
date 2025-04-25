using CombatSystem;
using Godot;
using System;

namespace SkillSystem
{
    [Tool]
    [GlobalClass]
    public partial class TauntEffect : BaseEffect
    {
        [Export(PropertyHint.MultilineText)] private string description;

        public override void ActivateEffect(BattleCharacter caster,
                                            BattleCharacter target,
                                            EffectContainer container)
        {
            target.Taunt(caster);
        }

        public override void DeactivateEffect(BattleCharacter caster,
                                              BattleCharacter target,
                                              EffectContainer container)
        {
            target.Untaunt();
        }
    }
}