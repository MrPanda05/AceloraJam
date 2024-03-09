using Godot;
using RocksObstacles;
using System;
using Monsters;


namespace Spawners
{
    public partial class MonsterSpawner : Node2D
    {
        [Export] private PackedScene MonsterScene;
        [Export] private int maxMonster = 3;

        private int monsterCounter;

        private Timer timer;

        public bool working = true;

        public void DecreaseCounter(bool value)
        {
            monsterCounter--;
        }
        public void SpawnMonster()
        {
            if (!working) return;
            if(monsterCounter == maxMonster) return;
            Node2D newRock = MonsterScene.Instantiate() as Node2D;
            GetParent().AddChild(newRock);
            monsterCounter++;
            newRock.Position = new Vector2(2000, GD.RandRange(30, 1060));
        }

        public override void _Ready()
        {
            timer = GetNode<Timer>("Timer");
            Monster.OnMonsterFree += DecreaseCounter;
        }
        public void OnTimerTimeout()
        {
            SpawnMonster();
        }
        public override void _ExitTree()
        {
            Monster.OnMonsterFree -= DecreaseCounter;

        }
    }
}
