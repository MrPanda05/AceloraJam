using Commons.FiniteStateMachine;
using Godot;
using RpgPart.Playerer.GunPart;
using System;

namespace RpgPart.Playerer.States
{
    public partial class PlayState : State
    {
        private PlayerRPG player;

        private Gun gun;

        private RpgPartStatsHolder stats;

        public override void Readys()
        {
            player = GetNode<PlayerRPG>("../../");
            gun = GetNode<Gun>("../../Gun");
            stats = GetTree().GetFirstNodeInGroup("GlobalStats") as RpgPartStatsHolder;
        }

        public override void FixUpdate(float delta)
        {
            player.Move(player.GetWASDInput());
            if (Input.IsActionPressed("Shoot"))
            {
                gun.ShootGun();
            }
            if (Input.IsActionJustPressed("Interact") && player.IsOnPlankableArea)
            {
                player.Repair();
            }
        }

        public void OnHitboxComponentAreaEntered(Area2D area)
        {
            if (area.GetParent().IsInGroup("Monster"))
            {
                GD.Print("Enemy hit me");
                player.hitbox.Damage(stats.enemyDamage);
                player.OnPlayerHit?.Invoke();
                if(player.health.health <= 0)
                {
                    PlayerRPG.OnPlayerDeath?.Invoke();
                }
            }
        }
    }

}
