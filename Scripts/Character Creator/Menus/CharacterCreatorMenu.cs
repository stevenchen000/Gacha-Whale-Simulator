using Godot;
using System.Runtime.InteropServices;

namespace CharacterCreator
{
    public partial class CharacterCreatorMenu : Control
    {
        protected CharacterCreatorManager creator;
        [Export] protected bool IsDefault = false;
        public bool IsEnabled { get; private set; } = false;

        public override void _Ready()
        {
            //GodotHelper.Print(this, "finding creator...");
            creator = (CharacterCreatorManager)FindParent("CharacterCreatorMenu");
            //GodotHelper.Print(this, "Creator found!");
            if (!IsDefault)
            {
                Visible = false;
            }
            else
            {
                _OnEnable();
                Visible = true;
                Position = new Vector2(0, 0);
            }
        }

        public virtual void _OnEnable() { IsEnabled = true; }

        public virtual void _OnDisable() { IsEnabled = false; }

    }
}
