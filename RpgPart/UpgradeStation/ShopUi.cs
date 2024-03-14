using Godot;
using System;

namespace RpgPart.Playerer.UpgradeStation
{
	public partial class ShopUi : Control
	{
		public RpgPartStatsHolder stats;

		private Label pointsLabel;

		public void UpdatePoints()
		{
            pointsLabel.Text = $"You got {stats.points} points";
        }

		public override void _Ready()
		{
			stats = GetTree().GetFirstNodeInGroup("GlobalStats") as RpgPartStatsHolder;
			pointsLabel = GetNode<Label>("PointsLabel");
			pointsLabel.Text = $"You got {stats.points} points";
			stats.OnPointsUpdate += UpdatePoints;
        }
        public override void _ExitTree()
        {
            stats.OnPointsUpdate -= UpdatePoints;
        }

        public void OnCloseButtoButtonDown()
		{
			Visible = false;
		}


    }
}
