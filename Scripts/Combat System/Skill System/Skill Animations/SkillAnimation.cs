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
        public bool PlayAnimation(BattleSkillCastData data, double delta)
        {
            data.Tick(delta);
            bool animationFinished = true;
            foreach(var animation in animations)
            {
                animationFinished &= animation.RunElement(data);
            }
            return animationFinished;
        }
    }
}