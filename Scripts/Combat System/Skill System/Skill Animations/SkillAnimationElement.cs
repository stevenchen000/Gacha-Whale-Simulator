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

        public bool RunElement(BattleSkillCastData data)
        {
            UpdateTime(data);

            if (completed) return true;

            if (prevFrame <= delay && currFrame > delay)
            {
                _StartElement(data);
            }

            if (!ignoreDuration && currFrame > duration)
            {
                _EndElement(data);
                completed = true;
            }
            else if(currFrame > delay)
            {
                completed = _RunElement(data);
                if (completed) _EndElement(data);
            }
            return completed;
        }

        public virtual void _StartElement(BattleSkillCastData data)
        {
        }

        //Returns true when the animation is finished
        public virtual bool _RunElement(BattleSkillCastData data)
        {
            return true;
        }

        public virtual void _EndElement(BattleSkillCastData data) { } 

        protected void UpdateTime(BattleSkillCastData data)
        {
            prevFrame = data.prevFrame - delay;
            currFrame = data.currFrame - delay;
        }
    }
}