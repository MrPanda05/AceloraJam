using Commons.Components;
using Godot;
using SingleToons;
using System;


namespace RocksObstacles
{
    public partial class HealItem : CharacterBody2D
    {
        [Export] private float speed = 800f;

        private Timer timer;

        private Vector2 vel;

        public static Action<bool> OnHealItemFree;

        private HitboxComponent hitbox;

        public void OnTimerTimeout()
        {
            QueueFree();
            OnHealItemFree?.Invoke(false);
        }
        public override void _Ready()
        {
            timer = GetNode<Timer>("Timer");
            vel = Velocity;
            hitbox = GetNode<HitboxComponent>("HitboxComponent");
        }

        public void OnHitboxComponentAreaEntered(Area2D area)
        {
            if (area.GetParent().Name != "SpaceShip") return;
            GD.Print("PLAYER HEAL");
            AudioManager.Instance.PlayHealSound();
            QueueFree();
            OnHealItemFree?.Invoke(false);
        }
        public override void _PhysicsProcess(double delta)
        {
            vel = Vector2.Left * speed;
            Velocity = vel;
            MoveAndSlide();
        }
    }
}
