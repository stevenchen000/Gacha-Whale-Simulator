using Godot;
using System;
using Godot.Collections;
using System.Collections.Generic;

namespace CombatSystem
{
    [Tool]
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
                                                   TurnData turnData, 
                                                   SkillCastData skillCast)
        {
            Utils.Print(this, "Adding animation...");
            if (relativeTo == AnimationParent.Caster)
            {
                var caster = skillCast.Caster;
                skillAnimations.Add(new SkillAnimationContainer(turnData, skillCast, this, delay, caster));
            }
            else if (relativeTo == AnimationParent.Target)
            {
                var targets = skillCast.Targets;
                for(int i = 0; i < targets.Count; i++)
                {
                    var target = targets[i];
                    var newAnim = new SkillAnimationContainer(turnData, skillCast, this, delay + i * delayBetweenTargets, target);
                    skillAnimations.Add(newAnim);
                }
            }
        }


        public override void StartElement(SkillAnimationContainer container, TurnData data,
                                                   SkillCastData skillCast)
        {
            var target = container.Target.PositionFollower;
            var obj = Utils.InstantiateCopy<Node2D>(castObject);
            target.AddChild(obj);
            obj.Position = offset;
            obj.Scale = scale;
            SetupISkillEffect(obj, skillCast);
        }

        public override bool RunElement(SkillAnimationContainer container, TurnData data,
                                                   SkillCastData skillCast, TimeHandler time)
        {
            return time.TimeIsUp(duration);
        }

        public override void EndElement(SkillAnimationContainer container, TurnData data,
                                                   SkillCastData skillCast)
        {
            
        }


        private void InstantiateAtTarget(Node target)
        {
            Utils.Print(this, "Instantiating object");
            var newObject = (Node2D)Utils.InstantiateCopy(castObject);
            target.AddChild(newObject);
            newObject.Position = offset;
            newObject.Scale = scale;
        }

        private void SetupISkillEffect(Node2D obj, SkillCastData data)
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