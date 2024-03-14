using Godot;
using System;


namespace RpgPart.UI
{
	public partial class MainMenu : Control
	{
		[Export] private Control Settings, Buttons;
		public void OnGiveUpButtonDown()
		{
			GetTree().Quit();
		}
		public void OnSettingsButtonDown()
		{
            Buttons.Visible = false;
			Settings.Visible = true;
        }
		public void OnStartButtonDown()
		{
            GetTree().ChangeSceneToFile("res://RpgPart/World1.tscn");
        }
    }
}
