using Godot;
using System;
using Godot.Collections;

namespace CombatSystem {
    [GlobalClass]
    public partial class CharacterSkill : Resource
    {
        [Export] public Texture2D Icon { get; private set; }
        [Export] public SkillAnimation animation { get; private set; }
        [Export] public Array<SkillEffect> effects { get; private set; }
        [Export] public float duration { get; private set; } = 2f;
        [Export] public int potency { get; private set; } = 100;
        [Export] public GridShape attackArea { get; private set; }
        [Export] public SkillDirection direction { get; private set; }

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

        public bool PlayAnimation(TurnData data, TimeHandler time, double delta)
        {
            return animation.PlayAnimation(data, time, delta);
        }

        public bool RunEffects(TurnData data, double delta)
        {
            return true;
        }
    }
}