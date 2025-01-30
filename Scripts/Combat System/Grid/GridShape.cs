using Godot;
using System;
using Godot.Collections;

namespace CombatSystem {
    [GlobalClass]
    public partial class GridShape : Resource
    {
        //<0,0> is where the character is standing
        //+Y is up, +X is right
        [Export] private CharacterDirection defaultDirection = CharacterDirection.UP;
        [Export] private Array<Vector2I> spaces;

        public Array<Vector2I> GetPositionsInRange(Vector2I position, CharacterDirection direction)
        {
            var result = TurnSpaces(direction);
            AddOffsetToSpaces(result, position);

            return result;
        }

        private void AddOffsetToSpaces(Array<Vector2I> spaces, Vector2I position)
        {
            for(int i = 0; i < spaces.Count; i++)
            {
                var space = spaces[i];
                int x = position.X;
                int y = position.Y;
                space.X += x;
                space.Y += y;
            }
        }

        private Array<Vector2I> TurnSpaces(CharacterDirection newDirection)
        {
            Array<Vector2I> result = null;

            if (FlippedHorizontal(newDirection))
            {
                result = FlipSpacesHorizontally(spaces);
            }
            else if (FlippedVertical(newDirection))
            {
                result = FlipSpacesVertically(spaces);
            }
            else
            {
                result.AddRange(spaces);
            }

            return result;
        }

        private bool FlippedHorizontal(CharacterDirection newDirection)
        {
            bool result = false;

            switch (newDirection)
            {
                case CharacterDirection.LEFT:
                    result = defaultDirection == CharacterDirection.RIGHT; 
                    break;
                case CharacterDirection.RIGHT:
                    result = defaultDirection == CharacterDirection.LEFT;
                    break;
            }
            return result;
        }

        private bool FlippedVertical(CharacterDirection newDirection)
        {
            bool result = false;

            switch (newDirection)
            {
                case CharacterDirection.UP:
                    result = defaultDirection == CharacterDirection.DOWN;
                    break;
                case CharacterDirection.DOWN:
                    result = defaultDirection == CharacterDirection.UP;
                    break;
            }
            return result;
        }

        private Array<Vector2I> FlipSpacesHorizontally(Array<Vector2I> spaces)
        {
            Array<Vector2I> result = new Array<Vector2I>();

            foreach(var space in spaces)
            {
                int x = -space.X;
                int y = space.Y;
                var newSpace = new Vector2I(x, y);
                result.Add(newSpace);
            }

            return result;
        }

        private Array<Vector2I> FlipSpacesVertically(Array<Vector2I> spaces)
        {
            Array<Vector2I> result = new Array<Vector2I>();

            foreach (var space in spaces)
            {
                int x = space.X;
                int y = -space.Y;
                var newSpace = new Vector2I(x, y);
                result.Add(newSpace);
            }

            return result;
        }

    }
}