using Godot;
using System;


namespace DialogueSystem
{
	public partial class Dialoguebox : PanelContainer
	{
		[Export] private Label textLabel;

        private bool showingDialogue;

		private DialoguesBase theDialogue;

        private int DialogueMax, DialogueCount;

        public Action OnAllDialogueFinished;


        public async void ShowDialogue(string text)
        {
            textLabel.Text = "";
            showingDialogue = true;
            foreach (char c in text)
            {
                textLabel.Text += c;
                await ToSignal(GetTree().CreateTimer(0.1f), Timer.SignalName.Timeout);
            }
            showingDialogue = false;
        }
        public void ShowNextDialogue()
        {
            GD.Print($"This dialogue as a total of {DialogueMax} and is on the {DialogueCount}");
            if((DialogueCount >= DialogueMax - 1))
            {
                GD.Print("Can't");
                return;
            }
            else
            {
                DialogueCount++;
            }
            ShowDialogue(theDialogue.Dialogue[DialogueCount]);
            if(DialogueCount == DialogueMax - 1)
            {
                GD.Print("Ended");
                OnAllDialogueFinished?.Invoke();
            }
        }


        public override void _Ready()
        {
            theDialogue = GetNode<DialoguesBase>("Dialogue");
            showingDialogue = false;
            GD.Print(theDialogue.GetLenght());
            DialogueMax = theDialogue.GetLenght();
            DialogueCount = 0;
            //ShowDialogue(theDialogue.Dialogue[DialogueCount]);
        }

        public override void _PhysicsProcess(double delta)
        {
            if (Input.IsActionJustPressed("Shoot") && !showingDialogue)
            {
                ShowNextDialogue();
            }
        }
    }
}
