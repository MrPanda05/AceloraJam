using Godot;
using System;


namespace RpgPart.Spawners
{
    public partial class MonsterSpawner : Node2D
    {
        private Timer timer;

        private int spawnMax = 1, spawnCount = 0;

        [Export] public PackedScene monster;

        public void SpawnMonsters()
        {
            for (; spawnCount < spawnMax; spawnCount++)
            {
                CharacterBody2D newMonster = monster.Instantiate() as CharacterBody2D;
                GetParent().AddChild(newMonster);
                newMonster.Position = Position;
            }
        }

        public void OnTimerTimeout()
        {
            SpawnMonsters();
        }


    }
}
