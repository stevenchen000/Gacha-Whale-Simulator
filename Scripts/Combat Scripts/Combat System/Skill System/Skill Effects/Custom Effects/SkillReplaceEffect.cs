using Godot;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class SkillReplaceEffect : SkillEffect
    {
        [Export] private CharacterSkill skill;
        [Export] private int skillIndex = -1;

        protected override void _StartEffect(TurnData data)
        {
            ReplaceSkill(data);
        }

        protected override bool _RunEffect(TurnData data, TimeHandler timer)
        {
            return timer.TimeIsUp(delay);
        }

        protected override void _EndEffect(TurnData data)
        {
            
        }

        private void ReplaceSkill(TurnData data)
        {
            var character = data.caster;
            character.Skills.ReplaceSkill(skillIndex, skill);
        }
    }
}
