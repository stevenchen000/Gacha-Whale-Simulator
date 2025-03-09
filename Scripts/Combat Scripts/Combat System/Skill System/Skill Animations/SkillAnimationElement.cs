using Godot;
using System;
using Godot.Collections;

namespace CombatSystem
{
    public partial class SkillAnimationElement : Resource
    {
        [Export] private double delay = 0;
        [Export] protected double duration = 0;
        [Export] private bool ignoreDuration = false;
        protected double prevFrame = 0;
        protected double currFrame = 0;

        private bool completed = false;

        public bool RunElement(TurnData data, TimeHandler time)
        {
            if (completed) return true;

            if (prevFrame <= 0 && currFrame > 0)
            {
                _StartElement(data, time);
            }

            if (!ignoreDuration && currFrame > duration)
            {
                _EndElement(data, time);
                completed = true;
            }
            else if(currFrame > 0)
            {
                completed = _RunElement(data, time);
                if (completed) _EndElement(data, time);
            }
            return completed;
        }

        public virtual void _StartElement(TurnData data, TimeHandler time)
        {
        }

        //Returns true when the animation is finished
        public virtual bool _RunElement(TurnData data, TimeHandler time)
        {
            return true;
        }

        public virtual void _EndElement(TurnData data, TimeHandler time) { } 

    }
}