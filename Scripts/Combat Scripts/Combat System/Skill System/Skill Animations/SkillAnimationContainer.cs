using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSystem
{
    public class SkillAnimationContainer
    {
        public SkillAnimationElement Animation { get; private set; }
        private TimeHandler time;
        private TurnData turnData;
        private SkillCastData skillData;
        public double Delay { get; private set; }
        public BattleCharacter Target {  get; private set; }


        public SkillAnimationContainer(TurnData data, SkillCastData skillCast, SkillAnimationElement animation, double delay, BattleCharacter target)
        {
            turnData = data;
            skillData = skillCast;
            Animation = animation;
            time = new TimeHandler();
            Delay = delay;
            Target = target;
        }

        public void StartAnimation()
        {
            Animation.StartElement(this, turnData, skillData);
        }

        public bool RunAnimation(double delta)
        {
            time.Tick(delta);
            return Animation.RunElement(this, turnData, skillData, time);
        }

        public void EndAnimation()
        {
            Animation.EndElement(this, turnData, skillData);
        }

        public bool AnimationHasStarted(TimeHandler time)
        {
            return time.TimeIsUp(Delay);
        }
    }
}
