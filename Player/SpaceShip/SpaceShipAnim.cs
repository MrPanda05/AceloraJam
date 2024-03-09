using Godot;
using System;

public partial class SpaceShipAnim : AnimatedSprite2D
{
    public override void _Ready()
    {
        Play("fly");
    }
}
