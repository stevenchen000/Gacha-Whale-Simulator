using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSystem
{
    public class DamageData
    {
        public BattleCharacter Target { get; private set; }
        public int Damage { get; private set; }
        public bool IsHeal { get; private set; } = false;
        public bool IsCrit { get; private set; } = false;
        public bool IsHealthDamage { get; private set; } = false;

        
    }
}
