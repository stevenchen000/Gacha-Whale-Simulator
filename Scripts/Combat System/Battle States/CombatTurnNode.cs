using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatTurnNode : CombatStateNode
    {
        private BattleCharacter character;
        private BattleGrid grid;
        [Export] private CombatStateNode transitionNode;
        private Vector2I prevCoords;
        private Vector2I currCoords;

        protected override void OnStateActivated()
        {
            character = battle.GetCurrentCharacter();
            grid = battle.GetGrid();
            grid.UpdateAllWalkableAreas(battle, character);
            character.StartCharacterTurn(battle, grid);
            character.EnableCollider();
            prevCoords = grid.GetNearestSpaceToCharacter(character);
            currCoords = prevCoords;
        }

        protected override void RunState(double delta)
        {
            bool turnFinished = character.ControlCharacter(delta, battle, grid);
            prevCoords = currCoords;
            currCoords = grid.GetNearestWalkableSpace(character);
            grid.SetSpaceState(prevCoords, GridState.ALLY_MOVEABLE);
            grid.SetSpaceState(currCoords, GridState.ALLY_STANDING);
            if (turnFinished)
            {
                //character.EndTurn(delta, battle, grid);
                SetupAction();
                ChangeState(transitionNode);
            }
        }

        protected override void OnStateDeactivated()
        {
            grid.SetAllSpacesToDefault();
            grid.OccupySpace(character);
            character.DisableCollider();
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
