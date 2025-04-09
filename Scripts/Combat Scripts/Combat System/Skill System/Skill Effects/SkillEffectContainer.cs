using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSystem
{
    public class SkillEffectContainer
    {
        private SkillEffect effect;
        public BattleCharacter target { get; private set; }

        public SkillEffectContainer(SkillEffect effect, BattleCharacter target, TurnData data)
        {
            
        }



        public void StartEffect()
        {

        }

        public bool RunEffect()
        {
            return false;
        }

        public void EndEffect()
        {

        }
    }
}
