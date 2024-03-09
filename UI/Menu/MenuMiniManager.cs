using Godot;
using System;

namespace UIgame
{
    public partial class MenuMiniManager : Node2D
    {
        [Export] private Control menuUI;
        [Export] private MenuUi ui;
        

        public override void _PhysicsProcess(double delta)
        {
            if(Input.IsActionJustPressed("Menu") && !ui.isOnTittleScreen)
            {
                GetTree().Paused = !GetTree().Paused;
                menuUI.Visible = !menuUI.Visible;
                ui.Settings.Visible = false;
                ui.Buttons.Visible = true;
            }
        }
    }
}
