using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    public class BattleParty
    {
        private List<BattleCharacter> LivingMembers = new List<BattleCharacter>();
        private List<BattleCharacter> DeadMembers = new List<BattleCharacter>();
        private List<BattleCharacter> TempMembers = new List<BattleCharacter>();
        
        //private Array<> partyBuffs;

        public BattleParty(BattleState state, 
                           Array<CharacterData> characters, 
                           PackedScene battleCharacterScene,
                           PortraitBorder border)
        {
            foreach (var character in characters)
            {
                if (character != null)
                {
                    var newChar = Utils.InstantiateCopy<BattleCharacter>(battleCharacterScene);
                    newChar.InitCharacter(character, this);
                    newChar.ChangeBorder(border);
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
                result.Add(character);
            }

            return result;
        }

        public Array<BattleCharacter> GetAllMembers()
        {
            var result = new Array<BattleCharacter>();

            foreach(var character in LivingMembers)
            {
                result.Add(character);
            }
            foreach(var character in DeadMembers)
            {
                result.Add(character);
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
                var member = LivingMembers[index];

                if (member.IsDead())
                {
                    member.RemoveFromField();
                    DeadMembers.Add(member);
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
                var member = DeadMembers[index];

                if (!member.IsDead())
                {
                    LivingMembers.Add(member);
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
                var member = reference;

            }
        }

        private void AddMember(BattleCharacter character)
        {
            LivingMembers.Add(character);
        }
    }
}
