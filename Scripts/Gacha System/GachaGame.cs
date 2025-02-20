using System;
using Godot;
using Godot.Collections;
using EventSystem;

namespace GachaSystem{
    [GlobalClass]
    public partial class GachaGame : Node
    {
        [Export] private string gameName;
        //game icon
        //game leaderboard
        //game points
        [Export] private int accountPower;
        //list of currencies

        [Export] public int currentPower = 0;
        [Export] public int premiumCurrency = 0;
        [Export] public int upgradeCurrency = 0;
        [Export] public GachaCharacterList allCharacters;
        [Export] public Array<GachaCharacterData> ownedCharacters;
        [Export] public GachaBanner currentBanner;
        //[Export] public GachaCharacterPower powerList;
        [Export] public int updateNumber = 0;

        [Export] public GachaCharacterEvent OnCharacterPulled;
        [Export] public VoidEvent OnAccountListUpdated;

        public override void _Ready()
        {
            OnCharacterPulled.SubscribeEvent(AddCharacterToAccount);
        }



        public void AddPremiumCurrency(int amount){
            premiumCurrency += amount;
        }

        public void UsePremiumCurrency(int cost){
            if(HasEnoughPremiumCurrency(cost)){
                premiumCurrency -= cost;
            }
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

        public GachaCharacter PullRandomCharacter(){
            var character = currentBanner.PullRandomCharacter(allCharacters);
            OnCharacterPulled.RaiseEvent(this, character);

            return character;
        }

        public void AddCharacterToAccount(GachaCharacter character){
            var characterData = GetDataIfCharacterOwned(character);

            if(characterData == null)
            {
                characterData = new GachaCharacterData(character);
                ownedCharacters.Add(characterData);
            }
            else
            {
                characterData.numberOfCopies++;
            }

            if(OnAccountListUpdated != null)
                OnAccountListUpdated.RaiseEvent(this);
        }

        public GachaCharacterData GetDataIfCharacterOwned(GachaCharacter character)
        {
            GachaCharacterData data = null;

            foreach(var characterData in ownedCharacters)
            {
                if(characterData.baseCharacter == character)
                {
                    data = characterData;
                    break;
                }
            }

            return data;
        }

        
        public void AddPower(int power){
            currentPower += power;
        }

        public int GetNumberOfCharacters()
        {
            return ownedCharacters.Count;
        }
    }
}