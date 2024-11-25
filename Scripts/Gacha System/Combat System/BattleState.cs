using Godot;
using System;
using GachaSystem;

public class BattleState
{
    public GachaCharacterData player;
    public GachaCharacterData enemy;
    public bool enemyPartyIsDead = false;
    public bool playerPartyIsDead = false;
}
