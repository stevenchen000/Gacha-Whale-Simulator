using Godot;
using System;
using Godot.Collections;

namespace CombatSystem
{
    public partial class SkillCaster : Node
    {
        private BattleSkillCastData data;

        public override void _Ready()
        {
            
        }

        public override void _Process(double delta)
        {
            
        }

        public bool RunSkill()
        {
            
            return false;
        }

        public void CastSkill(BattleCharacter caster, CharacterSkill skill,
                              Array<BattleCharacter> targets)
        {
            data = new BattleSkillCastData(caster, targets, skill);
        }

    }
}