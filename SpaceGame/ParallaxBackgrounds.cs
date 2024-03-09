using Godot;
using System;

public partial class ParallaxBackgrounds : ParallaxBackground
{

    private Vector2 offset;
    public override void _Ready()
    {
        offset = ScrollOffset;
    }
    public override void _Process(double delta)
    {
        offset.X -= 40 * (float)delta;
        ScrollOffset = offset;
    }
}
