using Godot;
using System;
using GachaSystem;
using Godot.Collections;

namespace CombatSystem
{
    public class BattleState
    {

        public Array<BattleCharacter> playerParty { get; set; }
        public Array<BattleCharacter> enemyParty { get; set; }
        public Array<Vector2I> playerStartingPositions;
        public Array<Vector2I> enemyStartingPositions;

        public bool enemyPartyIsDead = false;
        public bool playerPartyIsDead = false;

        public BattleState()
        {

        }
    }
}