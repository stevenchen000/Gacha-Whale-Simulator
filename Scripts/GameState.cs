using Godot;
using System;

public partial class GameState : Node
{
    public static GameState state;

    public static int currentPower = 0;
    public static int premiumCurrency = 0;
    public static int day = 0;
    public static int hour = 8;
    public static int minute = 0;
    public static int money = 10_000;
    public static int salary = 5_000;



    public GameState(){
        if(state == null){
            state = this;
        }
    }

    /****************
    * Gacha Functions
    *****************/

    public static void AddPower(int power){
        currentPower += power;
    }

    public static void AddPremiumCurrency(int amount){
        premiumCurrency += amount;
    }

    public static void UsePremiumCurrency(int cost){
        if(HasEnoughPremiumCurrency(cost)){
            premiumCurrency -= cost;
        }
    }

    public static bool HasEnoughPremiumCurrency(int cost){
        return premiumCurrency >= cost && cost >= 0;
    }




    /****************
    * Work-related Functions
    ****************/

    public static void Work(){
        if(IsTimeForWork()){
            hour = 17;
            minute = 30;
        }
    }

    public static bool IsTimeForWork(){
        return hour == 8;
    }

    
    /*****************
    * Mood-Related Functions
    ******************/

    public static void GoToSleep(){
        hour = 8;
        minute = 0;
        day++;
        if(day % 15 == 0){
            money += salary;
        }
    }

    public static bool TimeForBed(){
        return true;
    }
}
