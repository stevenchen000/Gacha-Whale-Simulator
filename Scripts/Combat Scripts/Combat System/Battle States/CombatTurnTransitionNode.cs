using System;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatTurnTransitionNode : CombatStateNode
    {
        [Export] private CombatStateNode animationNode;
        private BattleCharacter character;
        private GridSpace currSpace;

        protected override void OnStateActivated()
        {
            var grid = battle.GetGrid();
            character = battle.GetCurrentCharacter();
            /*var nearestCoords = grid.GetNearestWalkableSpace(character);
            currSpace = grid.GetSpaceFromCoords(nearestCoords);*/
        }

        protected override void RunState(double delta)
        {
            bool arrived = MoveCharacterTowardsSpace();
            if (arrived)
            {
                ChangeState(animationNode);
            }
        }

        protected override void OnStateDeactivated()
        {
            
        }

        private bool MoveCharacterTowardsSpace()
        {
            var spacePos = currSpace.GlobalPosition;
            character.Position = character.Position.Lerp(spacePos, 0.4f);
            var characterPos = character.Position;
            return CharacterIsWithinDistance(characterPos, spacePos, 0.1f);
        }

        private bool CharacterIsWithinDistance(Vector2 pos, Vector2 spacePos, float maxDistance)
        {
            float distance = (pos - spacePos).Length();
            return distance <= maxDistance;
        }
    }
}
