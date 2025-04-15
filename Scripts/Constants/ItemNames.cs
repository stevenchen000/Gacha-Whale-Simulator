using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystem
{
    public partial class ItemNames : Node
    {
        private static ItemNames instance;

        [Export] private ItemResource _spiritKeys;
        public static ItemResource SpiritKeys { get { return instance._spiritKeys; } }
        

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
    }
}
