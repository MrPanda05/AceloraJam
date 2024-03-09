using Godot;
using Monsters;
using RocksObstacles;
using System;

namespace Spawners
{
    public partial class HealingSpawner : Node2D
    {
        [Export] private PackedScene healItem;
        [Export] private int maxItems = 3;

        private int itemCounter;

        private Timer timer;

        public bool working = true;

        public void DecreaseCounter(bool value)
        {
            itemCounter--;
        }
        public void SpawnHeal()
        {
            if (!working) return;
            if (itemCounter == maxItems) return;
            Node2D newRock = healItem.Instantiate() as Node2D;
            GetParent().AddChild(newRock);
            itemCounter++;
            newRock.Position = new Vector2(2000, GD.RandRange(30, 1060));
        }

        public override void _Ready()
        {
            timer = GetNode<Timer>("Timer");
            HealItem.OnHealItemFree += DecreaseCounter;
        }
        public void OnTimerTimeout()
        {
            SpawnHeal();
        }
        public override void _ExitTree()
        {
            HealItem.OnHealItemFree -= DecreaseCounter;

        }
    }
}
