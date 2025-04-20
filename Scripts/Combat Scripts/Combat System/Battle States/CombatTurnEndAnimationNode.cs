using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatTurnEndAnimationNode : CombatStateNode
    {
        [Export] private CombatStateNode victoryCheckNode;
        private SkillContainer skill;
        private TurnData turnData;
        private BattleCharacter caster;

        private bool isCasting = false;

        protected override void OnStateActivated()
        {
            turnData = battle.turnData;
            if (turnData.HasSkillQueued())
            {
                var currSkillData = turnData.GetCurrentSkillCast();
                skill = currSkillData.Skill;
                caster = currSkillData.Caster;
                caster.CastSkill(skill, turnData, currSkillData);
                isCasting = true;
            }
        }

        protected override void RunState(double delta)
        {
            if (isCasting && !caster.IsCasting)
            {
                turnData.RemoveCurrentSkill();
                if (turnData.HasSkillQueued())
                {
                    var skillCast = turnData.GetCurrentSkillCast();
                    var skill = skillCast.Skill;
                    caster.CastSkill(skill, turnData, skillCast);
                }
                else
                {
                    isCasting = false;
                }
            }
        }

        protected override StateNode CheckStateChange()
        {
            CombatStateNode result = null;

            if (!isCasting)
            {
                result = victoryCheckNode;
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
