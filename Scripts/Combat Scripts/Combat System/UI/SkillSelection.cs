using Godot;
using Godot.Collections;
using System;

namespace CombatSystem
{
    public partial class SkillSelection : Control
    {
        [Export] private Array<BattleSkillButton> skillButtons;
        [Export] private BattleSkillButton skillButtonUltimate;
        [Export] private AnimationPlayer anim;

        [ExportCategory("Confirmation Buttons")]
        [Export] private Button confirmButton;
        [Export] private Button cancelButton;
        [Export] private Button skipButton;

        [ExportCategory("Show/Hide Colors")]
        [Export] private Color activeColor;
        [Export] private Color hiddenColor;


        private SimpleWeakRef<BattleManager> battle;
        private bool isActive = false;

        public override void _Ready()
        {
            base._Ready();
            var tempBattle = Utils.FindParentOfType<BattleManager>(this);
            battle = new SimpleWeakRef<BattleManager>(tempBattle);
            Position = new Vector2(0, 9999);
        }


        public void SetupButtons(BattleCharacter character)
        {
            var skillManager = character.Skills;
            var skills = skillManager.Skills;

            for (int i = 0; i < skillButtons.Count; i++)
            {
                if(skills.Count > i)
                {
                    SetSkillToButton(skills[i], i);
                }
                else
                {
                    SetSkillToButton(null, i);
                }
            }
        }

        private void SetSkillToButton(SkillContainer skill, int index)
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
                button.UpdateButtonState();
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

        public void HideUI()
        {
            if (isActive)
            {
                anim.Play("hide_ui");
                isActive = false;
            }
        }

        public void ShowUI()
        {
            if (!isActive)
            {
                anim.Play("show_ui");
                isActive = true;
            }
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