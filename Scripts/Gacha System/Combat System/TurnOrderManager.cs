using Godot;
using System;
using Godot.Collections;

namespace CombatSystem
{
    public class TurnOrderManager
    {
        private Array<BattleCharacter> fighters;
        private Array<BattleCharacter> turnOrder;

        public TurnOrderManager(Array<BattleCharacter> playerParty, Array<BattleCharacter> enemyParty)
        {
            fighters = new Array<BattleCharacter>();
            fighters.AddRange(playerParty);
            fighters.AddRange(enemyParty);
            InitTurnOrder();
        }


        public BattleCharacter GetCurrentCharacter()
        {
            return turnOrder[0];
        }

        public BattleCharacter SetupNextTurn(Vector2I newPosition)
        {
            turnOrder.Add(turnOrder[0]);
            turnOrder.RemoveAt(0);
            return turnOrder[0];
        }

        



        /***********
         * Helper Functions
         * **************/

        private void InitTurnOrder()
        {
            turnOrder = new Array<BattleCharacter>();
            turnOrder.AddRange(fighters);
        }


    }
}