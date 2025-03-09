using Godot;
using System;

namespace EventSystem
{
    public partial class VoidEventHandler : Node
    {
        [Signal]
        public delegate void MyVoidEventHandler();
        /// <summary>
        /// Test Tooltip
        /// </summary>
        [Export] private VoidEvent voidEvent;

        public override void _Ready()
        {
            base._Ready();
            voidEvent.SubscribeEvent(CallEvent);
        }

        public override void _Notification(int what)
        {
            if(what == NotificationPredelete)
            {
                voidEvent.UnsubscribeEvent(CallEvent);
            }
        }

        private void CallEvent()
        {
            EmitSignal(SignalName.MyVoid);
        }
    }
}