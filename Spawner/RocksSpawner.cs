using Godot;
using RocksObstacles;
using System;


namespace Spawners 
{
    public partial class RocksSpawner : Node2D
    {
        [Export] private PackedScene RockScene;
        [Export] private int maxRock = 60;

        private int rockCounter;

        private Timer timer;

        public bool working = true;

        public void DecreaseCounter(bool value)
        {
            rockCounter--;
        }
        public void SpawnRock()
        {
            if (!working) return;
            if (rockCounter == maxRock) return;
            Node2D newRock = RockScene.Instantiate() as Node2D;
            GetParent().AddChild(newRock);
            rockCounter++;
            newRock.Position = new Vector2(2000, GD.RandRange(30, 1060));
        }

        public override void _Ready()
        {
            timer = GetNode<Timer>("Timer");
            Rocks.OnRockFree += DecreaseCounter;
        }
        public void OnTimerTimeout()
        {
            SpawnRock();
        }
        public override void _ExitTree()
        {
            Rocks.OnRockFree -= DecreaseCounter;

        }
    }
}

