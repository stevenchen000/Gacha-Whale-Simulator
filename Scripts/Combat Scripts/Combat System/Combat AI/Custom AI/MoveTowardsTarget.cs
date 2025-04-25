using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class MoveTowardsTarget : CombatAI
    {

        public override CombatActionData CalculateAction(BattleManager battle, BattleCharacter caster, Array<BattleCharacter> targets)
        {
            var moveData = battle.Grid.CurrentMovementData;
            BattleCharacter target = null;
            CombatActionData result = null;
            Utils.Print(this, $"{targets.Count} targets on field");
            if (targets.Count > 0)
            {
                target = targets[0];
                Vector2I newPos = GetNearestPosFromTarget(moveData, target);
                Utils.Print(this, $"Moving to {newPos}");
                result = new CombatActionData(newPos, null, null, null, 9999);
            }
            return result;
        }

        private Vector2I GetNearestPosFromTarget(MovementData movement, BattleCharacter target)
        {
            var walkableSpaces = movement.WalkableSpaces;
            var targetPos = target.currPosition;

            Vector2I result = new Vector2I(9999, 9999);
            int distance = 9999;

            foreach(var space in walkableSpaces)
            {
                int xDiff = Math.Abs(space.X - targetPos.X);
                int yDiff = Math.Abs(space.Y - targetPos.Y);
                int newDistance = xDiff + yDiff;

                if (newDistance < distance)
                {
                    distance = newDistance;
                    result = space;
                }
            }

            return result;

        }
    }
}
