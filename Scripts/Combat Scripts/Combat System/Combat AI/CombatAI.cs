using Godot;
using Godot.Collections;
using System;

namespace CombatSystem
{
    [Tool]
    [GlobalClass]
    public partial class CombatAI : Resource
    {
        [Export(PropertyHint.MultilineText)] protected string description;


        public virtual CombatActionData CalculateAction(BattleManager battle)
        {
            return null;
        }

        protected Array<BattleCharacter> GetValidTargets(BattleState state, BattleCharacter caster, TargetType targetType)
        {
            var result = new Array<BattleCharacter>();

            switch (targetType)
            {
                case TargetType.Ally:
                    result = GetAllies(state, caster);
                    break;
                case TargetType.Enemy:
                    result = GetEnemies(state, caster);
                    break;
            }

            return result;
        }

        protected Array<BattleCharacter> GetAllies(BattleState state, BattleCharacter caster)
        {
            var result = new Array<BattleCharacter>();
            var fighters = state.GetAllLivingCharacters();

            for(int i = 0; i < fighters.Count; i++)
            {
                var character = fighters[i];
                if (!caster.IsEnemy(character) && caster != character)
                {
                    result.Add(character);
                }
            }

            return result;
        }

        protected Array<BattleCharacter> GetEnemies(BattleState state, BattleCharacter caster)
        {
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
    }
}