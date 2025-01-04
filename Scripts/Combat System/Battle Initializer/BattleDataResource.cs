using System;
using Godot;
using Godot.Collections;
using GachaSystem;

namespace CombatSystem
{
    [GlobalClass]
    public partial class BattleDataResource : Resource
    {
        [Export] public Array<GachaCharacter> enemyParty;
        [Export] public Vector2I gridSize;
        [Export] public Array<Vector2I> playerStartingPositions;
        [Export] public Array<Vector2I> enemyStartingPositions;

        public BattleData GetBattleData(Array<GachaCharacter> playerParty)
        {
            return null;
        }
    }
}
