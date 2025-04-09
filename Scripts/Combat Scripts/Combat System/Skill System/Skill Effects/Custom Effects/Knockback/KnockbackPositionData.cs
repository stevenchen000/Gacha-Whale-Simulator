using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSystem
{
    public class KnockbackPositionData
    {
        public BattleCharacter Character { get; private set; }
        public Vector2I Coords { get; private set; }
        public int RemainingSpaces { get; private set; }
        public CharacterDirection Direction { get; private set; }

        public KnockbackPositionData(BattleCharacter character, Vector2I coords, int remainingSpaces, CharacterDirection direction)
        {
            Character = character;
            Coords = coords;
            RemainingSpaces = remainingSpaces;
            Direction = direction;
        }

        public KnockbackCollisionData CheckCollision(BattleGrid grid, List<KnockbackPositionData> previousPositions)
        {
            KnockbackCollisionData collision = null;
            collision = CheckCollisionOnGrid(grid, previousPositions);
            return collision;
        }

        private KnockbackCollisionData CheckCollisionOnGrid(BattleGrid grid, List<KnockbackPositionData> previousPositions)
        {
            KnockbackCollisionData result = null;

            var space = grid.GetSpaceFromCoords(Coords);
            var charOnSpace = space.CharacterOnSpace;
            if (charOnSpace != null)
            {
                if (!CharacterIsKnockedBack(charOnSpace, previousPositions))
                    result = new KnockbackCollisionData(Character, charOnSpace, RemainingSpaces, Direction);
            }

            return result;
        }

        /// <summary>
        /// Unfinished, consider later after testing
        /// </summary>
        /// <param name="previousPositions"></param>
        /// <returns></returns>
        private KnockbackCollisionData CheckCollisionWithTargets(List<KnockbackPositionData> previousPositions)
        {
            KnockbackCollisionData collision = null;

            for(int i = 0; i < previousPositions.Count; i++)
            {
                var prevPos = previousPositions[i];
                var targetCoords = prevPos.Coords;
                
            }

            return collision;
        }

        private bool CharacterIsKnockedBack(BattleCharacter target, List<KnockbackPositionData> previousPositions)
        {
            bool result = false;

            for(int i = 0; i < previousPositions.Count; i++)
            {
                var prevPos = previousPositions[i];
                result = prevPos.Character == target;
                if (result) break;
            }

            return result;
        }
    }
}
