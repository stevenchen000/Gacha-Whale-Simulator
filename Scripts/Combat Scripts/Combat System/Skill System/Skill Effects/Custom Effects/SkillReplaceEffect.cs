using Godot;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class SkillReplaceEffect : SkillEffect
    {
        [Export] private CharacterSkill skill;
        [Export] private int skillIndex = -1;

        protected override void _StartEffect(TurnData data, SkillCastData skillCast)
        {
            ReplaceSkill(data, skillCast);
        }

        protected override bool _RunEffect(TurnData data, SkillCastData skillCast, TimeHandler timer)
        {
            return timer.TimeIsUp(delay);
        }

        protected override void _EndEffect(TurnData data, SkillCastData skillCast)
        {
            
        }

        private void ReplaceSkill(TurnData data, SkillCastData skillCast)
        {
            var character = skillCast.Caster;
            character.Skills.ReplaceSkill(skillIndex, skill);
        }
    }
}
