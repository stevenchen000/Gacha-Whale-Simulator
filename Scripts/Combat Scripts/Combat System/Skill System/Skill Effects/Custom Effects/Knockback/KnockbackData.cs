using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSystem
{
    public class KnockbackData
    {
        public BattleCharacter KnockedCharacter { get; private set; }
        public int SpacesKnockedBack { get; private set; }
        public CharacterDirection KnockedDirection { get; private set; }

        public KnockbackData(BattleCharacter character, int spaces, CharacterDirection direction) 
        { 
            KnockedCharacter = character;
            SpacesKnockedBack = spaces;
            KnockedDirection = direction;
        }
    }
}
