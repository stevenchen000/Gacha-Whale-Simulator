using System;
using System.Collections.Generic;

namespace CombatSystem
{
    public class CharacterDamageManager
    {
        private Dictionary<BattleCharacter, CharacterDamageData> _dict;

        public CharacterDamageManager()
        {
            _dict = new Dictionary<BattleCharacter, CharacterDamageData>();
        }

        public int GetTotalHealthDamage(BattleCharacter character)
        {
            AddMissingCharacter(character);
            var charData = GetCharacterDamageData(character);
            return charData.TotalHpDamage;
        }

        public int GetPreviousHealthDamage(BattleCharacter character)
        {
            AddMissingCharacter(character);
            var charData = GetCharacterDamageData(character);
            return charData.PreviousHpDamage;
        }

        public int GetTotalAmpDamage(BattleCharacter character)
        {
            AddMissingCharacter(character);
            var charData = GetCharacterDamageData(character);
            return charData.TotalAmpDamage;
        }

        public int GetPreviousAmpDamage(BattleCharacter character)
        {
            AddMissingCharacter(character);
            var charData = GetCharacterDamageData(character);
            return charData.PreviousAmpDamage;
        }



        public void AddDamage(BattleCharacter character, int damage, DamageType type)
        {
            AddMissingCharacter(character);
            var charData = _dict[character];
            int turn = character.ActionsTaken;

            if (type == DamageType.HealthDamage)
                charData.AddHpDamage(damage, turn);
            else if(type == DamageType.AmpDamage)
                charData.AddAmpDamage(damage, turn);
        }

        private void AddMissingCharacter(BattleCharacter character)
        {
            if (!_dict.ContainsKey(character))
            {
                var data = new CharacterDamageData();
                _dict[character] = data;
            }
        }

        private CharacterDamageData GetCharacterDamageData(BattleCharacter character)
        {
            return _dict[character];
        }
    }
}
