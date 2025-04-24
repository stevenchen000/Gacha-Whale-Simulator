using Godot;
using Godot.Collections;
using System;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class RangedShape : GridShape
    {
        [Export] private int minRange = 1;
        [Export] private int maxRange = 2;
        //Targeting Style = SelectSpace
        
        //Ranged Shape has the caster select a space to attack
        //Attack will target spaces in attackShape

        public override TargetingData GetTargetsFromPosition(BattleGrid grid, BattleCharacter caster, SkillContainer skill, Vector2I position)
        {
            TargetingData result = null;

            var coords = GetAllCoords();
            AddOffsetToSpaces(coords, position);
            RemoveSpacesOutOfRange(grid, coords);
            var targetData = CreateTargetingData(coords);
            result = new TargetingData(caster, skill, targetData);

            return result;
        }

        private Dictionary<Vector2I, Array<Vector2I>> CreateTargetingData(Array<Vector2I> coords)
        {
            var result = new Dictionary<Vector2I, Array<Vector2I>>();
            
            for(int i = 0; i < coords.Count; i++)
            {
                var currCoords = coords[i];
                var targetCoords = attackShape.Duplicate(true);
                AddOffsetToSpaces(targetCoords, currCoords);
                result[currCoords] = targetCoords;
            }

            return result;
        }



        private Array<Vector2I> GetAllCoords()
        {
            var result = new Array<Vector2I>();

            for (int i = minRange; i <= maxRange; i++)
            {
                var spaces = AddAllCoordsInRange(result, i);
            }

            return result;
        }


        /// <summary>
        /// Gets all spaces exactly (range) spaces away
        /// uses taxicab geometry
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        private Array<Vector2I> AddAllCoordsInRange(Array<Vector2I> result, int range)
        {
            int xOffset = range;
            int yOffset = 0;

            while(xOffset >= 0)
            {
                var offset1 = new Vector2I(xOffset, yOffset);
                var offset2 = new Vector2I(xOffset, -yOffset);
                var offset3 = new Vector2I(-xOffset, yOffset);
                var offset4 = new Vector2I(-xOffset, -yOffset);

                AddIfNotInList(offset1, result);
                AddIfNotInList(offset2, result);
                AddIfNotInList(offset3, result);
                AddIfNotInList(offset4, result);

                xOffset--;
                yOffset++;
            }

            return result;
        }

        private void AddIfNotInList(Vector2I coords, Array<Vector2I> list)
        {
            if (!list.Contains(coords))
            {
                list.Add(coords);
            }
        }
    }
}