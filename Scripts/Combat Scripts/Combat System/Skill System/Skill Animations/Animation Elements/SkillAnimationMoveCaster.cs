using Godot;
using System;
using Godot.Collections;

namespace CombatSystem
{
    [GlobalClass]
    public partial class SkillAnimationMoveCaster : SkillAnimationElement
    {

        public override bool RunElement(SkillAnimationContainer container, TurnData data,
                                                   SkillCastData skillCast, TimeHandler time)
        {
            return true;
        }
    }
}