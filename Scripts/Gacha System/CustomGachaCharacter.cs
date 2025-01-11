using Godot;
using System;
using Godot.Collections;

namespace GachaSystem{
    [GlobalClass]
    public partial class CustomGachaCharacter : GachaCharacter
    {
        [Export] private Array<string> skinPortraitFiles;

        public CustomGachaCharacter()
        {
            skinPortraitFiles = new Array<string>();
        }

        public CustomGachaCharacter(CustomCharacterPortrait portrait)
        {
            string portraitFilename = portrait.ResourcePath;
            skinPortraitFiles.Add(portraitFilename);
        }

        public CharacterPortrait GetPortrait(int skin = 0)
        {
            CharacterPortrait result = null;

            if(skin < 0 || skin >= skinPortraitFiles.Count)
            {
                result = GetDefaultPortrait();
            }
            else
            {
                string filename = skinPortraitFiles[skin];
                result = FileManager.GetCustomPortrait(filename);
            }

            return result;
        }

        private CharacterPortrait GetDefaultPortrait()
        {
            CharacterPortrait result = null;

            if(skinPortraitFiles.Count != 0)
            {
                string filename = skinPortraitFiles[0];
                result = FileManager.GetCustomPortrait(filename);
            }

            return result;
        }
    }
}