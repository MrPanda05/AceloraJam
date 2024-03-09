using Commons.Components;
using Godot;
using SingleToons;
using System;


namespace RocksObstacles
{
	public partial class Rocks : CharacterBody2D
	{
		[Export] private float speed = 800f;

        private Timer timer;

        private Vector2 vel;

        public static Action<bool> OnRockFree;

        private HitboxComponent hitbox;




        public void OnTimerTimeout()
        {
            QueueFree();
            OnRockFree?.Invoke(false);
        }

        public void OnHitboxComponentAreaEntered(Area2D area)
        {
            if (area.GetParent().Name == "SpaceShip") return;

            AudioManager.Instance.PlayRocksHit();
            hitbox.Damage();
            if (hitbox.healthComponent.health == 0)
            {
                OnRockFree?.Invoke(true);
                return;
            }

        }
        public override void _Ready()
        {
            timer = GetNode<Timer>("Timer");
            vel = Velocity;
            hitbox = GetNode<HitboxComponent>("HitboxComponent");
        }

        public override void _PhysicsProcess(double delta)
        {
            vel = Vector2.Left * speed;
            Velocity = vel;
            MoveAndSlide();
        }
    }
}
