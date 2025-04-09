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
        private BattleCharacter caster;

        protected override void OnStateActivated()
        {
            turnData = battle.turnData;
            skill = turnData.Skill;
            caster = turnData.caster;
            caster.CastSkill(skill, turnData);
        }

        protected override void RunState(double delta)
        {
            
        }

        protected override StateNode CheckStateChange()
        {
            CombatStateNode result = null;

            if (!caster.IsCasting)
            {
                result = damageCalculationNode;
            }

            return result;
        }

        protected override void OnStateDeactivated()
        {
            skill = null;
            turnData = null;
            caster = null;
        }
    }
}
