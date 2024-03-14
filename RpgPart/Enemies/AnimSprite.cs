using Godot;
using RpgPart.Playerer;
using System;

namespace RpgPart.Enemies
{
    public partial class AnimSprite : AnimatedSprite2D
    {
        [Export] private Enemy enemy;
        private PlayerRPG player;
        public override void _Ready()
        {
            player = GetTree().GetFirstNodeInGroup("Player") as PlayerRPG;
            uint rng = GD.Randi() % 3;
            switch (rng)
            {
                case 0:
                    Play("Walker");
                    break;
                case 1:
                    Play("Crawler");
                    break;
                case 2:
                    Play("Head");
                    break;
            }
        }
        private void RotateToTarget(Vector2 target, float delta)
        {
            Vector2 direction = target - GlobalPosition;
            float angle = Transform.X.AngleTo(direction);
            Rotate(Mathf.Sign(angle) * Mathf.Min(delta * 300, Mathf.Abs(angle)));
        }
        public override void _Process(double delta)
        {
            //RotateToTarget(player.GlobalPosition, (float)delta);
            if(enemy.Position.X - player.Position.X < 0)
            {
                FlipH = true;
            }else
            {
                FlipH = false;
            }
        }
    }
}
