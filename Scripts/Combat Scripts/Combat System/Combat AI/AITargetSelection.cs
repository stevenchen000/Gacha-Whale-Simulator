using Godot;
using Godot.Collections;
using System;
using System.Linq;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class AITargetSelection : Resource
    {
        [Export] private string description = "";

        public virtual Array<BattleCharacter> GetTargets(BattleManager battle, BattleCharacter caster)
        {
            return new Array<BattleCharacter>();
        }


        protected Array<BattleCharacter> GetAllies(BattleManager battle, BattleCharacter caster)
        {
            var state = battle.State;
            var result = new Array<BattleCharacter>();
            var fighters = state.GetAllLivingCharacters();

            for (int i = 0; i < fighters.Count; i++)
            {
                var character = fighters[i];
                if (!caster.IsEnemy(character) && caster != character)
                {
                    result.Add(character);
                }
            }

            return result;
        }

        protected Array<BattleCharacter> GetEnemies(BattleManager battle, BattleCharacter caster)
        {
            var state = battle.State;
            var result = new Array<BattleCharacter>();
            var fighters = state.GetAllLivingCharacters();

            for (int i = 0; i < fighters.Count; i++)
            {
                var character = fighters[i];
                if (caster.IsEnemy(character) && caster != character)
                {
                    result.Add(character);
                }
            }

            return result;
        }

        protected Array<BattleCharacter> SortArray(Array<BattleCharacter> enemies, Func<BattleCharacter, int> sort, bool descending = false)
        {
            System.Collections.Generic.List<BattleCharacter> tempList = new();
            tempList.AddRange(enemies);
            if(!descending)
                tempList = tempList.OrderBy(sort).ToList();
            else
                tempList = tempList.OrderByDescending(sort).ToList();


                var result = new Array<BattleCharacter>();
            result.AddRange(tempList);

            return result;
        }
    }
}