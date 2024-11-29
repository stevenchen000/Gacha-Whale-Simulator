using System;

namespace CombatSystem
{
    public enum GridState
    {
        DEFAULT,
        ALLY_MOVEABLE,
        ALLY_STANDING,
        ALLY_ATTACK,
        ALLY_SUPPORT,
        ENEMY_MOVEABLE,
        ENEMY_STANDING,
        ENEMY_ATTACK,
        ENEMY_SUPPORT,
        BOUNDARY
    }
}
