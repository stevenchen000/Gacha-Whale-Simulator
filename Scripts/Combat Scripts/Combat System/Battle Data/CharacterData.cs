using System;
using Godot;
using Godot.Collections;

namespace CombatSystem
{
    [GlobalClass]
    public partial class CharacterData : Resource
    {

        [Export] public GameCharacter Character { get; private set; }

        [ExportCategory("Level")]
        [Export] public int Level { get; private set; } = 1;
        public int LevelCap { get { return GetLevelCap(); } }
        [Export] public int CurrExp { get; private set; }
        public int ExpToNextLevel 
        { 
            get
            {
                return CharacterExpCurve.GetExpForNextLevel(Level);
            }
        }

        [ExportCategory("Rarity")]
        [Export] public CharacterRarity Rarity { get; private set; } = CharacterRarity.SR;
        public CharacterRarity BaseRarity
        {
            get
            {
                return Character.BaseRarity;
            }
        }
        [Export] public int Stars { get; private set; } = 0;
        [Export] public int ExtraCopies { get; private set; }

        [ExportCategory("Overrides")]
        [Export] public string PortraitOverride { get; private set; }




        public CharacterData()
        {

        }

        public CharacterData(GameCharacter character)
        {
            Character = character;
            Rarity = BaseRarity;
        }

        public CharacterData(string json)
        {
            try
            {
                LoadFromJson(json);
            }
            catch (Exception e)
            {
                Utils.Print(this, "Couldn't load file");
            }
        }



        public CharacterPortrait GetPortrait() { return Character.GetPortrait(); }


        /******************
         * Skills
         * ***************/

        public Array<CharacterSkill> GetSkills() 
        {
            var skills = new Array<CharacterSkill>();
            var basicSkills = GetBasicSkills();
            var charSkills = Character.GetSkills(BaseRarity, Rarity, Stars);

            skills.AddRange(basicSkills);
            skills.AddRange(charSkills);

            return skills;
        }

        private Array<CharacterSkill> GetBasicSkills()
        {
            var result = new Array<CharacterSkill>();
            int tier = GetRarityDifference();
            var role = Character.Role;

            var ampAtk = role.AmpAttackBranch.GetSkillAtTier(tier);
            var hpAtk = role.HpAttackBranch.GetSkillAtTier(tier);

            result.Add(ampAtk);
            result.Add(hpAtk);
            //Utils.Print(this, $"{role.Name} - {ampAtk.SkillName} - {hpAtk.SkillName}");

            return result;
        }

        public int GetRarityDifference()
        {
            return (int)Rarity - (int)BaseRarity;
        }
        /***************
         * Experience Functions
         * **************/

        public void AddExp(int exp)
        {
            CurrExp += exp;

            while (CurrExp >= ExpToNextLevel &&
                   Level < LevelCap)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            CurrExp -= ExpToNextLevel;
        }

        private int GetLevelCap()
        {
            return (int)Rarity + 20;
        }

        /*****************
         * Gacha Functions
         * ***************/

        public void AddCopies(int copies)
        {
            ExtraCopies += copies;
        }

        public void RemoveCopies(int copies)
        {
            ExtraCopies -= copies;
        }

        public void LimitBreak()
        {
            if(CopiesNeededForUpgrade() >= ExtraCopies)
            {
                if (Stars < 5)
                {
                    Stars++;
                }
                else if(Rarity == CharacterRarity.R)
                {
                    Rarity = CharacterRarity.SR;
                    Stars = 0;
                }else if(Rarity == CharacterRarity.SR)
                {
                    Rarity = CharacterRarity.SSR;
                    Stars = 0;
                }
                ExtraCopies -= CopiesNeededForUpgrade();
            }
        }

        public int CopiesNeededForUpgrade()
        {
            int result = -1;

            switch (BaseRarity)
            {
                case CharacterRarity.R:
                    result = 10;
                    break;
                case CharacterRarity.SR:
                    result = 3;
                    break;
                case CharacterRarity.SSR:
                    result = 1;
                    break;
            }

            return result;
        }



        /**************
         * Save Load
         * ***********/


        public string ToJson()
        {
            var data = new Dictionary<string, Variant>();

            int id = Character.ID;
            data["id"] = id;
            data["level"] = Level;
            data["exp"] = CurrExp;
            data["rarity"] = (int)Rarity;
            data["stars"] = Stars;
            data["copies"] = ExtraCopies;

            var json = Json.Stringify(data, "\t");

            return json;
        }

        public void LoadFromJson(string json) 
        {
            var data = (Dictionary<string, Variant>)Json.ParseString(json);
            
            int id = (int)data["id"];
            Character = GameState.GetCharacterByID(id);
            Level = (int)data["level"];
            CurrExp = (int)data["exp"];
            Rarity = (CharacterRarity)(int)data["rarity"];
            Stars = (int)data["stars"];
            ExtraCopies = (int)data["copies"];
        }
    }
}
