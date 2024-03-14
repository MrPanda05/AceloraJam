using Godot;
using RpgPart.Playerer.GunPart;
using RpgPart.Playerer;
using System;
using RpgPart.RoundManagerr.States;


namespace RpgPart.Playerer.UpgradeStation
{
    public partial class Upgrader : Node2D
    {
        [Export] private Control ShopUI;
        private PlayerRPG player;
        private Gun gun;
        private RpgPartStatsHolder stats;
        public Action OnUpgrade;
        public void EnableSHOP()
        {
            ShopUI.Visible = true;
        }
        public void DisableSHOP()
        {
            ShopUI.Visible = false;
        }
        public void IncreasePlankCapcity(int amount)
        {
            GD.PrintErr("Increase plank capacity");
            player.maxPlanks += amount;
            OnUpgrade?.Invoke();
        }
        public void BuyPlanks()
        {
            GD.PrintErr("Bought planks");
            if (player.planksCount+1 > player.maxPlanks) return;
            player.planksCount++;
            OnUpgrade?.Invoke();
        }
        public void IncreaseDamage(int amount)
        {

            GD.PrintErr("Increase damage");
            stats.playerDamage += amount;
            OnUpgrade?.Invoke();

        }
        public void IncreaseFireRate(float amount)
        {
            if (gun.fireDelay <= 0.01f) return;
            GD.PrintErr("Increase fire rate");
            gun.fireDelay -= 0.05f;
            gun.fireDelay = Mathf.Clamp(gun.fireDelay, 0.05f, 999999);
            OnUpgrade?.Invoke();

        }
        public void Heal()
        {
            GD.PrintErr("Got heal");
            player.health.health = player.health.maxHealth;
            OnUpgrade?.Invoke();

        }
        public void HealthUpgrade()
        {
            GD.PrintErr("Health upgrade");
            player.health.maxHealth += 1;
            OnUpgrade?.Invoke();

        }

        public override void _Ready()
        {
            Waiting.OnWaitingEnter += EnableSHOP;
            Waiting.OnWaitingExit += DisableSHOP;
            //Get nodes here
            player = GetTree().GetFirstNodeInGroup("Player") as PlayerRPG;
            gun = GetTree().GetFirstNodeInGroup("Gun") as Gun;
            stats = GetTree().GetFirstNodeInGroup("GlobalStats") as RpgPartStatsHolder;


        }

        public override void _ExitTree()
        {
            Waiting.OnWaitingEnter -= EnableSHOP;
            Waiting.OnWaitingExit -= DisableSHOP;
        }
    }
}
