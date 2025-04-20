using System;
using Godot;
using StateSystem;
using Godot.Collections;

namespace CombatSystem
{
    public partial class CombatDamageNode : CombatStateNode
    {
        [Export] private CombatStateNode knockbackNode;

        private Array<SkillEffect> effects;
        private SkillEffect currEffect;
        private int effectIndex = 0;
        private TurnData turnData;
        private SkillCastData skillCast;

        private TimeHandler timer;

        protected override void OnStateActivated()
        {
            timer = new TimeHandler();
            turnData = battle.turnData;
            skillCast = turnData.GetCurrentSkillCast();
            effects = skillCast.Skill.Skill.effects;
            if (effects != null && effects.Count > 0)
                currEffect = effects[0];
        }

        protected override void RunState(double delta)
        {
            /*timer.Tick(delta);
            bool finished = currEffect.RunEffect(turnData, skillCast, timer);
            
            if (finished)
            {
                effectIndex++;
                timer = new TimeHandler();
                if (effectIndex < effects.Count)
                    currEffect = effects[effectIndex];
                else
                    currEffect = null;
            }*/
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;

            if (currEffect == null)
            {
                result = knockbackNode;
            }

            return result;
        }

        protected override void OnStateDeactivated()
        {
            Reset();
            turnData.RemoveCurrentSkill();
        }

        private void Reset()
        {
            effects = null;
            effectIndex = 0;
            currEffect = null;
            timer = null;
            turnData = null;
            skillCast = null;
        }
    }
}
