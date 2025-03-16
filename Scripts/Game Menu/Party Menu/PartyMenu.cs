using Godot;
using Godot.Collections;
using CombatSystem;
using System;
using PartyMenuSystem;

public partial class PartyMenu : GameMenu
{
    [Export] private CurrentPartySubmenu partyMenu;
    [Export] private SelectPartyMemberSubmenu memberMenu;

    private int partyIndex = 0;
    public PartySetup CurrentParty { get; private set; }
    public int SelectedCharacterIndex { get; private set; }


    public override void _Init()
    {
        base._Init();

        partyIndex = GetSavedPartyIndex();
        CurrentParty = GameState.GetParty(partyIndex);
    }

    public override void _Notification(int what)
    {
        if(what == NotificationPredelete)
        {
            SavePartyIndex();
            GameState.SetCurrentParty(CurrentParty);
        }
    }


    private string partyIndexFlag = "current_party_index";

    private void SavePartyIndex()
    {
        GameState.SetFlag(partyIndexFlag, partyIndex);
    }

    private int GetSavedPartyIndex()
    {
        return GameState.GetFlagAmount(partyIndexFlag);
    }


    /********************
     * Menus
     * *****************/

    public void OpenPartyEditor()
    {
        memberMenu.Disable();
        partyMenu.Enable();
    }

    public void OpenCharacterSelector()
    {
        partyMenu.Disable();
        memberMenu.Enable();
    }

    /******************
     * Party Member Functions
     * ***************/

    public void SetPartyMemberIndex(int index)
    {
        SelectedCharacterIndex = index;
    }

    public void SetCurrentMember(CharacterData data)
    {
        CurrentParty.SetMember(SelectedCharacterIndex, data);
    }

    public CharacterData GetCurrentCharacter()
    {
        return CurrentParty.Party[SelectedCharacterIndex];
    }


    /**********************
     * Party Functions
     * *********************/

    public PartySetup SetNextParty()
    {
        int totalParties = GameState.GetNumberOfParties();
        partyIndex = (partyIndex + 1) % totalParties;
        CurrentParty = GameState.GetParty(partyIndex);
        return CurrentParty;
    }

    public PartySetup SetPreviousParty()
    {
        int totalParties = GameState.GetNumberOfParties();
        partyIndex--;
        if (partyIndex < 0)
            partyIndex = totalParties - 1;

        CurrentParty = GameState.GetParty(partyIndex);

        return CurrentParty;
    }

    public void SetCharacterIndex(int index)
    {
        SelectedCharacterIndex = index;
    }

}
