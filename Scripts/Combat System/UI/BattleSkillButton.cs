using Godot;
using System;

namespace CombatSystem
{
    public partial class BattleSkillButton : Button
    {
        public CharacterSkill skill { get; private set; }


        public override void _GuiInput(InputEvent @event)
        {
            if(@event is InputEventScreenTouch)
            {
                var touchEvent = @event as InputEventScreenTouch;
                bool pressed = touchEvent.Pressed;

                if (!pressed)
                {

                }
            }
        }

    }
}