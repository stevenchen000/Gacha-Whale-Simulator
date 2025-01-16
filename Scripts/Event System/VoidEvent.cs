using Godot;
using System;
using System.IO;

namespace EventSystem
{
    [GlobalClass]
    public partial class VoidEvent : Resource
    {
        public delegate void EventDelegate();
        private event EventDelegate OnEvent;
        [Export] private string description;
        [Export] private bool debug = false;

        public void RaiseEvent(Node node)
        {
            if (debug)
            {
                string filename = Path.GetFileNameWithoutExtension(ResourcePath);
                Utils.Print(node, $"{filename} was called");
            }
            if (OnEvent != null)
            {
                OnEvent();
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