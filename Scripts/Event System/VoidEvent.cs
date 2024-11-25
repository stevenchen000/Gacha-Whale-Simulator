using Godot;
using System;

namespace EventSystem
{
    [GlobalClass]
    public partial class VoidEvent : Resource
    {
        public delegate void EventDelegate();
        private event EventDelegate OnEvent;

        public void RaiseEvent()
        {
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