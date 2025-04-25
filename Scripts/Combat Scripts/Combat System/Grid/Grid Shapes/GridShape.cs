using Godot;
using System;
using Godot.Collections;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class GridShape : Resource
    {
        //<0,0> is where the character is standing
        //+Y is up, +X is right
        [Export] protected Array<Vector2I> attackShape = new Array<Vector2I>();


        public virtual TargetingData GetTargetsFromPosition(BattleGrid grid, BattleCharacter caster, SkillContainer skill, Vector2I position)
        {
            return null;
        }


        public virtual Array<Vector2I> GetSpacesReachableToTarget(BattleGrid grid, MovementData movement, Vector2I targetCoords)
        {
            return new Array<Vector2I>();
        }






        /*****************
         * Helpers
         * ****************/

        protected void AddOffsetToSpaces(Array<Vector2I> spaces, Vector2I position)
        {
            for(int i = 0; i < spaces.Count; i++)
            {
                var space = spaces[i];
                int x = position.X;
                int y = position.Y;
                space.X += x;
                space.Y += y;
                spaces[i] = space;
            }
        }

        protected void RemoveSpacesOutOfRange(BattleGrid grid, Array<Vector2I> coords)
        {
            int index = 0;
            while(index < coords.Count)
            {
                var currCoords = coords[index];
                if (!grid.CoordinatesInGrid(currCoords))
                {
                    coords.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }
        }

        protected bool ContainsValidTarget(BattleGrid grid, Array<Vector2I> coordinates, BattleCharacter character, TargetType targetType)
        {
            bool result = false;

            foreach(var coords in coordinates)
            {
                var space = grid.GetSpaceFromCoords(coords);
                var target = space.CharacterOnSpace;

                if (target != null)
                {
                    if(targetType == TargetType.Enemy && character.IsEnemy(target))
                    {
                        result = true;
                        break;
                    }else if(targetType == TargetType.Ally && !character.IsEnemy(target))
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }


        protected Array<Vector2I> JoinArrays(params Array<Vector2I>[] spaces)
        {
            var result = new Array<Vector2I>();

            foreach (var array in spaces)
            {
                foreach (var space in array)
                {
                    if (!result.Contains(space))
                        result.Add(space);
                }
            }

            return result;
        }

        protected Array<Vector2I> FindCommonSpaces(Array<Vector2I> a, Array<Vector2I> b)
        {
            Array<Vector2I> result = new();

            foreach (var space in a)
            {
                if (b.Contains(space))
                    result.Add(space);
            }

            return result;
        }

    }
}