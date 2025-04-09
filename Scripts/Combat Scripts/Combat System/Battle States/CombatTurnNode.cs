using System;
using System.ComponentModel.Design;
using Godot;
using StateSystem;

namespace CombatSystem
{
    public partial class CombatTurnNode : CombatStateNode
    {
        private BattleCharacter character;
        private BattleGrid grid;
        [Export] private CombatStateNode animationNode;
        [Export] private CombatStateNode turnFinishedNode;
        [Export] private CombatStateNode checkVictoryNode;
        private Vector2I prevCoords;
        private Vector2I currCoords;

        private TimeHandler timer;
        private bool turnFinished = false;

        private CombatActionData enemyAction;

        protected override void OnStateActivated()
        {
            battle.StartTurn();
            grid = battle.GetGrid();
            character = battle.GetCurrentCharacter();

            if (IsEnemyControlled(character))
            {
                timer = new TimeHandler();
                enemyAction = character.AI.CalculateAction(battle);
            }
            else
            {
                battle.ShowSkillUI();
            }

            RunCharacterTurnStarts();
            character.PlayAnimation("idle_turn");
        }

        private void RunCharacterTurnStarts()
        {
            var fighters = battle.State.GetAllLivingCharacters();
            foreach(var fighter in fighters)
            {
                fighter.AnyCharacterTurnStart(battle.State);
            }
        }



        /******************
         * Run State
         * **************/

        protected override void RunState(double delta)
        {
            if (IsEnemyControlled(character))
            {
                if (enemyAction != null)
                {
                    timer.Tick(delta);
                    if (timer.TimeIsBetween(0.5))
                    {
                        var coords = enemyAction.CoordsToGo;
                        battle.OccupySpace(coords, character);
                    }
                    else if (timer.TimeIsBetween(1))
                    {
                        var skill = enemyAction.Skill;

                        battle.SetSelectedSkill(skill);
                    }
                    else if (timer.TimeIsBetween(1.5))
                    {
                        var selection = enemyAction.Selection;
                        battle.SetTargetSelection(selection);
                    }else if (timer.TimeIsBetween(1.75))
                    {
                        battle.ConfirmAction();
                    }
                }
                else
                {
                    Utils.Print(this, "Failed to get action");
                    battle.SkipTurn();
                }
            }
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
                    result = turnFinishedNode;
                }
            }

            return result;
        }

        protected override void OnStateDeactivated()
        {
            grid.SetAllSpacesToDefault();
            //grid.OccupySpace(character);
            turnFinished = false;

            if(!IsEnemyControlled(character))
                battle.HideSkillUI();

            character.PlayAnimation("idle");
        }

        /*****************
         * Helpers
         * ***************/


        private bool IsEnemyControlled(BattleCharacter character)
        {
            return character.Party != battle.State.PlayerParty;
        }
    }
}
