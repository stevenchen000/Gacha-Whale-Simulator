using Godot;
using System;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class KnockbackEffect : SkillEffect
    {
        [Export] private float duration = 0.5f;
        [Export] private bool knockInSkillDirection = true;
        [Export] private int numofSpaces = 1;
        

        protected override void _StartEffect(TurnData data, SkillCastData skillCast)
        {
            ApplyKnockback(data, skillCast);
        }

        protected override bool _RunEffect(TurnData data, SkillCastData skillCast, TimeHandler timer)
        {
            return true;
        }

        protected override void _EndEffect(TurnData data, SkillCastData skillCast)
        {
            
        }

        private void ApplyKnockback(TurnData data, SkillCastData skillCast)
        {
            var targetSelection = skillCast.Selection;

            if (targetSelection == null && targetSelection.Style != TargetSelectionStyle.SelectDirection) return;
            var caster = skillCast.Caster;
            var targets = GetTargets(skillCast);
            foreach (var target in targets)
            {
                data.Battle.ApplyKnockback(target, numofSpaces, targetSelection.SelectedDirection);
            }
        }
    }
}
