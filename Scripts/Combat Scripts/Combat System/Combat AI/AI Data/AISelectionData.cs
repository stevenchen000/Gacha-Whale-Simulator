using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSystem
{
    public class AISelectionData
    {
        public Vector2I Coords { get; private set; }
        public TargetingSelection Selection { get; private set; }
        public List<BattleCharacter> Targets { get; private set; }

        public AISelectionData()
        {

        }
    }
}
