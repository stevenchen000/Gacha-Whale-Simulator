using Godot;
using System;
using Godot.Collections;

namespace CombatSystem
{
    [GlobalClass]
    public partial class SkillAnimationElementCreateObjectAtTarget : SkillAnimationElement
    {
        [Export] private PackedScene castObject;
        [Export] private Vector2 offset;
        [Export] private Vector2 scale = new Vector2(1,1);
        private Array<Node2D> tempObjects;

        public override void _StartElement(BattleSkillCastData data)
        {
            GD.Print("Starting instantiation");
            tempObjects = new Array<Node2D>();
            var targets = data.targets;

            foreach(var target in targets)
            {
                //GD.Print($"Instantiating at target: {target.Name}");
                var newObject = (Node2D)Utils.InstantiateCopy(castObject);
                target.AddChild(newObject);
                newObject.Position = offset;
                newObject.Scale = scale;
                //GD.Print($"Object created: {newObject.Name} at {newObject.GlobalPosition}");
                tempObjects.Add(newObject);
            }
        }

        public override bool _RunElement(BattleSkillCastData data)
        {
            //GD.Print("Nothing happening");
            return false;
        }

        public override void _EndElement(BattleSkillCastData data)
        {
            //GD.Print("Animatiion element ended");
            foreach(var obj in tempObjects)
            {
                //obj.QueueFree();
            }
            tempObjects = null;
        }
    }
}