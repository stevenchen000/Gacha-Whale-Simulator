using System;
using Godot;

namespace CombatSystem
{
    [GlobalClass]
    public partial class StageStarCondition : Resource
    {

        public virtual bool ConditionCleared(BattleState state)
        {
            return true;
        }
    }
}
