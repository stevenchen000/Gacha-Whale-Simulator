using Godot;
using System;

namespace EventSystem{ 
    public partial class GameEvent<T> : Resource
    {
        public delegate void EventDelegate(T character);
        private event EventDelegate OnEvent;

        public void RaiseEvent(T parameter)
        {
            if(OnEvent != null)
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