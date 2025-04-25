using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class _TemplateAI : CombatAI
    {

        public override CombatActionData CalculateAction(BattleManager battle, BattleCharacter caster, Array<BattleCharacter> targets)
        {
            return null;
        }

    }
}
