using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatAnimationNode : CombatStateNode
    {
        [Export] private CombatStateNode damageCalculationNode;
        private SkillContainer skill;
        private TurnData turnData;
        private SkillCastData skillCast;
        private BattleCharacter caster;

        protected override void OnStateActivated()
        {
            turnData = battle.turnData;
            skillCast = turnData.MainSkillCast;
            if (skillCast != null)
            {
                skill = skillCast.Skill;
                caster = skillCast.Caster;
                caster.CastSkill(skill, turnData, skillCast);
            }
        }

        protected override void RunState(double delta)
        {
            
        }

        protected override StateNode CheckStateChange()
        {
            CombatStateNode result = null;

            if (skillCast == null || !caster.IsCasting)
            {
                result = damageCalculationNode;
            }

            return result;
        }

        protected override void OnStateDeactivated()
        {
            turnData.RemoveCurrentSkill();
            skill = null;
            turnData = null;
            caster = null;
        }
    }
}
