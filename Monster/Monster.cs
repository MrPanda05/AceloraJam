using Commons.Components;
using Godot;
using SingleToons;
using System;


namespace Monsters
{
    public partial class Monster : CharacterBody2D
    {
        [Export] private float speed = 800f;

        private Timer timer;

        private Vector2 vel;

        public static Action<bool> OnMonsterFree;

        private HitboxComponent hitbox;

        public void OnTimerTimeout()
        {
            QueueFree();
            OnMonsterFree?.Invoke(false);
        }

        public void OnHitboxComponentAreaEntered(Area2D area)
        {
            if(area.GetParent().Name == "SpaceShip") return;

            hitbox.Damage();
            if (hitbox.healthComponent.health == 0)
            {
                OnMonsterFree?.Invoke(true);
                AudioManager.Instance.PlayMonsterhitSounds();
                return;
            }
            AudioManager.Instance.PlayRocksHit();

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
