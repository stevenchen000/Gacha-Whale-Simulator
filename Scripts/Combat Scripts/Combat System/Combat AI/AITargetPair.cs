using System;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class AITargetPair : Resource
    {
        [Export] public AITargetSelection TargetSelection { get; private set; }
        [Export] public CombatAI AI { get; private set; }

        public CombatActionData GetAction(BattleManager battle, BattleCharacter caster)
        {
            Array<BattleCharacter> targets = new();
            CombatActionData result = null;

            if(TargetSelection != null) 
                targets = TargetSelection.GetTargets(battle, caster);
            if(AI != null)
                result = AI.CalculateAction(battle, caster, targets);

            return result;
        }
    }
}
