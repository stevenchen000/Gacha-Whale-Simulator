using Godot;
using System;

namespace EventSystem{ 
    public partial class GameEvent<T> : Resource
    {
        public delegate void EventDelegate(T character);
        private event EventDelegate OnEvent;
        [Export] protected string description;
        [Export] protected bool debug = false;

        public void RaiseEvent(T parameter)
        {
            if(OnEvent != null)
            {
                OnEvent(parameter);
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