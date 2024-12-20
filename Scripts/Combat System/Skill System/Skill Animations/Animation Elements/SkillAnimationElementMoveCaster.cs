using Godot;
using System;
using Godot.Collections;

namespace CombatSystem
{
    [GlobalClass]
    public partial class SkillAnimationElementMoveCaster : SkillAnimationElement
    {

        public override bool _RunElement(BattleSkillCastData data)
        {
            return true;
        }
    }
}