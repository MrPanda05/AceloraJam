using Godot;
using System;



public partial class RocketAnimBasic : AnimatedSprite2D
{

    public override void _Ready()
    {
        Play("JetFull");
    }
}
