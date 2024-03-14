using Commons.FiniteStateMachine;
using Godot;
using RpgPart.Enemies;
using System;


namespace RpgPart.RoundManagerr
{
    public partial class RoundManager : Node2D
    {
        [Export] public Label startLabel;

        [Export]private RpgPartStatsHolder stats;

        public int roundCount = 0;

        public int enemysKilled;

        public int maxEnemys = 15, enemyCount, enemySpawned;

        public bool canSpawn;

        private Fsm FSM;
        public void EnemyCounterDeath()
        {
            enemysKilled++;
        }
        public void Reset()
        {
            enemyCount = 0;
            enemySpawned = 0;
            enemysKilled = 0;
        }
        public void IncreaseDifficulty()
        {
            if(roundCount > 15)
            {
                maxEnemys += 10;
                stats.enemyDamage += 5;
                stats.enemyHealth += 5;
                return;
            }
            maxEnemys += 5;
            stats.enemyDamage += 1;
            stats.enemyHealth += 1;
        }
        public override void _Ready()
        {
            Enemy.OnEnemyDeath += EnemyCounterDeath;
        }
        public override void _ExitTree()
        {
            Enemy.OnEnemyDeath -= EnemyCounterDeath;
        }
    }
}
