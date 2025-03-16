using Godot;
using Godot.Collections;
using System;

namespace CombatSystem
{
    public partial class ConditionalAI : CombatAI
    {
        [Export] private Array<CombatAI> aiList = new Array<CombatAI>();

        public override CombatActionData CalculateAction(BattleManager battle)
        {
            CombatActionData result = null;

            for(int i = 0; i < aiList.Count; i++)
            {
                var currAI = aiList[i];
                result = currAI.CalculateAction(battle);

                if (result != null) break;
            }

            return result;
        }
    }
}