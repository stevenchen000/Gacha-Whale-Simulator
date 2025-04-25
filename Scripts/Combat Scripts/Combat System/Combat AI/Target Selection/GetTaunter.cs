using Godot;
using Godot.Collections;
using System;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class GetTaunter : AITargetSelection
    {
        public override Array<BattleCharacter> GetTargets(BattleManager battle, BattleCharacter caster)
        {
            var taunter = caster.TauntTarget;
            var result = new Array<BattleCharacter>();
            if(taunter != null) 
                result.Add(taunter);
            return result;
        }
    }
}
