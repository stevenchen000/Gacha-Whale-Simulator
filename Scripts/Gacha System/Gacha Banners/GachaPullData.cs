using System;


namespace GachaSystem
{
    public class GachaPullData
    {
        public GameCharacter CharacterPulled { get; private set; }
        public bool IsNew { get; private set; }

        public GachaPullData(GameCharacter characterPulled, bool isNew)
        {
            CharacterPulled = characterPulled;
            IsNew = isNew;
        }
    }
}
