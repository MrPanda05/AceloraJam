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

        public override void Readys()
        {
            player = GetNode<PlayerRPG>("../../");
            gun = GetNode<Gun>("../../Gun");
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
    }

}
