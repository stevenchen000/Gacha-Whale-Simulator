using Godot;
using System.IO;

namespace EventSystem{ 
    public partial class GameEvent<T> : Resource
    {
        public delegate void EventDelegate(T character);
        private event EventDelegate OnEvent;
        [Export] protected string description;
        [Export] protected bool debug = false;

        public void RaiseEvent(Node node, T parameter)
        {
            if (debug)
            {
                string filename = Path.GetFileNameWithoutExtension(ResourcePath);
                Utils.Print(node, $"{filename} was called");
            }
            if (OnEvent != null)
            {
                OnEvent(parameter);
            }
        }

        public void SubscribeEvent(EventDelegate func)
        {
            OnEvent += func;
        }

        public void UnsubscribeEvent(EventDelegate func)
        {
            OnEvent -= func;
        }
    }
}