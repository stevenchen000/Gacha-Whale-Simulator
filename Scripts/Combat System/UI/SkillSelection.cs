using Godot;
using Godot.Collections;
using System;

namespace CombatSystem
{
    public partial class SkillSelection : Node
    {
        [Export] private Array<BattleSkillButton> skillButtons;
        [Export] private BattleSkillButton skillButtonUltimate;


        public void SetupButtons(BattleCharacter character)
        {
            var firstSkill = character.skill;
            SetSkillToButton(firstSkill, 0);
        }

        private void SetSkillToButton(CharacterSkill skill, int index)
        {
            skillButtons[index]?.SetSkill(skill);
        }


        private void ResetButtons()
        {
            
        }
    }
}