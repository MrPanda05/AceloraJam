using Godot;
using System;


namespace AnimationsBruh
{

	public partial class AnimaBeast : AnimationPlayer
	{

        public override void _Ready()
        {
        }
        public void OnAnimationFinishedbruh(string name)
        {
            GD.Print("finished cutscene");
            GetTree().ChangeSceneToFile("res://RpgPart/World1.tscn");
        }
    }
}
