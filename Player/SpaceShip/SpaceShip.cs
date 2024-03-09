using Commons.Components;
using Commons.FiniteStateMachine;
using Godot;
using System;

namespace Player.SpaceShip
{
	public partial class SpaceShip : CharacterBody2D
	{
        [Export] private float speed = 300f;
        [Export] private PackedScene bullet;
        [Export] public HitboxComponent hitbox;
        [Export] public PackedScene particleScene;
        [Export] public SoundPool soundPool, soundPoolHit;
        public Fsm playerFSM;

        private Timer fireRateTimer;

		private Vector2 vel;

        public Action OnPlayerDeath, OnRestartGame;

        public static Action OnPlayerHealthUpdate;

        public override void _Ready()
        {
            vel = Velocity;
            playerFSM = GetNode<Fsm>("FSM");
            fireRateTimer = GetNode<Timer>("fireRate");
        }

        public void Move(Vector2 input)
        {
            vel = input * speed;
            Velocity = vel;
            MoveAndSlide();
        }
        public void Shoot()
        {
            if (fireRateTimer.TimeLeft > 0) return;
            soundPool.PlayRandomSound();
            Node2D newBullet = bullet.Instantiate() as Node2D;
            GetParent().AddChild(newBullet);
            newBullet.Position = Position;
            fireRateTimer.Start();

        }

        public void Revive()
        {
            hitbox.healthComponent.health = 3;
            Visible = true;
            Position = new Vector2(291, 559);
            ProcessMode = ProcessModeEnum.Inherit;
            playerFSM.TransitioToState("PlayState");
        }
    }

}
