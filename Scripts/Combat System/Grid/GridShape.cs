using Godot;
using System;
using Godot.Collections;

namespace CombatSystem {
    [GlobalClass]
    public partial class GridShape : Resource
    {
        //<0,0> is where the character is standing
        //+Y is up, +X is right
        [Export] private Array<Vector2I> spaces;

        public Array<Vector2I> GetPositionsInRange(Vector2I position, bool facingLeft = false)
        {
            var result = new Array<Vector2I>();

            foreach(var space in spaces)
            {
                int xOffset = space.X;
                int yOffset = space.Y;

                if (facingLeft) xOffset = -xOffset;
                int posX = position.X;
                int posY = position.Y;
                var newPosition = new Vector2I(posX + xOffset, posY + yOffset);
                result.Add(newPosition);
            }

            return result;
        }

    }
}