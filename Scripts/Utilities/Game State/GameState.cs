using Godot;
using System;
using GachaSystem;
using Godot.Collections;
using InventorySystem;
using QuestSystem;
using CombatSystem;
using TimeSystem;
using StaminaSystem;

public partial class GameState : Node
{
	private static GameState state = null;

	[Export] private PlayerRankManager rank;
	[Export] private GameTimeManager time;
	[Export] private GameFlags flags;
	[Export] private GameStringFlags stringFlags;
	[Export] private Inventory playerInventory;
	[Export] private QuestManager quests;
    [Export] public GachaGame game { get; set; }
	[Export] public StaminaManager stamina { get; set; }

    private Random rng;

	[Export] private int money = 10_000;
	[Export] public int salary = 5_000;

	[ExportCategory("Combat Data")]
	[Export] private StageData Stage { get; set; }
	[Export] private PartySetup CurrentParty { get; set; }




	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(state == null)
        {
			state = this;
			rng = new Random(Guid.NewGuid().GetHashCode());
			CallDeferred(MethodName.LoadData);
        }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        
	}

	public override void _Notification(int what)
	{
		if (what == NotificationApplicationPaused ||
			what == NotificationWMCloseRequest)
		{
			Utils.Print(this, "Quitting game...");
			SaveData();
		}
    }


    public static GachaGame GetGame()
	{
		return state.game;
	}

	/******************
	 * Combat Functions
	 * ****************/


	public static StageData GetStage()
	{
		return state.Stage;
	}

	public static void SetCurrentStage(StageData newStage)
	{
		state.Stage = newStage;
	}

    /***********************
	 * Party Functions
	 * ******************/

    #region Party Functions

    public static PartySetup GetParty(int id)
    {
        return state.game.GetParty(id);
    }

	public static Array<PartySetup> GetAllParties()
	{
		return state.game.PlayerParties;
	}

    public static int GetNumberOfParties()
    {
        return state.game.GetNumberOfParties();
    }

    public static PartySetup GetCurrentParty()
    {
        return state.CurrentParty;
    }

    public static void SetCurrentParty(PartySetup newParty)
    {
        state.CurrentParty = newParty;
    }

	#endregion

	/**************************
	 * Character Functions
	 * **********************/

	#region Character Functions
	public static void AddCharacterToAccount(GameCharacter character)
    {
        var game = state.game;
        game.AddCharacterToAccount(character);
    }

    public static int GetNumberOfCharacters()
    {
        return state.game.GetNumberOfCharacters();
    }

    public static Array<CharacterData> GetAllOwnedCharacters()
    {
        return state.game.GetAllCharacterData();
    }

    public static GameCharacter GetCharacterByID(int id)
    {
        return state.game.GetCharacterByID(id);
    }

    public static CharacterData GetOwnedCharacterByID(int id)
    {
        return state.game.GetOwnedCharacterByID(id);
    }

	#endregion

	/****************
	* Gacha Functions
	*****************/

	#region Gacha Functions

	public static void AddPremiumCurrency(int amount){
		var game = state.game;
		game.AddPremiumCurrency(amount);
	}

	public static bool UsePremiumCurrency(int cost){
		var game = state.game;
		return game.UsePremiumCurrency(cost);
	}

	public static bool HasEnoughPremiumCurrency(int cost){
		return state.game.HasEnoughPremiumCurrency(cost);
	}

	public static void AddUpgradeCurrency(int amount){
		//state.upgradeCurrency += amount;
	}

	public static bool UseUpgradeCurrency(int cost){
		bool hasEnough = HasEnoughUpgradeCurrency(cost);

		if(hasEnough){
			//state.upgradeCurrency -= cost;
		}

		return hasEnough;
	}

	public static bool HasEnoughUpgradeCurrency(int cost){
		return true;//state.upgradeCurrency >= cost;
	}

	#endregion

	/*****************
	 * Rank Functions
	 * **************/

	public static PlayerRankManager GetRankManager()
	{
		return state.rank;
	}



	/**************
	 * Time Functions
	 * ***********/


	public static void ProgressTime()
	{
		state.time.ProgressTime();
	}

	public static GameTimeManager GetTimeManager()
	{
		return state.time;
	}

	public static TimeOfDay GetTimeOfDay()
	{
		return state.time.Time;
	}

	public static int GetDay()
    {
		return state.time.Day;
    }

	public static int GetWeek()
	{
		return state.time.Week;
	}

	public static int GetMonth()
	{
		return state.time.Month;
	}

	public static int GetYear()
	{
		return state.time.Year;
	}

	/*****************
	 * Stamina
	 * *************/

	public static int GetCurrentStamina()
	{
		return state.stamina.CurrentStamina;
	}

	public static int GetMaxStamina()
	{
		return state.stamina.MaxStamina;
	}

	public static void FillStamina()
	{
		state.stamina.FillStamina();
	}

	public static void UseStamina(int amount)
	{
		state.stamina.UseStamina(amount);
	}

	public static bool HasEnoughStamina(int amount)
	{
		return state.stamina.HasEnoughStamina(amount);
	}

	/**************
	 * Inventory
	 * *************/

	public static void AddItemToPlayerInventory(ItemResource item, int amount = 1)
    {
		var inventory = state.playerInventory;
		inventory.AddItem(item, amount);
		Utils.Print(state, $"Added {amount} {item.ItemName} to inventory");
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

	public static bool HasEnoughOfItem(ItemResource item, int amount)
	{
		var inventory = state.playerInventory;
		return inventory.HasEnough(item, amount);
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
	 * Num/Bool Flags
	 * *************/

	public static bool GetFlag(string flag)
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

	/*****************
	 * String Flags
	 * ***************/

	public static string GetStringFlag(string flag)
	{
		return state.stringFlags.GetFlag(flag);
	}

	public static void SetStringFlag(string flag, string text)
	{
		state.stringFlags.SetFlag(flag, text);
	}

	public static void RemoveStringFlag(string flag)
	{
		state.stringFlags.RemoveFlag(flag);
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

	public static double GetRandomDouble()
	{
		return state.rng.NextDouble();
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

	public static bool PlayerCompletedQuest(BaseQuest quest)
    {
		var questManager = state.quests;
		return questManager.QuestFinished(quest);
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



	/**************
	 * Save / Load
	 * ************/

	public static void SaveData()
	{
		state.SaveAllChildren(state);

		state.flags.Save();
		state.stringFlags.Save();
	}

	private void LoadData()
	{
		flags.Load();
		stringFlags.Load();

		LoadAllSavables();
	}

	private void LoadAllSavables()
	{
		var children = GetChildren();
		foreach(var child in children)
		{
			if(child is ISavable)
			{
				var script = child as ISavable;
				script.Load();
			}
		}
	}

	private void SaveAllChildren(Node node)
	{
        var children = node.GetChildren();
        foreach (var child in children)
        {
            _SaveSavableObject(child);
			SaveAllChildren(child);
        }
    }

	private void _SaveSavableObject(Node node)
	{
        if (node is ISavable)
        {
            var script = node as ISavable;
            script.Save();
        }
    }
}
