using Godot;
using System;

namespace RpgPart 
{ 
    public partial class World1 : Node2D
    {
        [Export] private PackedScene particleHIt;
        public override void _Ready()
        {
            //Avoid freeze
            Node2D newParticle = particleHIt.Instantiate() as Node2D;
            AddChild(newParticle);
            newParticle.Position = new Vector2(9999,9999);
        }
    }
}

