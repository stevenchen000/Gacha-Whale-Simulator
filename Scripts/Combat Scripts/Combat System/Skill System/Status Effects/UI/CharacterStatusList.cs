using Godot;
using Godot.Collections;
using SkillSystem;
using System;

public partial class CharacterStatusList : Node
{
    [Export] private EffectManager characterEffects;
    [Export] private Node statusList;
    private Array<CharacterStatusUI> statusListUI;


    public override void _Ready()
    {
        FindChildren();
        CallDeferred(MethodName.UpdateUI);
    }

    private void FindChildren()
    {
        statusListUI = new Array<CharacterStatusUI>();
        var children = statusList.GetChildren();

        foreach(var child in children)
        {
            if(child is CharacterStatusUI)
            {
                var cs = (CharacterStatusUI)child;
                statusListUI.Add(cs);
                cs.Hide();
            }
        }
    }


    public void UpdateUI()
    {
        var effectsList = characterEffects.StatusEffects;

        for(int i = 0; i < statusListUI.Count; i++)
        {
            var cs = statusListUI[i];

            if(effectsList.Count > i)
            {
                var effect = effectsList[i];
                cs.Show();
                cs.SetStatus(effect);
            }
            else
            {
                cs.Hide();
            }
        }
    }
}
