using System;
using Godot;

namespace CalendarEvent
{
    /// <summary>
    /// In-game events
    /// </summary>
    public partial class CalendarEvent : Resource
    {
        [Export] public bool IsRecurring = false;
        [Export] public bool IsPermanent = false;

        /// <summary>
        /// Returns true if the event can be started
        /// </summary>
        /// <returns></returns>
        public virtual bool EventConditionsMet()
        {
            return false;
        }
    }
}
