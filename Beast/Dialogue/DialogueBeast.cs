using DialogueSystem;
using Godot;
using System;

public partial class DialogueBeast : DialoguesBase
{
    [Export] public Dialoguebox myDialogueBox;

    [Export] private AnimationPlayer anim;
    public async void WaitFade()
    {
        await ToSignal(GetTree().CreateTimer(0.5f), Timer.SignalName.Timeout);
        myDialogueBox.Visible = true;
        myDialogueBox.ShowDialogue(Dialogue[0]);
    }
    public async void PlayCutscene()
    {
        await ToSignal(GetTree().CreateTimer(6f), Timer.SignalName.Timeout);
        myDialogueBox.Visible = false;
        anim.Play("Cutscene");
        GD.Print("Playing cutscene");
    }
    public override void _Ready()
    {
        DialogueAdder(texts);
        WaitFade();
        myDialogueBox.OnAllDialogueFinished += PlayCutscene;
    }

    public override void _ExitTree()
    {
        myDialogueBox.OnAllDialogueFinished -= PlayCutscene;

    }
}
