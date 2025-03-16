using Godot;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class CombatAI : Resource
    {
        [Export(PropertyHint.MultilineText)] protected string description;


        public virtual CombatActionData CalculateAction(BattleManager battle)
        {
            return null;
        }
    }
}