using Godot;
using System;

namespace CombatSystem
{
    public partial class SkipTurnButton : Node
    {
        
        public void OnClick()
        {
            var battle = Utils.FindParentOfType<BattleManager>(this);
            battle.SkipTurn();
        }
        
    }
}