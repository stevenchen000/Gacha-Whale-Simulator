using Godot;
using System.Runtime.InteropServices;

namespace CharacterCreator
{
    public partial class CharacterCreatorMenu : Control
    {
        protected CharacterCreatorManager creator;
        [Export] protected bool IsDefault = false;
        public bool IsEnabled { get; private set; } = false;

        public virtual void _Init(CharacterCreatorManager creator)
        {
            this.creator = creator;
            IsEnabled = true;
        }

        public virtual void _OnDisable() 
        {
            creator = null;
            IsEnabled = false;
            QueueFree();
        }

        /// <summary>
        /// Called when characters list changes
        /// </summary>
        public virtual void _Refresh()
        {

        }


    }
}
