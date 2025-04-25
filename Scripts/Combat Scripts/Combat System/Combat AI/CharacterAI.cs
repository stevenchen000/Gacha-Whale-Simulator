using System;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class CharacterAI : Resource
    {
        [Export] private Array<AITargetPair> aiList = new();

        public CombatActionData CalculateAction(BattleManager battle, BattleCharacter caster)
        {
            CombatActionData result = null;

            for(int i = 0; i < aiList.Count; i++)
            {
                var ai = aiList[i];

                if (ai != null)
                {
                    Utils.Print(this, ai.AI.GetType());
                    result = ai.GetAction(battle, caster);
                    if (result != null)
                        break;
                }
            }

            return result;
        }
    }
}
