using Godot;
using System;
using GachaSystem;
using Godot.Collections;

public partial class GameState : Node
{
    public static GameState state;

    [Export] public int currentPower = 0;
    [Export] public int premiumCurrency = 0;
    [Export] public int upgradeCurrency = 0;
    [Export] public int day = 0;
    [Export] public int hour = 8;
    [Export] public int minute = 0;
    [Export] private DateTime time;
    
    
    [Export] public int money = 10_000;
    [Export] public int salary = 5_000;
    [Export] public GachaCharacter[] allCharacters;
    [Export] public GachaCharacter[] ownedCharacters;

    [Export] public GachaGame game { get; set; }


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    //Called when object is about to get deleted
    public override void _Notification(int what)
    {
        if(what == NotificationPredelete)
        {

        }
    }

    public GameState(){
        if(state == null){
            state = this;
        }
    }


    public static GachaGame GetGame()
    {
        return state.game;
    }


    /****************
    * Gacha Functions
    *****************/

    public static void AddPower(int power){
        state.currentPower += power;
    }

    public static void AddPremiumCurrency(int amount){
        state.premiumCurrency += amount;
    }

    public static void UsePremiumCurrency(int cost){
        if(HasEnoughPremiumCurrency(cost)){
            state.premiumCurrency -= cost;
        }
    }

    public static bool HasEnoughPremiumCurrency(int cost){
        return state.premiumCurrency >= cost && cost >= 0;
    }

    public static void AddUpgradeCurrency(int amount){
        state.upgradeCurrency += amount;
    }

    public static bool UseUpgradeCurrency(int cost){
        bool hasEnough = HasEnoughUpgradeCurrency(cost);

        if(hasEnough){
            state.upgradeCurrency -= cost;
        }

        return hasEnough;
    }

    public static bool HasEnoughUpgradeCurrency(int cost){
        return state.upgradeCurrency >= cost;
    }

    public static GachaCharacter PullRandomGachaCharacter(){
        return state.game.PullRandomCharacter();
    }

    public static void AddCharacterToAccount(GachaCharacter character){
        state.game.AddCharacterToAccount(character);
    }

    public static int GetNumberOfCharacters()
    {
        return state.game.GetNumberOfCharacters();
    }

    public static Array<GachaCharacterData> GetAllOwnedCharacters()
    {
        return state.game.ownedCharacters;
    }


    /****************
    * Work-related Functions
    ****************/

    public static void Work(){
        if(IsTimeForWork()){
            state.hour = 17;
            state.minute = 30;
        }
    }

    public static bool IsTimeForWork(){
        return state.hour == 8;
    }

    
    /*****************
    * Mood-Related Functions
    ******************/

    public static void GoToSleep(){
        state.hour = 8;
        state.minute = 0;
        state.day++;
        if(state.day % 15 == 0){
            state.money += state.salary;
        }
    }

    public static bool TimeForBed(){
        return true;
    }
}
