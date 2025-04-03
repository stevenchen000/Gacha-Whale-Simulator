using Godot;
using System;
using Godot.Collections;

namespace CombatSystem {
    [GlobalClass]
    public partial class DirectionalShape : GridShape
    {
        //<0,0> is where the character is standing
        //+Y is up, +X is right
        [Export] private CharacterDirection defaultDirection = CharacterDirection.UP;
        [Export] private DirectionStyle directionStyle = DirectionStyle.VERTICAL;
        


        public override TargetingData GetTargetsFromPosition(BattleGrid grid, BattleCharacter caster, SkillContainer skill, Vector2I position)
        {
            TargetingData result = null;

            var directionData = CreateDirectionData(position);
            result = new TargetingData(caster, skill, directionData);

            return result;
        }

        private Dictionary<CharacterDirection, Array<Vector2I>> CreateDirectionData(Vector2I position)
        {
            var result = new Dictionary<CharacterDirection, Array<Vector2I>>();

            switch (directionStyle)
            {
                case DirectionStyle.VERTICAL:
                    result[CharacterDirection.UP] = GetAllSpacesInDirection(CharacterDirection.UP, position);
                    result[CharacterDirection.DOWN] = GetAllSpacesInDirection(CharacterDirection.DOWN, position);
                    break;
                case DirectionStyle.HORIZONTAL:
                    result[CharacterDirection.LEFT] = GetAllSpacesInDirection(CharacterDirection.LEFT, position);
                    result[CharacterDirection.RIGHT] = GetAllSpacesInDirection(CharacterDirection.RIGHT, position);
                    break;
                case DirectionStyle.OMNIDIRECTIONAL:
                    result[CharacterDirection.UP] = GetAllSpacesInDirection(CharacterDirection.UP, position);
                    result[CharacterDirection.DOWN] = GetAllSpacesInDirection(CharacterDirection.DOWN, position);
                    result[CharacterDirection.LEFT] = GetAllSpacesInDirection(CharacterDirection.LEFT, position);
                    result[CharacterDirection.RIGHT] = GetAllSpacesInDirection(CharacterDirection.RIGHT, position);
                    break;
            }

            return result;
        }




        public override Array<Vector2I> GetPositionsInRange(Vector2I position, CharacterDirection direction)
        {
            //var result = TurnSpaces(direction);
            //AddOffsetToSpaces(result, position);

            return null;
        }

        public override Array<Vector2I> GetPositionsToReachPosition(Vector2I target)
        {
            return null;
        }




        private Array<Vector2I> GetAllSpacesInDirection(CharacterDirection newDirection, Vector2I offset)
        {
            Array<Vector2I> result = new Array<Vector2I>();
            result.AddRange(attackShape);

            int rotationAmount = RotationNumber(newDirection);

            switch (rotationAmount)
            {
                case 1: // (X,Y) -> (Y, -X)
                    for(int i = 0; i < result.Count; i++)
                    {
                        var coords = result[i];
                        result[i] = new Vector2I(coords.Y, -coords.X) + offset;
                    }
                    break;
                case 2: // (X,Y) -> (-X, -Y)
                    for (int i = 0; i < result.Count; i++)
                    {
                        var coords = result[i];
                        result[i] = new Vector2I(-coords.X, -coords.Y) + offset;
                    }
                    break;
                case 3: // (X,Y) -> (-Y, X)
                    for (int i = 0; i < result.Count; i++)
                    {
                        var coords = result[i];
                        result[i] = new Vector2I(-coords.Y, coords.X) + offset;
                    }
                    break;
                default:
                    for (int i = 0; i < result.Count; i++)
                    {
                        var coords = result[i];
                        result[i] = coords + offset;
                    }
                    break;
            }

            return result;
        }


        /// <summary>
        /// 1 = 90 degrees counter-clockwise
        /// 2 = 180 degrees
        /// 3 = 270 degrees counter-clockwise
        /// </summary>
        /// <param name="newDirection"></param>
        /// <returns></returns>
        private int RotationNumber(CharacterDirection newDirection)
        {
            int result = (int)defaultDirection - (int)newDirection;

            if (result < 0) result += 4;

            return result;
        }
    }
}