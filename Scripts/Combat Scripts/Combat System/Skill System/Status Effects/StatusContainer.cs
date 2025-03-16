using CombatSystem;
using System;
using Godot;
using Godot.Collections;

namespace SkillSystem
{
    public class StatusContainer
    {
        public StatusEffect Status { get; private set; }
        public int Level { get; private set; } = 1;
        public BattleCharacter Caster { get; private set; }
        public BattleCharacter Target { get; private set; }
        public int TurnsLeft { get; private set; } = 5;


        public StatusContainer(BattleCharacter caster, 
                               BattleCharacter target,
                               StatusEffect effect)
        {
            Status = effect;
            Caster = caster;
            Target = target;
            Level = effect.Level;
        }

        public void CountDown()
        {
            TurnsLeft--;
        }

        public void RefreshDuration(BattleCharacter caster, int turns)
        {
            Caster = caster;
            TurnsLeft = turns;
        }

        public bool DurationIsUp() { return TurnsLeft <= 0; }

        public Array<BaseEffect> GetEffectsList()
        {
            var result = new Array<BaseEffect>();
            result.AddRange(Status.Effects);
            return result;
        }
    }
}
