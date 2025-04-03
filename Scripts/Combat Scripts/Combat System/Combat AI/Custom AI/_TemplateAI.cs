using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    [GlobalClass]
    public partial class _TemplateAI : CombatAI
    {

        public override CombatActionData CalculateAction(BattleManager battle)
        {
            return null;
        }

    }
}
