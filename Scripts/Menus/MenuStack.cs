using Godot;
using Godot.Collections;
using System;

public partial class MenuStack : Node
{
    private Array<StackableMenu> menus = new Array<StackableMenu>();

    public override void _Ready()
    {

    }

    public void OpenMenu(PackedScene menuScene)
    {
        if (menuScene != null)
        {
            var menu = Utils.InstantiateCopy<StackableMenu>(menuScene);
            menus.Add(menu);
            AddChild(menu);
            menu.InitMenu(this);
        }
    }

    public void CloseCurrentMenu()
    {
        int menuCount = menus.Count;

        if (menuCount > 0)
        {
            var currMenu = menus[menuCount - 1];
            currMenu.CloseMenu();
            menus.RemoveAt(menuCount - 1);
        }
    }
}
