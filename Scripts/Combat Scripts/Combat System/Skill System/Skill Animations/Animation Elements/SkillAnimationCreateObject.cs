using Godot;
using System;
using Godot.Collections;

namespace CombatSystem
{
    [GlobalClass]
    public partial class SkillAnimationCreateObject : SkillAnimationElement
    {
        [Export] private PackedScene castObject;
        [Export] private Vector2 offset;
        [Export] private Vector2 scale = new Vector2(1,1);
        private Array<Node2D> tempObjects;

        public override void _StartElement(TurnData data, TimeHandler time)
        {
            Utils.Print(this, "Starting instantiation");
            tempObjects = new Array<Node2D>();
            var targets = data.targets;

            foreach(var target in targets)
            {
                var newObject = (Node2D)Utils.InstantiateCopy(castObject);
                target.AddChild(newObject);
                newObject.Position = offset;
                newObject.Scale = scale;
                tempObjects.Add(newObject);
            }
        }

        public override bool _RunElement(TurnData data, TimeHandler time)
        {
            return false;
        }

        public override void _EndElement(TurnData data, TimeHandler time)
        {
            foreach(var obj in tempObjects)
            {
                //obj.QueueFree();
            }
            tempObjects = null;
        }
    }
}