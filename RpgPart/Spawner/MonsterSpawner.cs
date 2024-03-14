using Godot;
using RpgPart.RoundManagerr;
using System;


namespace RpgPart.Spawners
{
    public partial class MonsterSpawner : Node2D
    {
        private Timer timer;

        private RoundManager roundManager;

        [Export] public PackedScene monster;

        [Export] Godot.Collections.Array<Node2D> spawnPoints;
        
        public void SpawnMonsters()
        {
            if (!roundManager.canSpawn) return;
            if (roundManager.enemySpawned >= roundManager.maxEnemys) return;
            CharacterBody2D newMonster = monster.Instantiate() as CharacterBody2D;
            uint randSpawnPoint = GD.Randi() % 3;
            GetParent().AddChild(newMonster);
            newMonster.GlobalPosition = spawnPoints[(int)randSpawnPoint].GlobalPosition;
            roundManager.enemyCount++;
            roundManager.enemySpawned++;
            GD.PrintErr($"Spawned my {roundManager.enemySpawned} of a total of {roundManager.maxEnemys}");
        }

        public void OnTimerTimeout()
        {
            SpawnMonsters();
        }

        public override void _Ready()
        {
            roundManager = GetTree().GetFirstNodeInGroup("RoundManager") as RoundManager;
        }


    }
}
