using Godot;
using System;
using GachaSystem;

public partial class EventManager : Node
{
    public delegate void GachaCharacterPulled(GachaCharacter pulledCharacter);
    public event GachaCharacterPulled OnGachaCharacterPulled;
    public void RaiseCharacterPulledEvent(GachaCharacter character)
    {
        if(OnGachaCharacterPulled != null)
            OnGachaCharacterPulled(character);
    }


    public delegate void VoidEvent();
    public event VoidEvent OnCharacterListUpdated;
    public void RaiseCharacterListUpdatedEvent()
    {
        if(OnCharacterListUpdated != null)
            OnCharacterListUpdated();
    }
}
