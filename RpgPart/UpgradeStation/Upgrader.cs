using Godot;
using RpgPart.Playerer.GunPart;
using RpgPart.Playerer;
using System;


namespace RpgPart.Player.UpgradeStation
{
    public partial class Upgrader : Node2D
    {
        private PlayerRPG player;
        private Gun gun;

        public void IncreasePlankCapcity(int amount)
        {
            player.maxPlanks += amount;
        }
        public void IncreaseDamage(float amount)
        {
            gun.bulletDamage += amount;
        }
    }
}
