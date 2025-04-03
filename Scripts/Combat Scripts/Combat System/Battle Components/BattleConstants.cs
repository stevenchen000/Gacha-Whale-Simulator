using System;
using Godot;

namespace CombatSystem
{
    public class BattleConstants
    {
        public const double ElementalWeaknessMultiplier = 1.2;
        public const double ElementalResistanceMultiplier = 0.8;

        public static Vector2I GetDirectionOffset(CharacterDirection direction)
        {
            Vector2I result = Vector2I.Zero;

            switch (direction)
            {
                case CharacterDirection.UP:
                    result = new Vector2I(0, 1);
                    break;
                case CharacterDirection.DOWN:
                    result = new Vector2I(0, -1);
                    break;
                case CharacterDirection.LEFT:
                    result = new Vector2I(-1, 0);
                    break;
                case CharacterDirection.RIGHT:
                    result = new Vector2I(1, 0);
                    break;
            }

            return result;
        }
    }
}
