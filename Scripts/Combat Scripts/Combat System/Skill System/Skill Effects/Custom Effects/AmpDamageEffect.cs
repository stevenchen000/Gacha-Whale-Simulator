using Godot;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class AmpDamageEffect : SkillEffect
    {
        [Export] private float potency;
        [Export] private float duration = 0.2f;
        [Export] private int numberOfHits = 1;
        [Export] private BaseDamageFormula formula;

        protected override void _StartEffect(TurnData data)
        {
            //DealDamage(data);
        }

        protected override bool _RunEffect(TurnData data, TimeHandler timer)
        {
            int intervals = timer.IntervalsBetween(duration);
            while (intervals > 0)
            {
                intervals--;
                DealDamage(data);
            }

            return timer.TimeIsUp(delay + duration*numberOfHits);
        }

        protected override void _EndEffect(TurnData data)
        {
            
        }

        
        private void DealDamage(TurnData data)
        {
            var caster = data.caster;
            var targets = data.targets;

            foreach(var target in targets)
            {
                DealDamageToTarget(data, caster, target);
            }
        }

        private void DealDamageToTarget(TurnData data, BattleCharacter caster, BattleCharacter target)
        {
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
