using System;
using Godot;
using Godot.Collections;
using CombatSystem;
using EventSystem;

namespace GachaSystem{
    [GlobalClass]
    public partial class GachaGame : Node, ISavable
    {
        [Export] private string gameName;
        //game icon
        //game leaderboard
        //game points
        [Export] private int accountPower;
        //list of currencies

        [Export] public int currentPower = 0;
        [Export] public int premiumCurrency { get; private set; } = 0;
        [Export] public int upgradeCurrency { get; private set; } = 0;

        [ExportCategory("Character Data")]
        [Export] public CharacterDictionary allCharacters;
        [Export] public GachaBanner currentBanner;
        [Export] public int updateNumber = 0;

        [Export] private OwnedCharacterData characterData;
        [Export] public Array<PartySetup> PlayerParties { get; private set; }

        [ExportCategory("Events")]
        [Export] private VoidEvent OnGemCountChanged;


        //Flag names
        private string playerPartyIdFlag = "player_current_party_id";


        public override void _Ready()
        {
            //characterData.Init();

        }

        public override void _Notification(int what)
        {
            if(what == NotificationWMCloseRequest || what == NotificationApplicationPaused)
            {
                characterData.Save();
            }
        }



        public void AddPremiumCurrency(int amount){
            premiumCurrency += amount;
            OnGemCountChanged.RaiseEvent(this);
        }

        public bool UsePremiumCurrency(int cost){
            bool result = false;
            
            if(HasEnoughPremiumCurrency(cost)){
                premiumCurrency -= cost;
                result = true;
            }
            OnGemCountChanged.RaiseEvent(this);

            return result;
        }

        public bool HasEnoughPremiumCurrency(int cost){
            return premiumCurrency >= cost && cost >= 0;
        }

        public void AddUpgradeCurrency(int amount){
            upgradeCurrency += amount;
        }

        public bool UseUpgradeCurrency(int cost){
            bool hasEnough = HasEnoughUpgradeCurrency(cost);

            if(hasEnough){
                upgradeCurrency -= cost;
            }

            return hasEnough;
        }

        public bool HasEnoughUpgradeCurrency(int cost){
            return upgradeCurrency >= cost;
        }

        /****************
        * Character Functions
        *****************/

        public void AddCharacterToAccount(GameCharacter character){
            characterData.AddCharacterOrDupes(character);
        }

        public int GetNumberOfCharacters()
        {
            return characterData.OwnedCharacters.Count;
        }

        public Array<CharacterData> GetAllCharacterData()
        {
            return characterData.OwnedCharacters;
        }

        public GameCharacter GetCharacterByID(int id)
        {
            return allCharacters.GetCharacterByID(id);
        }

        public CharacterData GetOwnedCharacterByID(int id)
        {
            return characterData.GetCharacterByID(id);
        }

        public PartySetup GetParty(int number)
        {
            if(number < PlayerParties.Count)
                return PlayerParties[number];
            else 
                return null;
        }

        public int GetNumberOfParties()
        {
            return PlayerParties.Count;
        }


        /*****************
         * Save and Load
         * ***************/

        public void Save()
        {
            SaveOwnedCharacterData();
            SavePartySetups();
        }

        #region Save Functions

        private static string ownedCharacterFilename = "user://ownedcharacters.json";
        private static string partiesFilename = "user://playerparties.json";

        private void SaveOwnedCharacterData()
        {
            /*var file = FileAccess.Open(ownedCharacterFilename, FileAccess.ModeFlags.Write);

            var data = new Dictionary<string, Variant>();

            for (int i = 0; i < characterData.OwnedCharacters.Count; i++)
            {
                var character = characterData.OwnedCharacters[i];
                data[i.ToString()] = character;
            }

            string json = Json.Stringify(data);
            file.StorePascalString(json);
            file.Close();*/
            characterData.Save();
        }

        private void SavePartySetups()
        {
            foreach(var party in PlayerParties)
            {
                party.Save();
            }
        }

        #endregion

        public void Load()
        {
            int partyId = GameState.GetFlagAmount(playerPartyIdFlag);
            var currParty = PlayerParties[partyId];
            GameState.SetCurrentParty(currParty);

            characterData.Init();
            foreach(var party in PlayerParties)
            {
                party.Load();
            }
        }

        #region Load Functions

        private void LoadOwnedCharacterData()
        {
            /*Utils.Print(this, "Loading characters...");
            var file = FileAccess.Open(ownedCharacterFilename, FileAccess.ModeFlags.Read);
            string json = file.GetPascalString();
            var data = (Dictionary<string, Variant>)Json.ParseString(json);

            var list = new Array<Variant>();
            list.AddRange(data.Values);

            file.Close();*/
        }

        private void LoadPartySetups()
        {
            /*var file = FileAccess.Open(partiesFilename, FileAccess.ModeFlags.Read);
            string json = file.GetPascalString();
            var data = (Array<string>)Json.ParseString(json);

            PlayerParties = new Array<PartySetup>();

            for(int i = 0; i < data.Count; i++)
            {
                var partyData = data[i];
                var party = new PartySetup(partyData);
                PlayerParties.Add(party);
            }
            
            file.Close();*/
        }

        #endregion
    }
}