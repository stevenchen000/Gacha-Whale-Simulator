using Godot;
using System.Collections.Generic;
using CombatSystem;

namespace SkillSystem
{
    public partial class EffectManager : Node
    {
        private SimpleWeakRef<BattleCharacter> _character;
        public BattleCharacter Character
        {
            get
            {
                return _character.Value;
            }
            private set
            {
                _character = new SimpleWeakRef<BattleCharacter>(value);
            }
        }


        public List<StatusContainer> StatusEffects { get; private set; } = new List<StatusContainer>();

        private List<EffectContainer> turnStartEffects = new List<EffectContainer>();
        private List<EffectContainer> turnEndEffects = new List<EffectContainer>();
        private List<EffectContainer> onApplyEffects = new List<EffectContainer>();
        private List<EffectContainer> onExpireEffects = new List<EffectContainer>();

        // Used for effects that run after taking damage from an enemy skill.
        // Not used for counters, burns, etc
        private List<EffectContainer> afterHurtEffects = new List<EffectContainer>();

        [Signal]
        public delegate void OnStatusChangedEventHandler();




        public override void _Ready()
        {
            Character = Utils.FindParentOfType<BattleCharacter>(this);
        }


        /***************
         * Run Effects
         * (Call these in combat)
         * *************/

        public void RunTurnStartEffects()
        {
            RunEffectList(turnStartEffects);
        }

        public void RunTurnEndEffects()
        {
            RunEffectList(turnEndEffects);
        }


        private void RunEffectList(List<EffectContainer> effectsList)
        {
            foreach(var effect in effectsList)
            {
                effect.ActivateEffect();
            }
        }

        public void ReduceStatusTime()
        {
            foreach (var effect in StatusEffects)
            {
                effect.CountDown();
            }
            RemoveExpiredEffects();

            RaiseEvent();
        }


        /*****************
         * Add Status
         * ***************/


        public void AddEffect(BattleCharacter caster, StatusEffect status, BattleManager battle)
        {
            if (HasStatus(status))
            {
                RefreshStatus(caster, status);
            }
            else
            {
                _AddNewEffect(caster, status, battle);
            }

            RaiseEvent();
        }

        private void _AddNewEffect(BattleCharacter caster, StatusEffect status, BattleManager battle)
        {
            var newStatus = new StatusContainer(caster, Character, status);
            StatusEffects.Add(newStatus);
            AddBaseEffects(newStatus, status, battle);
        }
        private void AddBaseEffects(StatusContainer container, StatusEffect status, BattleManager battle)
        {
            var effects = status.Effects;

            foreach (var effect in effects)
            {
                var timing = effect.Timing;
                var effectContainer = new EffectContainer(effect, container, battle);

                switch (timing)
                {
                    case EffectTiming.OnActivate:
                        onApplyEffects.Add(effectContainer);
                        effectContainer.ActivateEffect();
                        break;
                    case EffectTiming.OnExpire:
                        onExpireEffects.Add(effectContainer);
                        break;
                    case EffectTiming.OnTurnStart:
                        turnStartEffects.Add(effectContainer);
                        break;
                    case EffectTiming.OnTurnEnd:
                        turnEndEffects.Add(effectContainer);
                        break;
                }
            }
        }



        /*****************
         * Refresh Status
         * ***************/


        private void RefreshStatus(BattleCharacter caster, StatusEffect status)
        {
            var existingStatus = GetExistingStatus(status);

            if(CanRefresh(existingStatus, status))
                existingStatus.RefreshDuration(caster, status.Duration);
        }

        private bool CanRefresh(StatusContainer existingStatus, StatusEffect status)
        {
            int existingLevel = existingStatus.Level;
            int newLevel = status.Level;

            return newLevel >= existingLevel;
        }




        /******************
         * Remove Status
         * ****************/

        /// <summary>
        /// Removes effects without triggering OnExpire effects
        /// </summary>
        /// <param name="numOfEffects"></param>
        public void DispelEffects(int numOfEffects)
        {
            int total = Mathf.Min(numOfEffects, StatusEffects.Count);

            while (total > 0)
            {
                RemoveEffect(0, false);
                total--;
            }

            RaiseEvent();
        }


        private void RemoveEffect(int index, bool expiredNaturally = true)
        {
            var status = StatusEffects[index];
            _RemoveEffectFromList(status, onApplyEffects);
            _RemoveEffectFromList(status, turnStartEffects);
            _RemoveEffectFromList(status, turnEndEffects);
            _RemoveEffectFromList(status, onExpireEffects, expiredNaturally);
        }

        private void _RemoveEffectFromList(StatusContainer status, List<EffectContainer> list, bool activateEffect = false)
        {
            int index = 0;

            while(index < list.Count)
            {
                var effect = list[index];
                if(effect.Status == status)
                {
                    list.RemoveAt(index);
                    if(activateEffect)
                        effect.ActivateEffect();
                    effect.DeactivateEffect();
                }
                else
                {
                    index++;
                }
            }
        }

        private void RemoveExpiredEffects()
        {
            int index = 0;
            while (index < StatusEffects.Count)
            {
                var effect = StatusEffects[index];

                if (effect.DurationIsUp())
                {
                    RemoveEffect(index);
                }
                else
                {
                    index++;
                }
            }
        }




        /*********************
         * Helpers
         * *****************/


        private bool HasStatus(StatusEffect status)
        {
            bool hasEffect = false;
            for (int i = 0; i < StatusEffects.Count; i++)
            {
                //check if caster has status already
                var currStatus = StatusEffects[i];
                if (currStatus.Status == status)
                {
                    hasEffect = true;
                    break;
                }
            }

            return hasEffect;
        }


        private StatusContainer GetExistingStatus(StatusEffect status)
        {
            StatusContainer result = null;

            for (int i = 0; i < StatusEffects.Count; i++)
            {
                var effect = StatusEffects[i];
                if (effect.Status == status)
                {
                    result = effect;
                    break;
                }
            }

            return result;
        }

        private void RaiseEvent()
        {
            EmitSignal(SignalName.OnStatusChanged);
        }
    }
}