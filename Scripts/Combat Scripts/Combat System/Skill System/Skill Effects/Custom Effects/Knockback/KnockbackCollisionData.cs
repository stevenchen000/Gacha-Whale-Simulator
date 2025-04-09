using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSystem
{
    public class KnockbackCollisionData
    {
        public BattleCharacter KnockedbackCharacter { get; private set; }
        public BattleCharacter KnockedIntoCharacter { get; private set; }
        public int NumberOfSpacesLeft { get; private set; }
        public CharacterDirection KnockbackDirection { get; private set; }

        public KnockbackCollisionData(BattleCharacter knockedbackCharacter, BattleCharacter knockedIntoCharacter, int numberOfSpacesLeft, CharacterDirection knockbackDirection)
        {
            KnockedbackCharacter = knockedbackCharacter;
            KnockedIntoCharacter = knockedIntoCharacter;
            NumberOfSpacesLeft = numberOfSpacesLeft;
            KnockbackDirection = knockbackDirection;
        }

        public bool ObstacleIsEnemy()
        {
            return KnockedbackCharacter.IsEnemy(KnockedIntoCharacter);
        }
    }
}
