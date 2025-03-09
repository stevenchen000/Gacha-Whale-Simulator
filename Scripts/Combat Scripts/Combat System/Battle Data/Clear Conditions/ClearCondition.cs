using System;
using Godot;

namespace CombatSystem
{
    [GlobalClass]
    public partial class ClearCondition : Resource
    {
        public virtual bool ConditionCleared(BattleState state) { return false; }
    }
}
