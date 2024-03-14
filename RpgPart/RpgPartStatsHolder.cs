using Godot;
using RpgPart.Enemies;
using RpgPart.States;
using System;


namespace RpgPart
{
    public partial class RpgPartStatsHolder : Node2D
    {
        public int points, pointsCounter;

        public int enemyDamage = 1, enemyHealth = 1;

        public int playerDamage = 1;

        public int plankAreasHealth = 5;//Plank health, set on start and on reset round

        public Action OnPointsUpdate;

        public void Restart()
        {
            enemyDamage = 1;
            enemyHealth = 1;
            points = 0;
            pointsCounter = 0;
            plankAreasHealth = 5;
        }

        public void GivePoints()
        {
            pointsCounter += 5;
            if(pointsCounter % 15 == 0)
            {
                GD.Print("You got 1 point");
                points++;
                pointsCounter = 0;
                OnPointsUpdate?.Invoke();
            }
        }

        public override void _Ready()
        {
            Enemy.OnEnemyDeath += GivePoints;
        }
        public override void _ExitTree()
        {
            Enemy.OnEnemyDeath -= GivePoints;
        }
    }
}
