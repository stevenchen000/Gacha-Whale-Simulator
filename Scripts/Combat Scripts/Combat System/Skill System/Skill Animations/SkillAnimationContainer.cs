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
        public double Delay { get; private set; }
        public BattleCharacter Target {  get; private set; }


        public SkillAnimationContainer(TurnData data, SkillAnimationElement animation, double delay, BattleCharacter target)
        {
            turnData = data;
            Animation = animation;
            time = new TimeHandler();
            Delay = delay;
            Target = target;
        }

        public void StartAnimation()
        {
            Animation.StartElement(this, turnData);
        }

        public bool RunAnimation(double delta)
        {
            time.Tick(delta);
            return Animation.RunElement(this, turnData, time);
        }

        public void EndAnimation()
        {
            Animation.EndElement(this, turnData);
        }

        public bool AnimationHasStarted(TimeHandler time)
        {
            return time.TimeIsUp(Delay);
        }
    }
}
