using Godot;
using System;

namespace EventSystem
{
    public partial class IntEventHandler : Node
    {
        [Signal]
        public delegate void MyIntEventHandler(int value);

        [Export]
        private IntGameEvent myIntEvent;

        public override void _Ready()
        {
            myIntEvent.SubscribeEvent(CallEvent);
        }

        public override void _Notification(int what)
        {
            if(what == NotificationPredelete)
            {
                myIntEvent.UnsubscribeEvent(CallEvent);
            }
        }

        public void CallEvent(int value)
        {
            EmitSignal(SignalName.MyInt, value);
        }
    }
}