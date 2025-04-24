using Godot;
using System;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class HealStatByDamageEffect : SkillEffect
    {
        [Export] private bool healAmp = true;
        [Export] private double percentHealing = 10;
        [Export] private bool useAmpDamage = true;

        protected override void _StartEffect(TurnData data, SkillCastData skillCast)
        {
            HealStat(data, skillCast);
        }

        protected override bool _RunEffect(TurnData data, SkillCastData skillCast, TimeHandler timer)
        {
            return true;
        }

        protected override void _EndEffect(TurnData data, SkillCastData skillCast)
        {
            
        }

        private void HealStat(TurnData data, SkillCastData skillCast)
        {
            var targets = GetTargets(skillCast);
            int damage = 0;

            if (useAmpDamage) damage = data.totalAmpDamageDealt;
            else damage = data.totalHpDamageDealt;

                int healing = (int)(damage * percentHealing / 100);
            foreach(var target in targets)
            {
                if (healAmp)
                    target.BatteryAmp(healing);
                
            }
        }
    }
}
