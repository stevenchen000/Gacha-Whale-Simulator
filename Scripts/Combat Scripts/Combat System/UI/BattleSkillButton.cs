using Godot;
using System;

namespace CombatSystem
{
    public partial class BattleSkillButton : Control
    {
        [Export] private TextureProgressBar progress;
        [Export] private TextureProgressBar progressGlow;
        [Export] private Button button;
        [Export] private TextureRect skillIcon;
        [Export] private TextureRect skillBackground;
        [Export] private Label nameLabel;
        [Export] private Label usesLabel;
        [Export] private TextureRect elementIcon;
        public SkillContainer Skill { get; private set; }
        private BattleManager battle;

        [ExportCategory("Button Enabled/Disabled Colors")]
        [Export] private Color enabledColor;
        [Export] private Color disabledColor;



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
            SetupUI(Skill);
            UpdateButtonState();
        }

        public void UpdateButtonState()
        {
            if (Skill != null && HasUsesLeft() && HasTargetsInRange())
            {
                EnableButton();
            }
            else
            {
                DisableButton();
            }
        }



        private void SetupUI(SkillContainer skill)
        {
            if (skill != null)
            {
                var baseSkill = skill.Skill;

                skillIcon.Texture = baseSkill.Icon;

                nameLabel.Text = baseSkill.SkillName;

                SetupProgressBar(skill);
                SetupSkillBackground();

                SetupUses(skill);

                var element = baseSkill.SkillElement;
                if (baseSkill.SkillElement != null)
                {
                    skillBackground.SelfModulate = element.ElementColor;
                }
                Visible = true;
            }
            else
            {
                ResetUI();
            }
        }

        private void SetupProgressBar(SkillContainer skill)
        {
            if (skill.IsChargeSkill)
            {
                float percent = skill.ChargePercent;
                progress.Value = percent;
                progressGlow.Value = percent;
                progress.Visible = true;
            }
            else
            {
                progress.Visible = false;
            }
        }

        private void SetupSkillBackground()
        {
            var character = battle.GetCurrentCharacter();
            var element = character.CharacterElement;

            if (element != null)
                skillBackground.SelfModulate = element.ElementColor;
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
            nameLabel.Text = "";
            usesLabel.Text = "";
            Visible = false;
        }

        #endregion

        public void EnableButton()
        {
            button.Disabled = false;
            skillIcon.SelfModulate = enabledColor;
        }

        public void DisableButton()
        {
            button.Disabled= true;
            skillIcon.SelfModulate = disabledColor;
        }


        public void OnClick()
        {            
            var selectedSkill = battle.SelectedSkill;

            if (selectedSkill != Skill)
            {
                battle.ResetDirection();
                battle.SetSelectedSkill(Skill);
            }
            else
            {
            }
            //reveal attack on grid
            //select direction if there's only one
            //show direction selector if there's more than one
        }



        /***************
         * Helpers
         * **************/

        private bool HasTargetsInRange()
        {
            if (Skill == null) return false;
            var grid = battle.Grid;
            var caster = battle.GetCurrentCharacter();
            var position = caster.currPosition;
            return Skill.HasTargetInRange(grid, caster, position);
        }


        private bool HasUsesLeft()
        {
            return Skill.RemainingUses > 0 || Skill.InfiniteUses;
        }


    }
}