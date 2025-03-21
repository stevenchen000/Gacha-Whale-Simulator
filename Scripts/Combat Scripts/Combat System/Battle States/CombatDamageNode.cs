using System;
using Godot;
using StateSystem;
using Godot.Collections;

namespace CombatSystem
{
    public partial class CombatDamageNode : CombatStateNode
    {
        [Export] private CombatStateNode victoryCheckNode;

        private Array<SkillEffect> effects;
        private SkillEffect currEffect;
        private int effectIndex = 0;

        private TimeHandler timer;

        protected override void OnStateActivated()
        {
            timer = new TimeHandler();
            effects = battle.SelectedSkill.Skill.effects;
            if (effects != null && effects.Count > 0)
                currEffect = effects[0];
        }

        protected override void RunState(double delta)
        {
            timer.Tick(delta);
            bool finished = currEffect.RunEffect(battle.turnData, timer);
            
            if (finished)
            {
                effectIndex++;
                timer = new TimeHandler();
                if (effectIndex < effects.Count)
                    currEffect = effects[effectIndex];
                else
                    currEffect = null;
            }
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;

            if (currEffect == null)
            {
                result = victoryCheckNode;
                battle.State.PlayerParty.CheckPartyHealth();
                battle.State.EnemyParty.CheckPartyHealth();
            }

            return result;
        }

        protected override void OnStateDeactivated()
        {
            Reset();
            
        }

        private void Reset()
        {
            effects = null;
            effectIndex = 0;
            currEffect = null;
            timer = null;
        }
    }
}
