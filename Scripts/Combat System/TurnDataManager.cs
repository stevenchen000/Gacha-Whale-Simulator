using System;
using Godot;
using System.Collections.Generic;

namespace CombatSystem
{
    public partial class TurnDataManager : Node
    {
        private List<TurnData> turnData;
        public TurnData currentTurn { get; private set; }

        public override void _Ready()
        {
            turnData = new List<TurnData>();
        }

        public void AddTurnData(TurnData data)
        {
            turnData.Add(data);
        }

        public TurnData GetTurnData(int turnNumber)
        {
            TurnData result = null;
            if(turnData.Count > turnNumber)
            {
                result = turnData[turnNumber];
            }
            return result;
        }

        public List<TurnData> GetAllTurnData()
        {
            var result = new List<TurnData>();
            result.AddRange(turnData);
            return result;
        }

        public void AddNewTurn(TurnData data)
        {
            turnData.Add(data);
        }
    }
}
