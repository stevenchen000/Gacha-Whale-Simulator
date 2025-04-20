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

        public KnockbackCollisionData CheckCollisionOnGrid(BattleGrid grid, Dictionary<BattleCharacter, BattleCharacter> dependencies)
        {
            KnockbackCollisionData collision = null;
            collision = _GridCollisions(grid, dependencies);

            return collision;
        }

        private KnockbackCollisionData _GridCollisions(BattleGrid grid, Dictionary<BattleCharacter, BattleCharacter> dependencies)
        {
            KnockbackCollisionData result = null;

            var space = grid.GetSpaceFromCoords(Coords);
            var charOnSpace = space.CharacterOnSpace;
            if (charOnSpace != null)
            {
                if (!CharacterIsKnockedBack(charOnSpace, dependencies))
                    result = new KnockbackCollisionData(Character, charOnSpace, RemainingSpaces, Direction);
            }

            return result;
        }

        public KnockbackCollisionData CheckCollisionWithKnockedUnits(List<KnockbackPositionData> newPositions,
                                                                     Dictionary<BattleCharacter, BattleCharacter> dependencies)
        {
            var dependency = dependencies[Character];
            KnockbackCollisionData collision = null;

            if (dependency != null)
            {
                for (int i = 0; i < newPositions.Count; i++)
                {
                    var position = newPositions[i];
                    var tarCoords = position.Coords;
                    var target = position.Character;
                    if (target == dependency && tarCoords == Coords)
                    {
                        collision = new KnockbackCollisionData(Character, target, RemainingSpaces, Direction);
                    }
                }
            }

            return collision;
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

        private bool CharacterIsKnockedBack(BattleCharacter target, Dictionary<BattleCharacter, BattleCharacter> dependencies)
        {
            return dependencies.ContainsKey(target);
        }
    }
}
