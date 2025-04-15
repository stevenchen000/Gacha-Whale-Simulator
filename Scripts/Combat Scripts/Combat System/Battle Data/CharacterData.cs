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
        public CharacterRarity MaxRarity
        {
            get
            {
                int max = (int)Character.Powercreep.GetMaxRarity();
                int min = (int)BaseRarity;
                int highest = Math.Max(max, min);
                return (CharacterRarity)highest;
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

        public LevelUpData AddExp(int exp)
        {
            LevelUpData data = null;
            double prevPercent = (double)CurrExp / ExpToNextLevel * 100;
            int prevLevel = Level;
            CurrExp += exp;

            LevelUp();

            double newExpPercent = (double)CurrExp / ExpToNextLevel * 100;
            int newLevel = Level;
            data = new LevelUpData(this, prevLevel, newLevel, prevPercent, newExpPercent);
            return data;
        }

        private void LevelUp()
        {
            while (CurrExp >= ExpToNextLevel &&
                   Level < LevelCap)
            {
                Level++;
                CurrExp -= ExpToNextLevel;
            }

            CapExp();
        }

        private void CapExp()
        {
            if(Level == LevelCap)
            {
                CurrExp = 0;
            }
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

        public void LimitBreakWithCopies()
        {
            if(CopiesNeededForUpgrade() <= ExtraCopies)
            {
                _LimitBreak();
                ExtraCopies -= CopiesNeededForUpgrade();
            }
        }

        public void LimitBreak()
        {
            _LimitBreak();
        }

        private void _LimitBreak()
        {
            if (Stars < 5)
            {
                Stars++;
            }
            else
            {
                int currRarity = (int)Rarity;
                int newRarity = currRarity + 1;
                Rarity = (CharacterRarity)newRarity;
                Stars = 0;
            }
        }


        public int CopiesNeededForUpgrade()
        {
            return LimitBreakAmounts.GetCopiesNeeded(this);
        }

        public int KeysNeededForUpgrade()
        {
            int rarity = (int)BaseRarity;
            return CopiesNeededForUpgrade() * (int)Math.Pow(10, rarity);
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
            FixRarity();
            Stars = (int)data["stars"];
            ExtraCopies = (int)data["copies"];
        }

        private void FixRarity()
        {
            if((int)Rarity < (int)BaseRarity)
            {
                Rarity = BaseRarity;
            }
        }
    }
}
