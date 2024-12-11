using Godot;
using System;
using GachaSystem;
using Godot.Collections;
using InventorySystem;
using QuestSystem;

public partial class GameState : Node
{
	public static GameState state;

	[Export] public int currentPower = 0;
	[Export] public int premiumCurrency = 0;
	[Export] public int upgradeCurrency = 0;
	[Export] private DateTimeManager time;
	[Export] private GameFlags flags;
	[Export] private Inventory playerInventory;
	[Export] private QuestManager quests;

	private Random rng;
	
	[Export] private int money = 10_000;
	[Export] public int salary = 5_000;
	[Export] public GachaCharacter[] allCharacters;
	[Export] public GachaCharacter[] ownedCharacters;

	[Export] public GachaGame game { get; set; }

	private bool frameZero = true;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(state == null)
        {
			state = this;
			rng = new Random(Guid.NewGuid().GetHashCode());
        }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (frameZero)
        {
			frameZero = false;
			if(state == this)
				GodotHelper.MoveNodeToRoot(this);
        }
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
			/*state.hour = 17;
			state.time.minute = 30;*/
		}
	}

	public static bool IsTimeForWork(){
		return state.time.hour == 8;
	}

	/**************
	 * Time Functions
	 * ***********/

	public static int GetDay()
    {
		return state.time.day;
    }

	public static int GetHour()
    {
		return state.time.hour;
    }

	public static int GetMinute()
    {
		return state.time.minute;
    }

	public static void SkipTime(int hours, int minutes)
    {
		state.time.SkipTime(hours, minutes);
    }

	public static void SkipToTime(int hour, int minute)
    {
		state.time.SkipToTime(hour, minute);
    }

	public static void PauseTime()
    {
		state.time.PauseTime();
    }

	public static void UnpauseTime()
    {
		state.time.UnpauseTime();
    }

	/**************
	 * Inventory
	 * *************/

	public static void AddItemToPlayerInventory(ItemResource item, int amount = 1)
    {
		var inventory = state.playerInventory;
		inventory.AddItem(item, amount);
		GD.Print($"Added {amount} {item.itemName} to inventory");
    }

	public static bool RemoveItemFromPlayerInventory(ItemResource item, int amount = 1)
    {
		var inventory = state.playerInventory;
		bool result = inventory.RemoveItem(item, amount);
		return result;
    }

	public static int GetAmountOfItemInPlayerInventory(ItemResource item)
    {
		var inventory = state.playerInventory;
		return inventory.GetItemCount(item);
    }

	/**************
	 * Money
	 * ***********/

	public static void AddMoney(int amount)
    {
		if(amount > 0)
			state.money += amount;
    }

	public static bool TakeMoney(int amount)
    {
		bool isEnough = state.money >= amount;

		if(isEnough)
        {
			state.money -= amount;
        }

		return isEnough;
    }

	public static int GetMoney()
    {
		return state.money;
    }

	/**************
	 * Flags
	 * *************/

	public static bool CheckFlag(string flag)
	{
		return state.flags.CheckFlag(flag);
	}

	public static int GetFlagAmount(string flag)
	{
		return state.flags.GetFlagAmount(flag);
	}

	public static void ResetFlag(string flag)
	{
		state.flags.ResetFlag(flag);
	}

	public static void SetFlag(string flag, int amount)
	{
		state.flags.SetFlag(flag, amount);
	}

	public static void AddFlag(string flag, int amount = 1)
	{
		state.flags.AddFlag(flag, amount);
	}

	public static void RemoveFlag(string flag, int amount = 1)
	{
		state.flags.RemoveFlag(flag, amount);
	}

	/**************
	 * Random
	 * **************/

	/// <summary>
	/// Gets a random number inclusive
	/// </summary>
	/// <param name="min"></param>
	/// <param name="max"></param>
	/// <returns></returns>
	public static int GetRandomNumber(int min, int max)
    {
		int value = state.rng.Next();
		int size = max - min + 1;
		return (value % size) + min;
    }

	/**************
	 * Quests
	 * ************/
	public static void AddQuest(BaseQuest quest)
    {
		var questManager = state.quests;
		if (!questManager.HasQuest(quest))
		{
			questManager.AddQuest(quest);
		}
    }

	public static bool PlayerHasQuest(BaseQuest quest)
    {
		var questManager = state.quests;
		return questManager.HasQuest(quest);
    }

	/*****************
	* Mood-Related Functions
	******************/

	public static void GoToSleep(){
		/*state.hour = 8;
		state.minute = 0;
		state.day++;
		if(state.day % 15 == 0){
			state.money += state.salary;
		}*/
	}

	public static bool TimeForBed(){
		return true;
	}
}
