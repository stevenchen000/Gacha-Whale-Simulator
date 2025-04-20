using Godot;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class ConditionalSkillEffect : SkillEffect
    {
        //insert condition here
        //Skill effect

        protected override void _StartEffect(TurnData data, SkillCastData skillCast)
        {
            
        }

        protected override bool _RunEffect(TurnData data, SkillCastData skillCast, TimeHandler timer)
        {
            return timer.TimeIsUp(delay);
        }

        protected override void _EndEffect(TurnData data, SkillCastData skillCast)
        {
            
        }

    }
}
