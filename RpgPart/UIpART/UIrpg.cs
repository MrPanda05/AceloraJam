using Godot;
using RpgPart.Playerer;
using RpgPart.Playerer.UpgradeStation;
using RpgPart.RoundManagerr.States;
using System;

namespace RpgPart.UI
{
	public partial class UIrpg : Control
	{
		
		public int maxHealth, health;

		[Export]public HSlider slider;

		[Export]public Label healthLabel, pointsLabel, plankLabel, timeLabel;

        private Timer timer;

		private PlayerRPG player;
        private RpgPartStatsHolder stats;
        private Upgrader upgrader;

        public void UpdateHealth()
		{
            maxHealth = (int)player.health.maxHealth;
            health = (int)player.health.health;
            healthLabel.Text = $"{health}/{maxHealth}";
            slider.MaxValue = maxHealth;
            slider.Value = health;
        }
        public void UpdatePoints()
        {
            pointsLabel.Text = stats.points.ToString();
        }
        public void UpdatePlank()
        {
            plankLabel.Text = player.planksCount.ToString();
        }
        public void EnableTimer()
        {
            timer.Start(30);
            timeLabel.Visible = true;
            timeLabel.Text = "30";
        }
        public void DisableTimer()
        {
            timer.Stop();
            timeLabel.Visible = false;
        }


        public override void _Ready()
        {
            player = GetTree().GetFirstNodeInGroup("Player") as PlayerRPG;
            stats = GetTree().GetFirstNodeInGroup("GlobalStats") as RpgPartStatsHolder;
            upgrader = GetTree().GetFirstNodeInGroup("Upgrader") as Upgrader;
            timer = GetNode<Timer>("TimerWai");
            maxHealth = (int)player.health.maxHealth;
			health = (int)player.health.health;
			slider.MaxValue = maxHealth;
			slider.Value = health;
			healthLabel.Text = $"{health}/{maxHealth}";
            pointsLabel.Text = stats.points.ToString();
            plankLabel.Text = player.planksCount.ToString();
            stats.OnPointsUpdate += UpdatePoints;
            stats.OnPointsUpdate += UpdatePlank;
            player.OnPlayerHit += UpdateHealth;
            Waiting.OnWaitingEnter += EnableTimer;
            Waiting.OnWaitingExit += DisableTimer;
            upgrader.OnUpgrade += UpdateHealth;
            upgrader.OnUpgrade += UpdatePlank;
            upgrader.OnUpgrade += UpdatePoints;

        }
        public override void _ExitTree()
        {
            stats.OnPointsUpdate -= UpdatePoints;
            player.OnPlayerHit -= UpdateHealth;
            Waiting.OnWaitingEnter -= EnableTimer;
            Waiting.OnWaitingExit -= DisableTimer;
            upgrader.OnUpgrade -= UpdateHealth;
            upgrader.OnUpgrade -= UpdatePlank;
            upgrader.OnUpgrade -= UpdatePoints;
            stats.OnPointsUpdate -= UpdatePlank;
        }

        public override void _Process(double delta)
        {
            if (timeLabel.Visible)
            {
                timeLabel.Text = Mathf.RoundToInt(timer.TimeLeft).ToString();
            }
        }
    }
}
