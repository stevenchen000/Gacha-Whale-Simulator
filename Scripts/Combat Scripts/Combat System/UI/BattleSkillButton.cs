using Godot;
using System;

namespace CombatSystem
{
    public partial class BattleSkillButton : Control
    {
        [Export] private Button button;
        [Export] private Label nameLabel;
        [Export] private Label usesLabel;
        [Export] private TextureRect elementIcon;
        public CharacterSkill skill { get; private set; }
        private BattleManager battle;

        public override void _Ready()
        {
            battle = Utils.FindParentOfType<BattleManager>(this);
            button.Disabled = true;
        }

        /**************
         * Set Skill
         * ************/

        #region Set Skill

        public void SetSkill(CharacterSkill skill)
        {
            this.skill = skill;
            if (skill != null)
            {
                SetupUI(skill);
                button.Disabled = false;
            }
            else
            {
                ResetUI();
                button.Disabled = true;
            }
        }

        private void SetupUI(CharacterSkill skill)
        {
            button.Icon = skill.Icon;
            nameLabel.Text = skill.SkillName;
            usesLabel.Text = skill.Uses.ToString();
            if(skill.SkillElement != null)
                elementIcon.Texture = skill.SkillElement.Texture;
        }

        private void ResetUI()
        {
            button.Icon = null;
            nameLabel.Text = "";
            usesLabel.Text = "";
            elementIcon.Texture = null;
        }

        #endregion

        public void EnableButton()
        {
            button.Disabled = false;
        }

        public void DisableButton()
        {
            button.Disabled= true;
        }


        public void OnClick()
        {            
            var selectedSkill = battle.SelectedSkill;

            if (selectedSkill != skill)
            {
                battle.ResetDirection();
                battle.SetSelectedSkill(skill);
                int numOfDirections = battle.GetNumberOfTargetableDirections();
                if (numOfDirections == 1)
                {//selects direction if there's only one viable direction
                    var directions = battle.GetValidDirections();
                    battle.SelectDirection(directions[0]);
                    battle.ShowConfirmationAndHideSkipButton();
                }
                else if(numOfDirections > 1)
                {//reveals buttons if there's multiple directions
                    battle.RevealDirectionButtons();
                    battle.HideConfirmationAndShowSkipButton();
                }
            }
            else
            {
            }
            //reveal attack on grid
            //select direction if there's only one
            //show direction selector if there's more than one
        }


    }
}