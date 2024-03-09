using Godot;
using System;


namespace BeastOfRevelations
{
    public partial class BeastMonologue : Node2D
    {
        [Export] private ColorRect background;


        public async void UnFadeToBlack()
        {
            GD.Print("bruh");
            while (background.Color.A >= 0)
            {
                GD.Print("working");
                background.Color -= new Color(0, 0, 0, 0.005f);
                await ToSignal(GetTree().CreateTimer(0.003f), SceneTreeTimer.SignalName.Timeout);
            }
            await ToSignal(GetTree().CreateTimer(1.5f), SceneTreeTimer.SignalName.Timeout);
        }
       

        public async void WaitAbit(float time)
        {
            await ToSignal(GetTree().CreateTimer(time), Timer.SignalName.Timeout);
        }
        public override void _Ready()
        {
            background.Visible = true;
            background.Color = new Color(0, 0, 0, 1);
            UnFadeToBlack();
        }
    }
}
