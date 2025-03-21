using Godot;
using Godot.Collections;
using System;

namespace CombatSystem
{
    [GlobalClass]
    public partial class RangedShape : GridShape
    {
        [Export] private int minRange = 1;
        [Export] private int maxRange = 2;
        public override Array<Vector2I> GetPositionsInRange(Vector2I position, CharacterDirection direction)
        {
            var result = new Array<Vector2I>();

            for(int i = minRange; i <= maxRange; i++)
            {
                var spaces = GetAllCoordsOfRange(i);
                result.AddRange(spaces);
            }

            return result;
        }

        /// <summary>
        /// Gets all spaces exactly (range) spaces away
        /// uses taxicab geometry
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        private Array<Vector2I> GetAllCoordsOfRange(int range)
        {
            Array<Vector2I> result = new Array<Vector2I>();

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