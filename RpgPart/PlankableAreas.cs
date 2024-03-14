using Godot;
using RpgPart.Playerer;
using RpgPart.RoundManagerr.States;
using RpgPart.States;
using SingleToons;
using System;



namespace RpgPart.PlankAreas
{
    public partial class PlankableAreas : StaticBody2D
    {

        public PlayerRPG player;
        [Export] public int myID = -1;
        [Export] public int repairCost = 1;

        [Export]private CollisionShape2D plankBlock;// The colisor that block the enemy

        [Export]private Sprite2D plankSprite;

        private int enemyCount = 0;

        private bool enemyOnMe;

        private int myHealth;

        private RpgPartStatsHolder stats;
        private Timer timer;

        public void IncreaseCost()
        {
            repairCost += 2;
        }
        public void Reset()
        {
            repairCost = 0;
            myHealth = 5;

        }
        public void OnTimerTimeout()
        {

        }
        public void DamageMe()
        {
            GD.Print("Got damaged");
            myHealth -= enemyCount;

            if(myHealth <= 0)
            {
                plankBlock.Disabled = true;
                plankSprite.Visible = false;
                myHealth = Mathf.Clamp(myHealth, 0, 2147483647);
                AudioManager.Instance.PlanksBreakSounds();
                return;
            }
            AudioManager.Instance.PlanksBreakingSounds();
        }

        public void Repair(int id)
        {
            if (id != myID) return;
            if (player.planksCount < repairCost) return;
            GD.Print("gOT REPAIRED");
            player.planksCount -= repairCost;
            myHealth = stats.plankAreasHealth;
            plankBlock.Disabled = false;
            plankSprite.Visible = true;
            stats.OnPointsUpdate?.Invoke();
        }
        public override void _Ready()
        {
            player = GetTree().GetFirstNodeInGroup("Player") as PlayerRPG;
            stats = GetTree().GetFirstNodeInGroup("GlobalStats") as RpgPartStatsHolder;
            timer = GetNode<Timer>("Timer");
            myHealth = stats.plankAreasHealth;
            player.OnAreaRepair += Repair;
            Waiting.OnWaitingExit += IncreaseCost;
            GameOver.RestartGame += Reset;
        }

        public override void _ExitTree()
        {
            Waiting.OnWaitingExit -= IncreaseCost;
            GameOver.RestartGame -= Reset;
        }
        //Enemy Trigger only
        public void OnBreakableAreaAreaEntered(Area2D area)
        {
            if (plankBlock.Disabled) return;
            GD.Print("An enemy should have been here");
            enemyCount++;
            enemyOnMe = true;
        }
        public void OnBreakableAreaAreaExited(Area2D area)
        {
            enemyCount--;
            if(enemyCount == 0)
            {
                enemyOnMe = false;
            }
        }

        //Player trigger only
        public void OnRepairAreaAreaEntered(Area2D area)
        {
            GD.Print("The player should've been here");
            player.IsOnPlankableArea = true;
            player.plancableAreaID = myID;
        }
        public void OnRepairAreaAreaExited(Area2D area)
        {
            player.IsOnPlankableArea = false;
            player.plancableAreaID = -1;
        }

        public override void _PhysicsProcess(double delta)
        {
            if (enemyOnMe && timer.IsStopped() && !plankBlock.Disabled)
            {
                timer.Start();
                DamageMe();
            }
        }
    }
}
