using Godot;
using System;

namespace CombatSystem
{
    public enum BattleStateEnum
    {
        PREBATTLE,
        CHARACTER_TURN,
        CHARACTER_SELECT_ATTACK,
        CHARACTER_ATTACK,
        BATTLE_OVER
    }
}