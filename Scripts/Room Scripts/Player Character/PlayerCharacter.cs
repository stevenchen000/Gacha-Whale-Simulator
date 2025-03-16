using Godot;
using System;

namespace PlayerCharacterCreator
{
    public partial class PlayerCharacter : Node
    {
        [Export] private Sprite2D bodySprite;

        [ExportCategory("Head")]
        [Export] private Sprite2D eyesSprite;
        [Export] private Sprite2D mouthSprite;
        [Export] private Sprite2D hairBackSprite;
        [Export] private Sprite2D hairFrontSprite;

        public void SetupCharacter()
        {

        }
    }
}