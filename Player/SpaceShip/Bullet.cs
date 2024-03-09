using Godot;
using Particles;
using System;

namespace Player.SpaceShip
{
	public partial class Bullet : CharacterBody2D
	{
		[Export] private float speed;
        [Export] private PackedScene particleHIt;
		private Timer timer;


        public void OnHitboxComponentAreaEntered(Area2D area)
        {
            Node2D newParticle = particleHIt.Instantiate() as Node2D;
            GetParent().AddChild(newParticle);
            newParticle.Position = Position;
            GD.Print("Destryoing");
            QueueFree();
        }
        public void OnTimerTimeout()
        {
            QueueFree();
        }
        public override void _Ready()
        {
            timer = GetNode<Timer>("Timer");

        }
        public override void _PhysicsProcess(double delta)
        {
            Velocity = Vector2.Right * speed;
            MoveAndSlide();
        }
    }
}
