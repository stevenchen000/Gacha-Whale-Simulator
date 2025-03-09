using System;
using Godot;
using Godot.Collections;
using GachaSystem;

namespace CombatSystem
{
    //Data created for initializing combat
    public class BattleData
    {
        public Array<BattleCharacter> playerParty { get; private set; }
        public Array<BattleCharacter> enemyParty { get; private set; }
        public Array<Vector2I> playerStartingPositions { get; private set; }
        public Array<Vector2I> enemyStartingPositions { get; private set; }
        public Vector2I gridSize { get; private set; }

        public BattleData(Array<GachaCharacter> playerCharacters)
        {

        }
    }
}
