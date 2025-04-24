using Godot;
using System;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class _TemplateEffect : SkillEffect
    {

        protected override void _StartEffect(TurnData data, SkillCastData skillCast)
        {
            
        }

        protected override bool _RunEffect(TurnData data, SkillCastData skillCast, TimeHandler timer)
        {
            return true;
        }

        protected override void _EndEffect(TurnData data, SkillCastData skillCast)
        {
            
        }
    }
}
