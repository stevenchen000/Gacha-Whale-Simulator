using Godot;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class ConditionalSkillEffect : SkillEffect
    {
        //insert condition here
        //Skill effect

        protected override void _StartEffect(TurnData data)
        {
            
        }

        protected override bool _RunEffect(TurnData data, TimeHandler timer)
        {
            return timer.TimeIsUp(delay);
        }

        protected override void _EndEffect(TurnData data)
        {
            
        }

    }
}
