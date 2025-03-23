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

        [ExportCategory("Damage Colors")]
        [Export] private Color ampDamageColor;
        [Export] private Color critAmpDamageColor;
        [Export] private Color hpDamageColor;
        [Export] private Color healingDamageColor;


        public override void _Ready()
        {
            time = new TimeHandler();
        }

        public override void _Process(double delta)
        {
            /*double y = -speed * delta;
            Position += new Vector2(0, (float)y);*/
            if (time.TimeIsBetween(duration))
            {
                QueueFree();
            }

            time.Tick(delta);
        }

        public void SetValue(int damage, DamageType type)
        {
            textLabel.Text = damage.ToString();
            switch (type)
            {
                case DamageType.HealthDamage:
                    Modulate = hpDamageColor;
                    Scale = new Vector2(2, 2);
                    break;
                case DamageType.AmpDamage:
                    Modulate = ampDamageColor;
                    break;
                case DamageType.CritDamage:
                    Modulate = critAmpDamageColor;
                    break;
                case DamageType.Healing:
                    Modulate = healingDamageColor;
                    break;
                case DamageType.Battery:
                    Modulate = healingDamageColor;
                    break;
            }
        }

        
    }
}
