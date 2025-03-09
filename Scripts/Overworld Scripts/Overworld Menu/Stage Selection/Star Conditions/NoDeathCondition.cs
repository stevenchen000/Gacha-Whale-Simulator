using System;
using Godot;

namespace CombatSystem
{
    [GlobalClass]
    public partial class NoDeathCondition : StageStarCondition
    {

        public override bool ConditionCleared(BattleState state)
        {
            return state.PlayerParty.EntirePartyAlive();
        }
    }
}
