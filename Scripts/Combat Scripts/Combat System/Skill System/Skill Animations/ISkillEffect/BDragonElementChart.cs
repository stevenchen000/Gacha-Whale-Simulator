using Godot;
using Godot.Collections;

namespace CombatSystem
{
    public partial class BDragonElementChart : Node
    {
        private static BDragonElementChart _instance;
        [Export] private Dictionary<Element, int> elementChart;


        public override void _Ready()
        {
            if(_instance == null)
            {
                _instance = this;
            }
            else
            {
                QueueFree();
            }
        }

        public static int GetIntFromElement(Element element)
        {
            if (_instance.elementChart.ContainsKey(element))
                return _instance.elementChart[element];
            else
                return -1;
        }
    }
}
