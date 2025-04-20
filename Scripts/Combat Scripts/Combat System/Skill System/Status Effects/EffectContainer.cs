using System;
using CombatSystem;
using Godot;
using Godot.Collections;

namespace SkillSystem
{
    public class EffectContainer
    {
        public BaseEffect Effect { get; private set; }
        public StatusContainer Status { get; private set; }
        public BattleCharacter Caster 
        { 
            get
            {
                return Status.Caster;
            }
        }
        public BattleCharacter Target 
        {
            get
            {
                return Status.Target;
            }
        }
        public BattleManager Battle { get; private set; }


        //Used for effects that shouldn't repeat (unlike poison effects)
        private bool isActive = false;


        public EffectContainer(BaseEffect effect, StatusContainer status, BattleManager battle)
        {
            Effect = effect;
            Status = status;
            Battle = battle;
        }

        public void ActivateEffect()
        {
            Effect.ActivateEffect(Caster, Target, this);
        }

        public void DeactivateEffect()
        {
            Effect.DeactivateEffect(Caster, Target, this);
        }
    }
}
