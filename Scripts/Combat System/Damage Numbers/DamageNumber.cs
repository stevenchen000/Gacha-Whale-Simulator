using System;
using Godot;
using GachaSystem;
using Godot.Collections;

namespace CombatSystem
{
    public partial class DamageNumber : Control
    {
        [Export] private Label textLabel;
        [Export] private double speed = 30;
        [Export] private double duration = 2;
        private TimeHandler time;

        public override void _Ready()
        {
            time = new TimeHandler();
        }

        public override void _Process(double delta)
        {
            double y = -speed * delta;
            Position += new Vector2(0, (float)y);
            if (time.TimeIsBetween(duration))
            {
                QueueFree();
            }

            time.Tick(delta);
        }

        public void SetValue(int damage)
        {
            textLabel.Text = damage.ToString();
        }

        
    }
}
