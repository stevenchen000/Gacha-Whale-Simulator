using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatAnimationNode : CombatStateNode
    {
        [Export] private CombatStateNode damageCalculationNode;
        private TimeHandler time;
        private SkillContainer skill;
        private TurnData turnData;

        private bool finishedCast = false;

        protected override void OnStateActivated()
        {
            battle.turnData.Skill.ConsumeSkillUse();
            /*time = new TimeHandler();
            turnData = battle.turnData;
            skill = battle.SelectedSkill.GetDuplicate();
            Utils.Print(this, "Playing animation...");*/
        }

        protected override void RunState(double delta)
        {
            //time.Tick(delta);
            //finishedCast = skill.PlayAnimation(turnData, time, delta);
        }

        protected override StateNode CheckStateChange()
        {
            /*StateNode result = null;

            if (finishedCast)
            {
                result = damageCalculationNode;
            }*/

            return damageCalculationNode;
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
