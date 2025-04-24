using Godot;
using System;
using Godot.Collections;
using System.Collections.Generic;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class SkillAnimation : Resource
    {
        [Export] private Array<SkillAnimationElement> animations;
        [Export] public double Duration { get; private set; }

        public SkillAnimation()
        {

        }

        public void AddAnimationsToList(List<SkillAnimationContainer> animList,
                                        TurnData turnData,
                                        SkillCastData skillCast)
        {
            if (animations != null)
            {
                foreach (var anim in animations)
                {
                    anim.AddAnimationContainer(animList, turnData, skillCast);
                }
            }
        }
    }
}