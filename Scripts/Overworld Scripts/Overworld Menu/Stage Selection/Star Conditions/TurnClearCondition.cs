using System;
using Godot;

namespace CombatSystem
{
    [GlobalClass]
    public partial class TurnClearCondition : StageStarCondition
    {
        [Export] private int turnCount = 10;

        public override bool ConditionCleared(BattleState state)
        {
            int finalTurnCount = state.TurnCount;
            return finalTurnCount <= turnCount;
        }
    }
}
