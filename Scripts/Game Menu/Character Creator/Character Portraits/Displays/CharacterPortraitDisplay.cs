using Godot;
using System;
using GachaSystem;
using EventSystem;

public partial class CharacterPortraitDisplay : Control
{
	[Export] private TextureRect background;
	[Export] private TextureRect border;
	[Export] private Sprite2D portraitElement;
	[Export] private float borderSize = 5f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdatePortrait(CharacterPortrait portrait)
    {
		if(portrait == null)
        {
			portraitElement.Texture = null;
        }
        else
        {
			portrait.SetupPortraitSimple(background, portraitElement, borderSize);
        }
    }

	public void SetBorder(PortraitBorder border)
    {
		background.Texture = border.Background;
		if(this.border != null)
			this.border.Texture = border.Border;
		CenterBorder();
    }

	private void CenterBorder()
    {
		var backgroundSize = background.Size;
		var borderSize = border.Size;

		var diff = borderSize - backgroundSize;
		border.Position = background.Position - (diff / 2);
    }
}
