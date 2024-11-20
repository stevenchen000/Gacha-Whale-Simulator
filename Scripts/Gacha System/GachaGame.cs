using System;
using Godot;

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
        [Export] public GachaCharacter[] allCharacters;
        [Export] public GachaCharacter[] ownedCharacters;




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

        public GachaCharacter PullRandomGachaCharacter(){
            int length = allCharacters.Length;
            return allCharacters[0];
        }

        
        public void AddPower(int power){
            currentPower += power;
        }
    }
}