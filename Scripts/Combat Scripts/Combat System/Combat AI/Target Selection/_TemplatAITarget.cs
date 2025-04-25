using Godot;
using Godot.Collections;
using System;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class _TemplatAITarget : AITargetSelection
    {
        public override Array<BattleCharacter> GetTargets(BattleManager battle, BattleCharacter caster)
        {
            return null;
        }
    }
}
