using CombatSystem;
using Godot;
using System;

namespace SkillSystem
{
    [Tool]
    [GlobalClass]
    public partial class AddStatEffect : BaseEffect
    {
        [Export] private StatType stat;
        [Export] private StatMultiplierType multiplierType;

        [Export(PropertyHint.MultilineText)] private string description;

        public override void ActivateEffect(BattleCharacter caster,
                                            BattleCharacter target,
                                            EffectContainer container)
        {
            float totalMultiplier = GetTotalAmount(container);
            target.Stats.AddStatMultiplier(stat, multiplierType, totalMultiplier);
            
        }

        public override void DeactivateEffect(BattleCharacter caster,
                                              BattleCharacter target,
                                              EffectContainer container)
        {
            float totalMultiplier = GetTotalAmount(container);
            target.Stats.AddStatMultiplier(stat, multiplierType, -totalMultiplier);
        }

        private float GetTotalAmount(EffectContainer container)
        {
            int level = container.Status.Level;
            return Potency * level;
        }
    }
}