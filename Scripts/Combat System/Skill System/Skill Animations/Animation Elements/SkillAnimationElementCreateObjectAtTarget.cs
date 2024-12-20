using Godot;
using System;
using Godot.Collections;

namespace CombatSystem
{
    [GlobalClass]
    public partial class SkillAnimationElementCreateObjectAtTarget : SkillAnimationElement
    {
        [Export] private PackedScene castObject;
        private Array<Node2D> tempObjects;

        public override void _StartElement(BattleSkillCastData data)
        {
            tempObjects = new Array<Node2D>();
            var targets = data.targets;

            foreach(var target in targets)
            {
                var newObject = (Node2D)GodotHelper.InstantiateCopy(castObject);
                newObject.GlobalPosition = target.GlobalPosition;
                tempObjects.Add(newObject);
            }
        }

        public override bool _RunElement(BattleSkillCastData data)
        {
            return false;
        }

        public override void _EndElement(BattleSkillCastData data)
        {
            foreach(var obj in tempObjects)
            {
                obj.QueueFree();
            }
            tempObjects = null;
        }
    }
}