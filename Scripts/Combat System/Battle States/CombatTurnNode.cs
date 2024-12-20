﻿using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatTurnNode : CombatStateNode
    {
        private BattleCharacter character;
        private BattleGrid grid;
        [Export] private CombatStateNode transitionNode;

        protected override void OnStateActivated()
        {
            character = battle.GetCurrentCharacter();
            grid = battle.GetGrid();
            grid.UpdateAllWalkableAreas(character);
            character.StartCharacterTurn(battle, grid);
        }

        protected override void RunState(double delta)
        {
            bool turnFinished = character.ControlCharacter(delta, battle, grid);
            if (turnFinished)
            {
                character.EndTurn(delta, battle, grid);
                SetupAction();
                ChangeState(transitionNode);
            }
        }

        protected override void OnStateDeactivated()
        {
            grid.SetAllSpacesToDefault();
        }

        /*****************
         * Helpers
         * ***************/

        private void SetupAction()
        {
            var skill = character.currSkill;
            var targets = character.targets;
            battle.SelectAction(character, targets, skill);
        }
    }
}
