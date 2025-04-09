using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSystem
{
    public partial class BDragonElementEffect : Node2D, ISkillEffect
    {
        [Export] private SpriteSheetYSetter colorMod;

        public void SetupForSkill(TurnData data)
        {
            if (colorMod != null)
            {
                var caster = data.caster;
                var element = caster.CharacterElement;
                int colorIndex = BDragonElementChart.GetIntFromElement(element);
                colorMod.SetColorIndex(colorIndex);
            }
        }
    }
}
