using System;
using Godot;

namespace CombatSystem
{
    [GlobalClass]
    public partial class TurnCountClearCondition : ClearCondition
    {
        [Export] public int TurnsToClear { get; private set; }

        public override bool ConditionCleared(BattleState state) 
        { 
            return state.TurnCount <= TurnsToClear; 
        }
    }
}
