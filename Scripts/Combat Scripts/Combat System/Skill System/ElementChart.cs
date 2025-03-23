using Godot;
using Godot.Collections;
using System;

namespace CombatSystem
{
    public partial class ElementChart : Node
    {
        private static ElementChart instance;

        [Export] private Dictionary<Element, Element> Weaknesses = new Dictionary<Element, Element>();
        [Export] private Dictionary<Element, Element> Resistances = new Dictionary<Element, Element>();


        public override void _Ready()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                QueueFree();
            }
        }


        public static Element GetWeakness(Element element)
        {
            if(instance.Weaknesses.ContainsKey(element))
                return instance.Weaknesses[element];
            else
                return null;
        }

        public static Element GetResistance(Element element)
        {
            if(instance.Resistances.ContainsKey(element))
                return instance.Resistances[element];
            else
                return null;
        }
    }
}