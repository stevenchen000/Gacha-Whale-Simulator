using System;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    [GlobalClass]
    public partial class BaseDamageFormula : Resource
    {
        public virtual int CalculateDamage(BattleCharacter caster, BattleCharacter target, double potency)
        {
            var casterStats = caster.stats;
            var targetStats = target.stats;
            int atk = casterStats.attack;
            int def = targetStats.defense;
            int baseDamage = atk - def / 2;
            int totalDamage = (int)(baseDamage * potency / 100);
            GD.Print("Damage isn't calculated yet");
            return totalDamage;
        }
    }
}
