using Godot;
using Godot.Collections;
using System;

namespace CombatSystem {
    [Tool]
    [GlobalClass]
    public partial class SkillEffect : Resource
    {
        [Export] protected float delay = 0f;
        [Export(PropertyHint.MultilineText)] protected string description;
        [Export] protected SkillEffectTargetType targetType = SkillEffectTargetType.SameAsSkill;

        public bool RunEffect(TurnData data, SkillCastData skillCast, TimeHandler timer) {
            bool finished = false;
            
            if (timer.TimeIsBetween(delay))
                _StartEffect(data, skillCast);
            if(timer.TimeIsUp(delay))
                finished = _RunEffect(data, skillCast, timer);

            if(finished) _EndEffect(data, skillCast);

            return finished;
        }

        protected virtual void _StartEffect(TurnData data, SkillCastData skillCast) { }

        protected virtual bool _RunEffect(TurnData data, SkillCastData skillCast, TimeHandler timer) { return true; }
        protected virtual void _EndEffect(TurnData data, SkillCastData skillCast) { }


        protected Array<BattleCharacter> GetTargets(SkillCastData skillCast)
        {
            var result = new Array<BattleCharacter>();

            var caster = skillCast.Caster;
            var targets = skillCast.Targets;

            switch (targetType)
            {
                case SkillEffectTargetType.SameAsSkill:
                    result = targets;
                    break;
                case SkillEffectTargetType.AllyParty:
                    result = caster.Party.GetAllLivingMembers();
                    break;
                case SkillEffectTargetType.EnemyParty:
                    result = targets[0].Party.GetAllLivingMembers();
                    break;
                case SkillEffectTargetType.Self:
                    result.Add(caster);
                    break;
            }

            return result;
        }
    }
}