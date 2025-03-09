using Godot;
using System;

namespace CombatSystem
{
    public partial class SkipTurnButton : Button
    {
        
        public void OnClick()
        {
            var battle = Utils.FindParentOfType<BattleManager>(this);
            battle.SkipTurn();
        }
        
    }
}