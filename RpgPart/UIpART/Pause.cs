using Godot;
using RpgPart.States;
using System;

namespace RpgPart.UI
{
	public partial class Pause : Control
	{
        [Export] private Control pauseButton, settings;
        public void EnableMe()
        {
            Visible = true;
        }
        public void DisableMe()
        {
            Visible = false;
            settings.Visible = false;
            pauseButton.Visible = true;

        }


        public override void _Ready()
        {
            PauseState.OnPauseEnter += EnableMe;
            PauseState.OnPauseExit += DisableMe;
        }
        public override void _ExitTree()
        {
            PauseState.OnPauseEnter -= EnableMe;
            PauseState.OnPauseExit -= DisableMe;
        }

        public void OnGiveUpButtonButtonDown()
        {
            GetTree().Quit();
        }
        public void OnSettingsButtonButtonDown()
        {
            settings.Visible = true;
            pauseButton.Visible = false;
        }
    }
}
