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
        public SkillContainer Skill { get; private set; }
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

        public void SetSkill(SkillContainer skill)
        {
            Skill = skill;
            if (skill != null)
            {
                if (HasUsesLeft())
                {
                    SetupUI(skill);
                    button.Disabled = false;
                }
                else
                {
                    SetupUI(skill);
                    button.Disabled = true;
                }
            }
            else
            {
                ResetUI();
                button.Disabled = true;
            }
        }

        private void SetupUI(SkillContainer skill)
        {
            var baseSkill = skill.Skill;

            button.Icon = baseSkill.Icon;

            nameLabel.Text = baseSkill.SkillName;

            SetupUses(skill);

            if(baseSkill.SkillElement != null)
                elementIcon.Texture = baseSkill.SkillElement.Texture;
        }

        private void SetupUses(SkillContainer skill)
        {
            if (!skill.InfiniteUses)
            {
                usesLabel.Text = skill.RemainingUses.ToString();
                usesLabel.Visible = true;
            }
            else
            {
                usesLabel.Visible = false;
            }
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

            if (selectedSkill != Skill)
            {
                battle.ResetDirection();
                battle.SetSelectedSkill(Skill);
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

        private bool HasUsesLeft()
        {
            return Skill.RemainingUses > 0 || Skill.InfiniteUses;
        }


    }
}