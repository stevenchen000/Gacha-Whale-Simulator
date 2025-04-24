using System;
using Godot;
using Godot.Collections;

namespace SkillSystem
{
    public partial class StatusEffectsDict : Node
    {
        private static StatusEffectsDict instance;

        [Export] private Array<StatusEffect> _Statuses;
        public Dictionary<int, StatusEffect> Statuses { get; private set; } = new Dictionary<int, StatusEffect>();



        public override void _Ready()
        {
            if (instance == null)
            {
                instance = this;
                foreach(var status in _Statuses)
                {
                    if (status != null)
                    {
                        int id = status.ID;
                        Statuses[id] = status;
                    }
                }
            }
            else
                QueueFree();
        }


        public static StatusEffect GetStatusByID(int id)
        {
            StatusEffect result = null;

            if (instance.Statuses.ContainsKey(id))
            {
                result = instance.Statuses[id];
            }

            return result;
        }
    }
}
