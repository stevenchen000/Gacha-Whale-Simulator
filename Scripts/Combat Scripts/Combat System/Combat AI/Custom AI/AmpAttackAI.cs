using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    [GlobalClass]
    public partial class AmpAttackAI : CombatAI
    {

        public override CombatActionData CalculateAction(BattleManager battle)
        {
            var state = battle.State;
            var caster = battle.GetCurrentCharacter();

            var enemies = GetEnemies(state, caster);
            var ampAttack = caster.Skills.Skills[0];
            //get spaces where you can reach each target from
            //get walkable spaces
            //get intersection of both

            return null;
        }



    }
}
