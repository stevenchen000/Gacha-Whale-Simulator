using Godot;
using System;
using Godot.Collections;
using System.Collections.Generic;

namespace CombatSystem {
    [GlobalClass]
    public partial class CharacterSkill : Resource
    {
        [Export] public Texture2D Icon { get; private set; }
        [Export] public string SkillName { get; private set; }
        [Export] public SkillAnimation animation { get; private set; }
        public double Duration 
        { 
            get 
            {
                if (animation == null)
                    return 0;
                else
                    return animation.Duration; 
            } 
        }


        [ExportCategory("Damage & Effects")]
        [Export] public bool InfiniteUses { get; private set; } = false;
        [Export] public int Uses { get; private set; } = 3;
        [Export] public Element SkillElement { get; private set; }
        [Export] public Array<SkillEffect> effects { get; private set; }
        [Export] public int potency { get; private set; } = 100;
        [Export] public GridShape AttackArea { get; private set; }
        [Export] public SkillDirection direction { get; private set; }
        [Export] public TargetType targetType { get; private set; }



        [ExportCategory("Charge Skill Data")]
        [Export] public bool IsChargeSkill { get; private set; } = false;
        [Export] public int ChargeAmount { get; private set; } = 100;


        public void SetupSkillAnimation(List<SkillAnimationContainer> animations,
                                        TurnData turnData)
        {
            if(animation != null)
                animation.AddAnimationsToList(animations, turnData);
        }

        public void SetupSkillEffects(List<SkillAnimationContainer> animations, TurnData turnData)
        {
            foreach(var effect in effects)
            {
                
            }
        }



        public bool RunEffects(TurnData data, TimeHandler time, double delta)
        {
            return true;
        }

        public bool IsValidTarget(BattleCharacter caster, BattleCharacter target)
        {
            bool result = false;

            switch (targetType)
            {
                case TargetType.Enemy:
                    result = caster.IsEnemy(target);
                    break;
                case TargetType.Ally:
                    result = !caster.IsEnemy(target);
                    break;
            }

            return result;
        }

        public TargetingData GetTargetingData(BattleGrid grid, BattleCharacter caster, SkillContainer skill, Vector2I position)
        {
            var data = AttackArea.GetTargetsFromPosition(grid, caster, skill, position);

            return data;
        }


        

        private bool HasTargetsInList(BattleManager battle, BattleCharacter caster, Array<GridSpace> spaces)
        {
            bool result = false;

            foreach (var space in spaces)
            {
                var target = space.CharacterOnSpace;
                if (target != null)
                {
                    result = IsValidTarget(caster, target);
                    if (result)
                        break;
                }
            }

            return result;
        }


    }
}