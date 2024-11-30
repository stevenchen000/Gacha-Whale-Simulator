using Godot;
using System;

namespace EventSystem
{
    [GlobalClass]
    public partial class VoidEvent : Resource
    {
        public delegate void EventDelegate();
        private event EventDelegate OnEvent;
        [Export] private bool debug = false;

        public void RaiseEvent()
        {
            if (OnEvent != null)
            {
                OnEvent();
            }
            if (debug) GD.Print($"{ResourcePath.TrimSuffix(".tres")} was called");
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