using System;
using Godot;
using Godot.Collections;

namespace GachaSystem{
    [GlobalClass]
    public partial class GachaGame : Resource
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
            return currentBanner.PullRandomCharacter(allCharacters);
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

            GameState.GetEventManager().RaiseCharacterListUpdatedEvent();
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