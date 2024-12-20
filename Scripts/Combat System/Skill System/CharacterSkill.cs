using Godot;
using System;
using Godot.Collections;

namespace CombatSystem {
    [GlobalClass]
    public partial class CharacterSkill : Resource
    {
        [Export] public SkillAnimation animation;
        [Export] public Array<SkillEffect> effects;
        [Export] public float duration = 2f;
        [Export] public int potency = 100;
        [Export] public GridShape attackArea;

        public CharacterSkill GetDuplicate()
        {
            var newSkill = new CharacterSkill();
            newSkill.animation = animation.GetDuplicate();
            //duplicate skill effects
            newSkill.duration = duration;
            newSkill.potency = potency;
            newSkill.attackArea = attackArea;

            return newSkill;
        }

        public bool PlayAnimation(BattleSkillCastData data, double delta)
        {
            return animation.PlayAnimation(data, delta);
        }

        public bool RunEffects(BattleSkillCastData data, double delta)
        {
            return true;
        }
    }
}