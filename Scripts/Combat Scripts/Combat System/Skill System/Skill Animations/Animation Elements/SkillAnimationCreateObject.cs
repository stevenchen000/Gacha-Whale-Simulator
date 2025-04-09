using Godot;
using System;
using Godot.Collections;
using System.Collections.Generic;

namespace CombatSystem
{
    [GlobalClass]
    public partial class SkillAnimationCreateObject : SkillAnimationElement
    {
        [Export] private PackedScene castObject;
        [Export] private Vector2 offset;
        [Export] private Vector2 scale = new Vector2(1,1);

        [Export] private double delayBetweenTargets = 0.5;

        [Export] private AnimationParent relativeTo;
        [Export] private bool changesWithElement = false;


        public override void AddAnimationContainer(List<SkillAnimationContainer> skillAnimations,
                                                   TurnData turnData)
        {
            if (relativeTo == AnimationParent.Caster)
            {
                var caster = turnData.caster;
                skillAnimations.Add(new SkillAnimationContainer(turnData, this, delay, caster));
            }
            else if (relativeTo == AnimationParent.Target)
            {
                var targets = turnData.targets;
                for(int i = 0; i < targets.Count; i++)
                {
                    var target = targets[i];
                    var newAnim = new SkillAnimationContainer(turnData, this, delay + i * delayBetweenTargets, target);
                    skillAnimations.Add(newAnim);
                }
            }
        }


        public override void StartElement(SkillAnimationContainer container, TurnData data)
        {
            var target = container.Target.PositionFollower;
            var obj = Utils.InstantiateCopy<Node2D>(castObject);
            target.AddChild(obj);
            obj.Position = offset;
            obj.Scale = scale;
            SetupISkillEffect(obj, data);
        }

        public override bool RunElement(SkillAnimationContainer container, TurnData data, TimeHandler time)
        {
            return time.TimeIsUp(duration);
        }

        public override void EndElement(SkillAnimationContainer container, TurnData data)
        {
            
        }


        private void InstantiateAtTarget(Node target)
        {
            var newObject = (Node2D)Utils.InstantiateCopy(castObject);
            target.AddChild(newObject);
            newObject.Position = offset;
            newObject.Scale = scale;
        }

        private void SetupISkillEffect(Node2D obj, TurnData data)
        {
            if (!changesWithElement) return;
            
            if(obj is ISkillEffect)
            {
                var effect = (ISkillEffect)obj;
                effect.SetupForSkill(data);
            }
        }
    }
}