using Godot;
using Monsters;
using Player.SpaceShip;
using RocksObstacles;
using System;

namespace UIgame
{
    public partial class Score : Control
    {
	    private int score = 0;

	    private Label label;

        [Export] private SpaceShip player;

        public void AddScoreRock(bool value)
        {
            if(!value) return;

            score += 1;
            UpdateScore();
        }
        public void AddScoreMonster(bool value)
        {
            if(!value) return;

            score += 5;
            UpdateScore();
        }
        public void UpdateScore()
        {
            if(score >= 490 && score <= 510)
            {
                GD.Print("ALMOST WON");
            }
            label.Text = score.ToString();
        }
        public void RestartScore()
        {
            score = 0;
            label.Text = score.ToString();
        }
        public override void _Ready()
        {
            label = GetNode<Label>("Label");
            label.Text = score.ToString();
            Rocks.OnRockFree += AddScoreRock;
            Monster.OnMonsterFree += AddScoreMonster;
            player.OnRestartGame += RestartScore;
        }

        public override void _ExitTree()
        {
            Rocks.OnRockFree -= AddScoreRock;
            Monster.OnMonsterFree -= AddScoreMonster;
            player.OnRestartGame -= RestartScore;
        }

    }
}

