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
            Utils.Print(this, "Damage isn't calculated yet");
            return 0;
        }
    }
}
