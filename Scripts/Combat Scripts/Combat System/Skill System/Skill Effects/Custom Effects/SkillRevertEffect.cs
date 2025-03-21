using Godot;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class SkillRevertEffect : SkillEffect
    {
        [Export] private int skillIndex = -1;

        protected override void _StartEffect(TurnData data)
        {
            RevertSkill(data);
        }

        protected override bool _RunEffect(TurnData data, TimeHandler timer)
        {
            return timer.TimeIsUp(delay);
        }

        protected override void _EndEffect(TurnData data)
        {
            
        }

        private void RevertSkill(TurnData data)
        {
            var character = data.caster;
            character.Skills.RevertToOriginal(skillIndex);
        }
    }
}
