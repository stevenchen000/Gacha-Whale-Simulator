using Godot;
using System;
using Godot.Collections;
using System.Collections.Generic;

namespace CombatSystem
{
    public partial class SkillCaster : Node
    {

        public bool IsCasting { get { return turnData != null; } }
        private SkillContainer skill;
        private TimeHandler time;
        private TurnData turnData;
        private SkillCastData skillCast;
        private List<SkillAnimationContainer> animations = new List<SkillAnimationContainer>();
        private List<SkillAnimationContainer> runningAnimations = new List<SkillAnimationContainer>();

        private List<SkillEffect> effects = new List<SkillEffect>();
        private int effectIndex = 0;

        private bool isRunningEffects = false;


        public override void _Process(double delta)
        {
            if (turnData != null)
            {
                time.Tick(delta);

                if (!isRunningEffects)
                {
                    bool animDone = RunSkillAnimations(delta);
                    if (animDone)
                    {
                        isRunningEffects = true;
                        time.Reset();
                    }
                }else if(effectIndex < effects.Count)
                {
                    RunEffects(delta);
                }
                else
                {
                    EndSkillCast();
                }
            }
        }

        private bool RunSkillAnimations(double delta)
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
            var effect = effects[effectIndex];
            bool finished = effect.RunEffect(turnData, skillCast, time);

            if (finished)
            {
                effectIndex++;
                time.Reset();
            }
        }


        public void CastSkill(SkillContainer skill, TurnData turn, SkillCastData skillCast)
        {
            this.skill = skill;
            turnData = turn;
            this.skillCast = skillCast;
            skill = skillCast.Skill;
            time = new TimeHandler();

            SetupAnimationData(turnData, skillCast);
            SetupEffectData(skillCast);
        }

        private void SetupAnimationData(TurnData turnData, SkillCastData skillCast)
        {
            Utils.Print(this, "Targets: ");
            foreach(var target in skillCast.Targets)
            {
                Utils.Print(this, target.Character.Character.Name);
            }
            if(!IgnoringAnimations())
                skill.Skill.SetupSkillAnimation(animations, turnData, skillCast);
        }

        private bool IgnoringAnimations()
        {
            string setting = SettingNames.DisableCombatAnimationsFlag;
            return GameState.GetFlag(setting);
        }

        private void SetupEffectData(SkillCastData skillCast)
        {
            var newEffects = skillCast.Skill.Skill.effects;
            effects.AddRange(newEffects);
        }

        private void EndSkillCast()
        {
            skill = null;
            time = null;
            turnData = null;
            isRunningEffects = false;
            runningAnimations.Clear();
            effects.Clear();
            effectIndex = 0;
            skillCast = null;
        }

        private bool SkipAnimation()
        {
            return GameState.GetFlag(SettingNames.DisableCombatAnimationsFlag);
        }
    }
}