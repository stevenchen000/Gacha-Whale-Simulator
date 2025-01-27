using Godot;
using System;
using Godot.Collections;

namespace CombatSystem
{
    [GlobalClass]
    public partial class SkillAnimation : Resource
    {
        [Export] private Array<SkillAnimationElement> animations;

        public SkillAnimation()
        {

        }

        public SkillAnimation(Array<SkillAnimationElement> newAnimations)
        {
            animations = new Array<SkillAnimationElement>();

            foreach(var animation in newAnimations)
            {
                var newAnim = (SkillAnimationElement)(animation.Duplicate());
                animations.Add(newAnim);
            }
        }

        public SkillAnimation GetDuplicate()
        {
            return new SkillAnimation(animations);
        }


        //Returns true when the animation is finished
        public bool PlayAnimation(TurnData data, TimeHandler time, double delta)
        {
            bool animationFinished = true;
            foreach(var animation in animations)
            {
                animationFinished &= animation.RunElement(data, time);
            }
            return animationFinished;
        }
    }
}