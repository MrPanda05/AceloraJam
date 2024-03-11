using Godot;
using System;


namespace RpgPart.Playerer.GunPart
{

    public partial class Gun : Node2D
    {

        [Export] private float radius;

        [Export] private PackedScene bulletScene;

        [Export] public float bulletSpeed = 300.0f;
        [Export] public float bulletDamage = 1f;
        [Export] public float fireDelay = 1f;

        private Marker2D bulletPoint;

        private Vector2 mousePos, rotation;
        private CharacterBody2D player;

        private Timer fireRate;

        public override void _Ready()
        {
            player = GetParent() as CharacterBody2D;
            bulletPoint = GetNode<Marker2D>("BulletHole");
            fireRate = GetNode<Timer>("FireRate");

        }

        public void ShootGun()
        {
            if (!fireRate.IsStopped()) return;
            fireRate.Start(fireDelay);
            CharacterBody2D newBullet = bulletScene.Instantiate() as CharacterBody2D;
            GetParent().GetParent().AddChild(newBullet);
            newBullet.GlobalPosition = bulletPoint.GlobalPosition;
        }
        public void MoveGun()
        {
            mousePos = GetViewport().GetMousePosition();

            rotation = (mousePos - player.Position).Normalized() * radius;

            Position = rotation;

            LookAt(GetViewport().GetMousePosition());

        }
        public override void _PhysicsProcess(double delta)
        {
            MoveGun();

        }
    }
}
