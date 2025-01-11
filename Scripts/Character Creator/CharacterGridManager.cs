using Godot;
using System;
using Godot.Collections;
using EventSystem;

namespace CharacterCreator
{
    public partial class CharacterGridManager : GridContainer
    {
        [Export] private PackedScene portraitScene;
        [Export] private VoidEvent OnPortraitAdded;

        private Array<EditorPortraitDisplay> displays;
        [Export] private CharacterCreatorManager ui;

        public override void _Ready()
        {
            base._Ready();
            ui = GodotHelper.FindParentOfType<CharacterCreatorManager>(this);
            displays = new Array<EditorPortraitDisplay>();
            RemoveUnwantedChildren();
            OnPortraitAdded.SubscribeEvent(LoadPortraits);
        }

        public override void _Process(double delta)
        {

        }

        private void AddDisplay()
        {
            var sceneDupe = (PackedScene)portraitScene.Duplicate();
            var display = sceneDupe.Instantiate<EditorPortraitDisplay>();
            AddChild(display);
            displays.Add(display);
        }

        private void SetPortrait(EditorPortraitDisplay display, CustomCharacterPortrait portrait)
        {
            display.SetPortrait(portrait);
        }

        private void RemoveUnwantedChildren()
        {
            var children = GetChildren();
            for (int i = 1; i < children.Count; i++)
            {
                RemoveChild(children[i]);
            }
        }

        private void LoadPortraits()
        {
            GodotHelper.Print(this, "Loading portraits...");
            var portraits = ui.portraits;
            AddEnoughDisplays(portraits.Count);
            for (int i = 0; i < displays.Count; i++)
            {
                var display = displays[i];
                var portrait = portraits[i];
                SetPortrait(display, portrait);
            }
            GodotHelper.Print(this, $"Loaded {portraits.Count} portraits!");
        }

        private void AddEnoughDisplays(int amount)
        {
            int count = amount - displays.Count;

            for (int i = 0; i < count; i++)
            {
                AddDisplay();
            }
        }
    }
}