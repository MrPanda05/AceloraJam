using Godot;
using Player.SpaceShip;
using System;

namespace UIgame
{
	public partial class Health : Control
	{

		private HSlider slider;

        [Export] private SpaceShip player;
        public void UpdateHealth()
        {
            slider.Value = player.hitbox.healthComponent.health;
        }
        public override void _Ready()
        {
            slider = GetNode<HSlider>("HSlider");
            SpaceShip.OnPlayerHealthUpdate += UpdateHealth;
            player.OnRestartGame += UpdateHealth;
        }
        public override void _ExitTree()
        {
            SpaceShip.OnPlayerHealthUpdate -= UpdateHealth;
            player.OnRestartGame -= UpdateHealth;

        }
    }
}
