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
        [Export] private PackedScene particleHIt;
        //Node2D newParticle = particleHIt.Instantiate() as Node2D;
        //GetParent().AddChild(newParticle);
        //newParticle.Position = Position;
        public void GoToDir()
        {
            vel = new Vector2(dire.X, dire.Y).Normalized() * gun.bulletSpeed;
            Velocity = vel;
        }
        public void OnHitboxComponentAreaEntered(Area2D area)
        {
            if (area.GetParent().IsInGroup("Monster"))
            {
                Node2D newParticle = particleHIt.Instantiate() as Node2D;
                GetParent().AddChild(newParticle);
                newParticle.Position = Position;
            }
        }

        public void OnTimerTimeout()
        {
            //GD.Print("Out of time");
            QueueFree();
        }
        public void OnHitboxComponentBodyEntered(Node2D body)
        {
            if(body.Name == "NonSolid")
            {
                QueueFree();
            }
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
            //GD.Print(player.Name);
            //GD.Print(gun.Name);

        }

        public override void _PhysicsProcess(double delta)
        {
            GoToDir();
            MoveAndSlide();
        }
    }
}
