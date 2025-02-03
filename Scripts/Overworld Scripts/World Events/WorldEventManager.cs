using Godot;
using System;
using Godot.Collections;
using DialogueSystem;
using EventSystem;

public partial class WorldEventManager : Node
{
    [Export] private DialogueEvent OnDialogueEndEvent;
    [Export] private Array<WorldEventDialogueEvent> dialogueEvents;
    private Dictionary<DialogueTree, WorldEventDialogueEvent> _dialogueEvents;

    public override void _Ready()
    {
        base._Ready();
        Init();
        OnDialogueEndEvent.SubscribeEvent(RunDialogueEffect);
    }

    private void RunDialogueEffect(DialogueTree dialogue)
    {
        if (_dialogueEvents.ContainsKey(dialogue))
        {
            var dialogueEvent = _dialogueEvents[dialogue];
            dialogueEvent.effect.ActivateEffect();
        }
    }

    private void Init()
    {
        _dialogueEvents = new Dictionary<DialogueTree, WorldEventDialogueEvent>();
        foreach(var dialogueEvent in dialogueEvents)
        {
            var dialogue = dialogueEvent.afterDialogue;
            _dialogueEvents[dialogue] = dialogueEvent;
        }
    }
}
