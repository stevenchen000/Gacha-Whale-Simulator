using Godot;
using System;

public partial class GachaBottomBar : Control
{
	[Export] private PackedScene characterMenuScene;
	[Export] private PackedScene bannersMenuScene;
	private Control phoneScreen;

	private Control characterMenu;
	private Control bannersMenu;
	private Control currentMenu;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		phoneScreen = (Control)FindParent("Phone Screen");
		GD.Print(phoneScreen.Name);
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnCharacterButton()
    {
		CloseCurrentMenus();

		if (characterMenu == null)
			characterMenu = (Control)characterMenuScene.Instantiate();
		currentMenu = characterMenu;
		InitMenu(currentMenu);
    }

	public void OnBannersButton()
    {
		CloseCurrentMenus();

		if (bannersMenu == null)
			bannersMenu = (Control)bannersMenuScene.Instantiate();
		currentMenu = bannersMenu;
		InitMenu(currentMenu);
	}



	/****************
	 * Helper Functions
	 ****************/

	private void CloseCurrentMenus()
    {
		if(currentMenu != null)
        {
			var parent = currentMenu.GetParent();
			parent.RemoveChild(currentMenu);
			currentMenu.Visible = false;
			currentMenu = null;
        }
    }

	private void InitMenu(Control menu)
    {
		menu.Visible = true;
		phoneScreen.AddChild(menu);
		phoneScreen.MoveChild(menu, 0);
		currentMenu.SetPosition(new Vector2(10, 10));
		currentMenu.Scale = new Vector2(1, 1);
	}
}
