using System;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    public class MovementData
    {
        public BattleCharacter Character { get; private set; }
        public Array<Vector2I> WalkableSpaces { get; private set; }

        public MovementData(BattleCharacter character, Array<Vector2I> walkableSpaces) 
        {
            Character = character;
            WalkableSpaces = new Array<Vector2I>();
            WalkableSpaces.AddRange(walkableSpaces);
        }
    }
}
