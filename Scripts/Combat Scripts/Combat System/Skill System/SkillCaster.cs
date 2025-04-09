using Godot;
using System;
using Godot.Collections;
using System.Collections.Generic;

namespace CombatSystem
{
    public partial class SkillCaster : Node
    {

        public bool IsCasting { get { return skill != null; } }
        private SkillContainer skill;
        private TimeHandler time;
        private TurnData turnData;
        private List<SkillAnimationContainer> animations;
        private List<SkillAnimationContainer> runningAnimations;
        private List<SkillAnimationContainer> effects;
        private SkillAnimationContainer currEffect;

        public override void _Ready()
        {
            animations = new List<SkillAnimationContainer>();
            runningAnimations = new List<SkillAnimationContainer>();
        }

        public override void _Process(double delta)
        {
            if (skill != null && !SkipAnimation())
            {
                time.Tick(delta);
                bool hasFinished = RunSkill(delta);
                if (hasFinished)
                {
                    EndSkillCast();
                }
            }
            else
            {
                EndSkillCast();
            }
        }

        private bool RunSkill(double delta)
        {
            CheckUnstartedAnimations();
            RunCurrentAnimations(delta);
            return SkillAnimationIsOver();
        }

        private bool SkillAnimationIsOver()
        {
            return animations.Count == 0 && runningAnimations.Count == 0;
        }

        private void CheckUnstartedAnimations()
        {
            int index = 0;
            while (index < animations.Count)
            {
                var anim = animations[index];
                if (anim.AnimationHasStarted(time))
                {
                    animations.RemoveAt(index);
                    runningAnimations.Add(anim);
                    anim.StartAnimation();
                }
                else
                {
                    index++;
                }
            }
        }

        private void RunCurrentAnimations(double delta)
        {
            int index = 0;
            while(index < runningAnimations.Count) 
            {
                var anim = runningAnimations[index];
                bool hasFinished = anim.RunAnimation(delta);
                if (hasFinished)
                {
                    runningAnimations.RemoveAt(index);
                    anim.EndAnimation();
                }
                else
                {
                    index++;
                }
            }
        }

        private void RunEffects(double delta)
        {

        }


        public void CastSkill(SkillContainer skill, TurnData turn)
        {
            this.skill = skill;
            turnData = turn;
            skill = turn.Skill;
            time = new TimeHandler();
            SetupAnimationData();
        }

        private void SetupAnimationData()
        {
            if(!IgnoringAnimations())
                skill.Skill.SetupSkillAnimation(animations, turnData);
        }

        private bool IgnoringAnimations()
        {
            string setting = SettingNames.DisableCombatAnimationsFlag;
            return GameState.GetFlag(setting);
        }

        private void SetupEffectData()
        {
            effects = new List<SkillAnimationContainer>();
            
        }

        private void EndSkillCast()
        {
            skill = null;
            time = null;
            turnData = null;
        }

        private bool SkipAnimation()
        {
            return GameState.GetFlag(SettingNames.DisableCombatAnimationsFlag);
        }
    }
}