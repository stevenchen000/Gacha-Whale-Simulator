using System;
using Godot;
using System.Collections.Generic;

namespace CombatSystem
{
    public partial class TurnDataManager : Node
    {
        private List<BattleSkillCastData> turnData;
        public BattleSkillCastData currentTurn { get; private set; }

        public override void _Ready()
        {
            turnData = new List<BattleSkillCastData>();
        }

        public void AddTurnData(BattleSkillCastData data)
        {
            turnData.Add(data);
        }

        public BattleSkillCastData GetTurnData(int turnNumber)
        {
            BattleSkillCastData result = null;
            if(turnData.Count > turnNumber)
            {
                result = turnData[turnNumber];
            }
            return result;
        }

        public List<BattleSkillCastData> GetAllTurnData()
        {
            var result = new List<BattleSkillCastData>();
            result.AddRange(turnData);
            return result;
        }

        public void AddNewTurn(BattleSkillCastData data)
        {
            turnData.Add(data);
        }
    }
}
