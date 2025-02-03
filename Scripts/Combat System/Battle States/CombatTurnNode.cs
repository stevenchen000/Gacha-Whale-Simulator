using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatTurnNode : CombatStateNode
    {
        private BattleCharacter character;
        private BattleGrid grid;
        [Export] private CombatStateNode animationNode;
        [Export] private CombatStateNode checkVictoryNode;
        private Vector2I prevCoords;
        private Vector2I currCoords;

        private bool turnFinished = false;

        protected override void OnStateActivated()
        {
            Utils.Print(this, "Turn started");
            Utils.Print(this, battle);
            battle.StartTurn();
            grid = battle.GetGrid();
            character = battle.GetCurrentCharacter();
        }

        protected override void RunState(double delta)
        {
            /*bool turnFinished = character.ControlCharacter(delta, battle, grid);
            prevCoords = currCoords;
            currCoords = grid.GetNearestWalkableSpace(character);
            grid.SetSpaceState(prevCoords, GridState.ALLY_MOVEABLE);
            grid.SetSpaceState(currCoords, GridState.ALLY_STANDING);
            if (turnFinished)
            {
                SetupAction();
            }*/
        }

        protected override StateNode CheckStateChange()
        {
            StateNode result = null;

            if (battle.TurnConfirmed)
            {
                if (battle.SelectedSkill != null)
                    result = animationNode;
                else
                {
                    battle.EndTurn();
                    result = this;
                }
            }

            return result;
        }

        protected override void OnStateDeactivated()
        {
            grid.SetAllSpacesToDefault();
            //grid.OccupySpace(character);
            turnFinished = false;
        }

        /*****************
         * Helpers
         * ***************/

        private void SetupAction()
        {
            var skill = battle.SelectedSkill;
            var targets = character.targets;
            battle.SelectAction(character, targets, skill);
        }
    }
}
