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
            var casterStats = caster.Stats;
            var targetStats = target.Stats;
            int atk = casterStats.GetStat(StatNames.Attack);
            int def = targetStats.GetStat(StatNames.Defense);
            int baseDamage = atk - def / 2;
            int totalDamage = (int)(baseDamage * potency / 100);
            Utils.Print(this, "Damage isn't calculated yet");
            return totalDamage;
        }
    }
}
