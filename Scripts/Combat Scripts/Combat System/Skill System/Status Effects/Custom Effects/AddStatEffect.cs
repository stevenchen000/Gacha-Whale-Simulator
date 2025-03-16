using CombatSystem;
using Godot;
using System;

namespace SkillSystem
{
    [GlobalClass]
    public partial class AddStatEffect : BaseEffect
    {
        [Export] private StatType stat;

        [Export(PropertyHint.MultilineText)] private string description;

        public override void ActivateEffect(BattleCharacter caster,
                                            BattleCharacter target,
                                            EffectContainer container)
        {
            float totalMultiplier = GetTotalAmount(container);
            target.Stats.AddStatMultiplier(stat, totalMultiplier);
            Utils.Print(this, "Added stat. Caster now at {}% multiplier");
        }

        public override void DeactivateEffect(BattleCharacter caster,
                                              BattleCharacter target,
                                              EffectContainer container)
        {
            float totalMultiplier = GetTotalAmount(container);
            target.Stats.AddStatMultiplier(stat, -totalMultiplier);
        }

        private float GetTotalAmount(EffectContainer container)
        {
            int level = container.Status.Level;
            return Potency * level;
        }
    }
}