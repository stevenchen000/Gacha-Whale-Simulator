﻿using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatTurnFinishedNode : CombatStateNode
    {
        [Export] private CombatStateNode victoryCheck;
        [Export] private float delay = 1f;

        protected override void OnStateActivated()
        {
            base.OnStateActivated();
            var character = battle.GetCurrentCharacter();
            character.Status.RunTurnEndEffects();
            character.Status.ReduceStatusTime();
        }

        protected override void RunState(double delta)
        {
            
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;

            //if (timeInState > delay)
            result = victoryCheck;

            return result;
        }

        protected override void OnStateDeactivated()
        {
            
        }
    }
}
