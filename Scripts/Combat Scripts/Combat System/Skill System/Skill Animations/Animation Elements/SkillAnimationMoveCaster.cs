using Godot;
using System;
using Godot.Collections;

namespace CombatSystem
{
    [GlobalClass]
    public partial class SkillAnimationMoveCaster : SkillAnimationElement
    {

        public override bool _RunElement(TurnData data, TimeHandler time)
        {
            return true;
        }
    }
}