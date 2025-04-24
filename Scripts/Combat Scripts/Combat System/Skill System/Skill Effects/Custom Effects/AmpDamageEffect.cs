using Godot;
using System;

namespace CombatSystem
{
    [Tool]
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
            double potency = skillCast.Skill.Skill.potency;
            int numOfTargets = targets.Count;
            if(numOfTargets > 1) potency = potency / numOfTargets + 0.2;

            foreach(var target in targets)
            {
                DealDamageToTarget(data, skillCast, caster, target, potency);
            }
        }

        private void DealDamageToTarget(TurnData data, 
                                        SkillCastData skillCast, 
                                        BattleCharacter caster, 
                                        BattleCharacter target, 
                                        double potency)
        {
            var skill = skillCast.Skill;
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
