using Godot;
using System;
using Godot.Collections;

namespace CombatSystem
{
    public class TurnOrderManager
    {
        private Array<BattleCharacter> fighters;
        private Array<BattleCharacter> turnOrder;


        public TurnOrderManager(BattleParty playerParty, BattleParty enemyParty)
        {
            fighters = new Array<BattleCharacter>();
            fighters.AddRange(playerParty.GetAllLivingMembers());
            fighters.AddRange(enemyParty.GetAllLivingMembers());
            InitTurnOrder();
        }


        public BattleCharacter GetCurrentCharacter()
        {
            return turnOrder[0];
        }

        public BattleCharacter SetupNextTurn()
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