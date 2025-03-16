using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    public class BattleParty
    {
        private List<SimpleWeakRef<BattleCharacter>> LivingMembers = new List<SimpleWeakRef<BattleCharacter>>();
        private List<SimpleWeakRef<BattleCharacter>> DeadMembers = new List<SimpleWeakRef<BattleCharacter>>();
        private List<SimpleWeakRef<BattleCharacter>> TempMembers = new List<SimpleWeakRef<BattleCharacter>>();
        
        //private Array<> partyBuffs;

        public BattleParty(BattleState state, Array<CharacterData> characters, PackedScene battleCharacterScene)
        {
            foreach (var character in characters)
            {
                if (character != null)
                {
                    var newChar = Utils.InstantiateCopy<BattleCharacter>(battleCharacterScene);
                    newChar.InitCharacter(character, this);
                    state.AddChild(newChar);
                    AddMember(newChar);
                }
            }
            
        }





        public BattleParty(Array<BattleCharacter> characters)
        {
            foreach (var character in characters)
            {
                AddMember(character);
            }
        }

        public Array<BattleCharacter> GetAllLivingMembers()
        {
            var result = new Array<BattleCharacter>();

            foreach(var character in LivingMembers)
            {
                result.Add(character.Value);
            }

            return result;
        }

        public Array<BattleCharacter> GetAllMembers()
        {
            var result = new Array<BattleCharacter>();

            foreach(var character in LivingMembers)
            {
                result.Add(character.Value);
            }
            foreach(var character in DeadMembers)
            {
                result.Add(character.Value);
            }

            return result;
        }



        public bool IsPartyDead()
        {
            return LivingMembers.Count == 0;
        }

        public bool EntirePartyAlive()
        {
            return DeadMembers.Count == 0;
        }

        
        /// <summary>
        /// Run once after all damage calculations done
        /// </summary>
        public void CheckPartyHealth()
        {
            CheckLivingMembers();
            CheckDeadMembers();
        }

        private void CheckLivingMembers()
        {
            int index = 0;

            while (index < LivingMembers.Count)
            {
                var reference = LivingMembers[index];
                var member = reference.Value;

                if (member.IsDead())
                {
                    DeadMembers.Add(reference);
                    LivingMembers.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }
        }

        private void CheckDeadMembers()
        {
            int index = 0;

            while (index < DeadMembers.Count)
            {
                var reference = DeadMembers[index];
                var member = reference.Value;

                if (!member.IsDead())
                {
                    LivingMembers.Add(reference);
                    DeadMembers.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }
        }

        private void CheckTempMembers()
        {
            int index = 0;

            while (index < LivingMembers.Count)
            {
                var reference = LivingMembers[index];
                var member = reference.Value;

            }
        }

        private void AddMember(BattleCharacter character)
        {
            var _ref = new SimpleWeakRef<BattleCharacter>(character);
            LivingMembers.Add(_ref);
        }
    }
}
