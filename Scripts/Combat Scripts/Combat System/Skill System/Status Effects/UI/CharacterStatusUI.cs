using Godot;
using SkillSystem;
using System;

public partial class CharacterStatusUI : Control
{
    [Export] private TextureRect background;
    [Export] private TextureRect border;
    [Export] private Sprite2D icon;
    [Export] private Label durationText;

    [ExportCategory("Colors")]
    [Export] private Color buffBackgroundColor;
    [Export] private Color buffBorderColor;
    [Export] private Color debuffBackgroundColor;
    [Export] private Color debuffBorderColor;

    private StatusContainer status;

    public override void _Notification(int what)
    {
        if(what == NotificationPredelete)
        {
            status = null;
        }
    }

    public void SetStatus(StatusContainer status)
    {
        this.status = status;
        bool isBuff = status.Status.IsBuff;

        SetupIcon(status);

        if (isBuff) SetAsBuff();
        else SetAsDebuff();

        SetDuration();
    }

    private void SetupIcon(StatusContainer status)
    {
        this.status = status;
        var texture = status.Status.Texture;

        if (texture != null)
        {
            icon.Texture = texture;
            var backgroundSize = background.Size;
            var textureSize = texture.GetSize();

            icon.Scale = backgroundSize / textureSize;
        }
    }

    private void SetAsBuff()
    {
        background.SelfModulate = buffBackgroundColor;
        border.SelfModulate = buffBorderColor;
    }

    private void SetAsDebuff()
    {
        background.SelfModulate = debuffBackgroundColor;
        border.SelfModulate = debuffBorderColor;
    }

    private void SetDuration()
    {
        int duration = status.TurnsLeft;
        durationText.Text = duration.ToString();
    }


    /****************
     * Visibility
     * *************/


    public new void Show()
    {
        Visible = true;
    }

    public new void Hide()
    {
        Visible = false;
    }
}
