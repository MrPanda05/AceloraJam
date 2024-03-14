using Commons.Components;
using Commons.FiniteStateMachine;
using Godot;
using RpgPart.States;
using SingleToons;
using System;
using System.Threading.Tasks;


namespace RpgPart.Enemies
{
    public partial class Enemy : CharacterBody2D
    {
        [Export] public float speed;

        [Export] private HitboxComponent hitbox;
        [Export] private HealthComponent health;
        [Export] public NavigationAgent2D navigationAgent;


        [Export] private Fsm fsm;

        private RpgPartStatsHolder stats;

        private int myDamage;

        public Timer timer;

        public bool isOnBreakable;

        public static Action OnEnemyDeath;
        public void SignalDeath()
        {
            OnEnemyDeath?.Invoke();
        }
        public void ForceRestart()
        {
            QueueFree();
        }
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
            health.OnDeath += SignalDeath;
            GD.Print($"Spawned with {myDamage} of damge and {health.health} health");


        }

        public override void _ExitTree()
        {
            health.OnDeath -= SignalDeath;
        }

        public void OnHitboxComponentAreaEntered(Area2D area)
        {
            if (area.GetParent().IsInGroup("Bullet"))
            {
                area.GetParent().QueueFree();
                hitbox.Damage(stats.playerDamage);
                AudioManager.Instance.PlayRPGMonter();
                //GD.Print("Got hit");
                return;
            }
        }
        
    }
}
