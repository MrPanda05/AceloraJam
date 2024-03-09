using Godot;
using System;

public partial class RockSprite : Sprite2D
{
    [Export] private Texture2D texture1, texture2, texture3;

    public override void _Ready()
    {
        uint rand = GD.Randi() % 3;
        switch (rand)
        {
            case 0:
                Texture = texture1;
                break;
            case 1:
                Texture = texture2;
                break;
            case 2:
                Texture = texture3;
                break;
        }
    }
}
