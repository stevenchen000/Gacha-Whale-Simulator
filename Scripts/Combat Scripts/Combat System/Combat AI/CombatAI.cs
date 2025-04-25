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


        public virtual CombatActionData CalculateAction(BattleManager battle, BattleCharacter caster, Array<BattleCharacter> targets)
        {
            return null;
        }

        protected Array<BattleCharacter> GetValidTargets(BattleManager battle, BattleCharacter caster, TargetType targetType)
        {
            var state = battle.State;
            var result = new Array<BattleCharacter>();

            switch (targetType)
            {
                case TargetType.Ally:
                    result = GetAllies(battle, caster);
                    break;
                case TargetType.Enemy:
                    result = GetEnemies(battle, caster);
                    break;
            }

            return result;
        }

        protected Array<BattleCharacter> GetAllies(BattleManager battle, BattleCharacter caster)
        {
            var state = battle.State;
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
    }
}