using Godot;
using Godot.Collections;
using System;

namespace CombatSystem
{
    public partial class SkillSelection : Node
    {
        [Export] private Array<BattleSkillButton> skillButtons;
        [Export] private BattleSkillButton skillButtonUltimate;

        [ExportCategory("Confirmation Buttons")]
        [Export] private Button confirmButton;
        [Export] private Button cancelButton;
        [Export] private Button skipButton;


        private SimpleWeakRef<BattleManager> battle;

        public override void _Ready()
        {
            base._Ready();
            var tempBattle = Utils.FindParentOfType<BattleManager>(this);
            battle = new SimpleWeakRef<BattleManager>(tempBattle);
        }


        public void SetupButtons(BattleCharacter character)
        {
            var skills = character.skills;
            SetSkillToButton(skills[0], 0);
            SetSkillToButton(skills[1], 1);
            SetSkillToButton(skills[2], 2);
            SetSkillToButton(skills[3], 3);
        }

        private void SetSkillToButton(CharacterSkill skill, int index)
        {
            skillButtons[index]?.SetSkill(skill);
        }


        private void ResetButtons()
        {
            
        }

        public void UpdateIfButtonsShouldBeActive(BattleManager battle, BattleGrid grid, BattleCharacter caster)
        {
            foreach (var button in skillButtons)
            {
                var skill = button.skill;
                bool usable = false;

                if(skill != null) usable = skill.HasTargetInRange(battle, grid, caster);

                if (usable) button.EnableButton();
                else button.DisableButton();
                    
            }
        }

        /*******************
         * Confirmation Buttons
         * *****************/

        public void ShowSkipButton()
        {
            EnableButton(skipButton);
        }

        public void HideSkipButton()
        {
            DisableButton(skipButton);
        }

        public void ShowConfirmationButtons()
        {
            EnableButton(confirmButton);
            EnableButton(cancelButton);
        }

        public void HideConfirmationButtons()
        {
            DisableButton(confirmButton);
            DisableButton(cancelButton);
        }

        private void EnableButton(Button button)
        {
            button.Disabled = false;
            button.Visible = true;
            button.MouseFilter = Control.MouseFilterEnum.Stop;
        }

        private void DisableButton(Button button)
        {
            button.Disabled = true;
            button.Visible = false;
            button.MouseFilter = Control.MouseFilterEnum.Ignore;
        }

        /************
         * Button events
         * ***********/


        public void ConfirmAction()
        {
            battle.Value.ConfirmAction();
        }

        public void CancelAction()
        {
            battle.Value.CancelSelectedSkill();
        }
    }
}