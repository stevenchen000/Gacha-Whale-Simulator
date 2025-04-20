using CombatSystem;
using Godot;
using Godot.Collections;
using System;

namespace SkillSystem
{
    [GlobalClass]
    public partial class CastSkillEffect : BaseEffect
    {
        [Export] private CharacterSkill skill;
        [Export(PropertyHint.MultilineText)] private string description;

        public override void ActivateEffect(BattleCharacter caster,
                                            BattleCharacter target,
                                            EffectContainer container)
        {
            var turn = container.Battle.turnData;
            AddSkill(turn, caster, target);
        }

        public override void DeactivateEffect(BattleCharacter caster,
                                              BattleCharacter target,
                                              EffectContainer container)
        {
            
        }


        private void AddSkill(TurnData turn, BattleCharacter caster, BattleCharacter target)
        {
            var targets = new Array<BattleCharacter>();
            targets.Add(target);
            turn.AddSkillCast(caster, targets, skill);
        }
    }
}