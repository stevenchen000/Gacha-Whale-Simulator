using Godot;
using System;

namespace CombatSystem
{
    public partial class BattleSkillButton : Button
    {
        public CharacterSkill skill { get; private set; }
        private BattleManager battle;

        public override void _Ready()
        {
            battle = Utils.FindParentOfType<BattleManager>(this);
        }

        public void SetSkill(CharacterSkill skill)
        {
            this.skill = skill;
            if (skill != null)
            {
                Icon = skill.Icon;
            }
            else
            {
                Icon = null;
            }
        }

        public void OnClick()
        {
            Utils.Print(this, skill.ResourcePath);
            if (skill != null)
            {
                var selectedSkill = battle.SelectedSkill;
                if (skill != selectedSkill)
                {
                    battle.SetSelectedSkill(skill);
                }
                else
                {
                    battle.SetSelectedSkill(null);
                }
            }
        }
    }
}