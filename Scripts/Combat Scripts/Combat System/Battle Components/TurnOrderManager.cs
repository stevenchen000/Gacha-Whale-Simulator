using Godot;
using System;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CombatSystem
{
    public partial class TurnOrderManager : Node
    {
        private BattleState battleState;
        public List<CharacterTurnTime> turnOrder { get; private set; } = new List<CharacterTurnTime>();
        private BattleCharacter currentCharacter;
        private double turnTime = 0;

        [Signal]
        public delegate void TurnOrderChangedEventHandler();


        public void Init(BattleState state)
        {
            battleState = state;

            SetupInitialTurns();
            CalculateTurnOrder();
            turnTime = turnOrder[0].TurnTime;
            currentCharacter = turnOrder[0].Character;
        }


        public BattleCharacter GetCurrentCharacter()
        {
            return currentCharacter;
        }

        public double SetupNextTurn()
        {
            double turnProgress = 0;

            //Update curr character's time
            double charTurnTime = CalculateTurnTime(currentCharacter);
            currentCharacter.SetTurnTime(turnTime + charTurnTime);
            

            //Remove curr turn and recalculate
            CalculateTurnOrder();

            //Get time diff
            double newTurnTime = turnOrder[0].TurnTime;
            turnProgress = newTurnTime - turnTime;
            turnTime = turnOrder[0].TurnTime;
            currentCharacter = turnOrder[0].Character;

            return turnProgress;
        }

        public void DelayCharacter(BattleCharacter character, int numOfTurns)
        {
            double currTime = character.NextTurnTime;
            int priority = 0;
            for (int i = 0; i < numOfTurns; i++)
            {
                var result = GetDelayedTurnTime(character, currTime);
                currTime = result.Item1;
                priority = result.Item2;
            }

            character.SetTurnTime(currTime);
            character.SetTurnPriority(priority);
        }

        private Tuple<double, int> GetDelayedTurnTime(BattleCharacter character, double turnTime)
        {            
            var fighters = GetLivingCharacters();
            double nextTurn = 99999;
            int priority = 0;

            foreach (var fighter in fighters)
            {
                
                if (fighter != character)
                {
                    double temp = GetFirstTurnAfterTime(fighter, turnTime, character.TurnPriority);
                    if(temp < nextTurn)
                    {
                        nextTurn = temp;
                        priority = fighter.TurnPriority + 1;
                    }
                }
            }

            var result = new Tuple<double, int>(nextTurn, priority);
            return result;
        }

        private double GetFirstTurnAfterTime(BattleCharacter character, double turnTime, int priority)
        {
            double nextTurnTime = character.NextTurnTime;
            double turnSpeed = CalculateTurnTime(character);

            while(nextTurnTime < turnTime)
            {
                nextTurnTime += turnSpeed;
            }

            if (nextTurnTime == turnTime && priority > character.TurnPriority)
                nextTurnTime += turnSpeed;

            return nextTurnTime;
        }



        /***********
         * Helper Functions
         * **************/

        private void SetupInitialTurns()
        {
            var fighters = GetLivingCharacters();

            for(int i = 0; i < fighters.Count; i++)
            {
                var fighter = fighters[i];
                double turnTime = CalculateTurnTime(fighter)/10;
                fighter.SetTurnTime(turnTime);
                fighter.SetTurnPriority(GameState.GetRandomNumber(0,1000));
            }
        }

        private void CalculateTurnOrder()
        {
            turnOrder = new List<CharacterTurnTime>();
            var fighters = GetLivingCharacters();

            foreach(var fighter in fighters)
            {
                var turns = GetCharacterTurns(fighter, 3);
                turnOrder.AddRange(turns);
            }

            turnOrder = turnOrder.OrderBy(x => x.TurnTime).ThenBy(x => x.Character.TurnPriority).ToList();
            EmitSignal(SignalName.TurnOrderChanged);
        }

        private void PrintTurns()
        {
            foreach(var turn in turnOrder)
            {
                Utils.Print(this, $"{turn.Character.Character.Character.Name} - {turn.TurnTime}");
            }
        }

        private List<CharacterTurnTime> GetCharacterTurns(BattleCharacter character, int num)
        {
            List<CharacterTurnTime> result = new List<CharacterTurnTime>();
            double turnTime = CalculateTurnTime(character);
            double startingTime = character.NextTurnTime;

            for(int i = 0; i < num; i++)
            {
                double nextTurnTime = startingTime + turnTime * i;
                var nextTurn = new CharacterTurnTime(character, i, nextTurnTime);
                result.Add(nextTurn);
            }

            return result;
        }

        private double CalculateTurnTime(BattleCharacter character)
        {
            int speed = character.Stats.GetStat(StatNames.Speed);

            return 10_000 / (Mathf.Pow(speed, 1.5));
        }


        private Array<BattleCharacter> GetLivingCharacters()
        {
            return battleState.GetAllLivingCharacters();
        }








        public class CharacterTurnTime
        {
            public BattleCharacter Character { get; private set; }
            public double TurnTime = 0;
            public int QueueNumber { get; private set; }

            public CharacterTurnTime(BattleCharacter character, int turnNumber, double turnTime)
            {
                Character = character;
                QueueNumber = turnNumber;
                TurnTime = turnTime;
            }
        }
    }
}