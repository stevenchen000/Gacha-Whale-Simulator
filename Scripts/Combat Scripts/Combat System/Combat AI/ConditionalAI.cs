using Godot;
using Godot.Collections;
using System;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class ConditionalAI : CombatAI
    {
        [Export] private Array<CombatAI> aiList = new Array<CombatAI>();

        public override CombatActionData CalculateAction(BattleManager battle, BattleCharacter caster, Array<BattleCharacter> targets)
        {
            

            return null;
        }
    }
}