using Godot;
using System;
using Godot.Collections;
using System.Collections.Generic;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class SkillAnimationElement : Resource
    {
        [Export] protected double delay = 0;
        [Export] protected double duration = 0;
        [Export] private bool ignoreDuration = false;
        protected double prevFrame = 0;
        protected double currFrame = 0;

        private bool completed = false;

        /// <summary>
        /// Adds the animation to the list of animations to play
        /// </summary>
        /// <param name="skillAnimations"></param>
        public virtual void AddAnimationContainer(List<SkillAnimationContainer> skillAnimations,
                                                  TurnData turnData,
                                                  SkillCastData skillCast)
        {
            
        }


        /// <summary>
        /// Called by 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        /*public bool RunElement(TurnData data, TimeHandler time)
        {
            if (completed) return true;

            if (prevFrame <= 0 && currFrame > 0)
            {
                StartElement(data, time);
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
        }*/

        public virtual void StartElement(SkillAnimationContainer container, 
                                         TurnData data, 
                                         SkillCastData skillCast)
        {
        }

        //Returns true when the animation is finished
        public virtual bool RunElement(SkillAnimationContainer container, 
                                       TurnData data, 
                                       SkillCastData skillCast, 
                                       TimeHandler time)
        {
            return true;
        }

        public virtual void EndElement(SkillAnimationContainer container, 
                                       TurnData data,
                                       SkillCastData skillCast) { } 

        

    }
}