using Commons.Components;
using Commons.FiniteStateMachine;
using Godot;
using System;
using System.Threading.Tasks;


namespace RpgPart.Enemies
{
    public partial class Enemy : CharacterBody2D
    {
        [Export] public float speed;

        [Export] private HitboxComponent hitbox;
        [Export] private HealthComponent health;


        [Export] private Fsm fsm;

        private RpgPartStatsHolder stats;

        private int myDamage;

        public Timer timer;

        public bool isOnBreakable;

        public void OnTimerTimeout()
        {
            if (!isOnBreakable) return;
            GD.Print("Break again");

        }

        public override void _Ready()
        {
            stats = GetTree().GetFirstNodeInGroup("GlobalStats") as RpgPartStatsHolder;
            timer = GetNode<Timer>("Timer");
            myDamage = stats.enemyDamage;
            health.health = stats.enemyHealth;

        }

        public void OnHitboxComponentAreaEntered(Area2D area)
        {
            if (area.GetParent().IsInGroup("Bullet"))
            {
                area.GetParent().QueueFree();
                hitbox.Damage(stats.playerDamage);
                GD.Print("Got hit");
                return;
            }
            if (area.IsInGroup("Breakable"))
            {
                isOnBreakable = true;
                timer.Start();
                GD.Print("Started breaking");
                return;
            }
        }

        public void OnHitboxComponentAreaExited(Area2D area)
        {
            if (area.IsInGroup("Breakable"))
            {
                isOnBreakable = false;
                timer.Stop();
                GD.Print("Stoped breaking");
            }
        }
    }
}
