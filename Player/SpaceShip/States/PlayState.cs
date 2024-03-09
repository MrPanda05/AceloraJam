using Commons.FiniteStateMachine;
using Godot;
using RocksObstacles;
using SingleToons;
using System;

namespace Player.SpaceShip.States
{
	public partial class PlayState : State
	{
		private SpaceShip playerShip;
        private Vector2 vecInput;
        public void OnHitboxComponentAreaEntered(Area2D area)
        {
            if (area.GetParent() is HealItem)
            {
                GD.Print("heal");
                playerShip.hitbox.healthComponent.health += 1;
                if(playerShip.hitbox.healthComponent.health > 3)
                {
                    playerShip.hitbox.healthComponent.health = 3;
                } 
                SpaceShip.OnPlayerHealthUpdate?.Invoke();
                return;
            }
            playerShip.hitbox.Damage();
            SpaceShip.OnPlayerHealthUpdate?.Invoke();
            if(playerShip.hitbox.healthComponent.health == 0)
            {
                Node2D newParticle = playerShip.particleScene.Instantiate() as Node2D;
                GetParent().AddChild(newParticle);
                newParticle.Position = playerShip.Position;
                GD.Print("DYING");
                playerShip.OnPlayerDeath?.Invoke();
                playerShip.playerFSM.TransitioToState("PlayerDeath");
                AudioManager.Instance.PlayDeathSounds();
                return;
            }
            playerShip.soundPoolHit.PlayRandomSound();
        }

        public override void Readys()
        {
            playerShip = GetParent().GetParent() as SpaceShip;
        }
        public override void FixUpdate(float delta)
        {
            vecInput = Input.GetVector("Left", "Right", "Up", "Down").Normalized();
            playerShip.Move(vecInput);
            if (Input.IsActionPressed("Shoot"))
            {
                playerShip.Shoot();
            }
        }

    }
}
