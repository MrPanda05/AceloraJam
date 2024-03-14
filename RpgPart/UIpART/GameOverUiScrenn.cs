using Godot;
using RpgPart.States;
using System;


namespace RpgPart.UI
{
    public partial class GameOverUiScrenn : Control
    {
        [Export] private ColorRect color;
        public void GetEnable()
        {
            Visible = true;
        }
        public void GetDisable()
        {
            Visible = false;

        }

        public override void _Ready()
        {
            GameOver.OnGameOverEnter += GetEnable;
            GameOver.OnGameOverExit += GetDisable;
        }
        public override void _ExitTree()
        {
            GameOver.OnGameOverEnter -= GetEnable;
            GameOver.OnGameOverExit -= GetDisable;
        }

        public void OnTryAgainButtonDown()
        {
            GetTree().Paused = false;
            GetTree().ChangeSceneToFile("res://RpgPart/World1.tscn");
        }
        public void OnGiveUpButtonDown()
        {
            GetTree().Quit();
        }
    }
}

