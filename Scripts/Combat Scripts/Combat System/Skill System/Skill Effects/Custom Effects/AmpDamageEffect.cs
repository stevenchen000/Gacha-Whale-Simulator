using Godot;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class AmpDamageEffect : SkillEffect
    {
        [Export] private float duration = 0.2f;
        [Export] private int numberOfHits = 1;
        [Export] private BaseDamageFormula formula;

        protected override void _StartEffect(TurnData data, SkillCastData skillCast)
        {
            //DealDamage(data);
        }

        protected override bool _RunEffect(TurnData data, SkillCastData skillCast, TimeHandler timer)
        {
            int intervals = timer.IntervalsBetween(duration);
            while (intervals > 0)
            {
                intervals--;
                DealDamage(data, skillCast);
            }

            return timer.TimeIsUp(delay + duration*numberOfHits);
        }

        protected override void _EndEffect(TurnData data, SkillCastData skillCast)
        {
            
        }

        
        private void DealDamage(TurnData data, SkillCastData skillCast)
        {
            var caster = skillCast.Caster;
            var targets = GetTargets(skillCast);

            foreach(var target in targets)
            {
                DealDamageToTarget(data, skillCast, caster, target);
            }
        }

        private void DealDamageToTarget(TurnData data, SkillCastData skillCast, BattleCharacter caster, BattleCharacter target)
        {
            var skill = skillCast.Skill;
            double potency = skill.Skill.potency;
            int totalDamage = formula.CalculateDamage(caster, target, potency);
            int targetAmp = target.Stats.GetSlidingStat(StatNames.Amp);
            
            target.TakeAmpDamage(totalDamage);
            caster.AddAmp(totalDamage);


            if (totalDamage > targetAmp)
            {
                bool brokeTarget = target.BreakCharacter();
                if (brokeTarget)
                {
                    int spirit = caster.Stats.GetStat(StatNames.Spirit);
                    caster.AddAmp(spirit);
                }
            }

            data.AddAmpDamage(totalDamage);
        }
    }
}
