using Godot;
using System;


namespace RpgPart.Playerer.GunPart
{ 
	public partial class Bullet : CharacterBody2D
	{
		private PlayerRPG player;

        private Gun gun;

        private Vector2 mousePos, dire, vel;

        private Timer timer;

        public void GoToDir()
        {
            vel = new Vector2(dire.X, dire.Y).Normalized() * gun.bulletSpeed;
            Velocity = vel;
        }

        public void OnTimerTimeout()
        {
            GD.Print("Out of time");
            QueueFree();
        }
        public override void _Ready()
        {
            timer = GetNode<Timer>("Timer");
            player = GetTree().GetFirstNodeInGroup("Player") as PlayerRPG;
            gun = GetTree().GetFirstNodeInGroup("Gun") as Gun;
            vel = Velocity;
            mousePos = GetViewport().GetMousePosition();
            dire = mousePos - player.Position;
            LookAt(dire);
            GD.Print(player.Name);
            GD.Print(gun.Name);

        }

        public override void _PhysicsProcess(double delta)
        {
            GoToDir();
            MoveAndSlide();
        }
    }
}
