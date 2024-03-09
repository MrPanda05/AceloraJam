using Commons.FiniteStateMachine;
using Godot;
using System;


namespace Player.SpaceShip.States
{
	public partial class PlayerDeath : State
	{
        private SpaceShip playerShip;
        public override void Readys()
        {
            playerShip = GetParent().GetParent() as SpaceShip;
        }
        public override void Enter()
        {
            GD.Print("eNTER up");
        }
        public override void FixUpdate(float delta)
        {
            if(Input.IsActionJustPressed("Restart"))
            {
                GD.Print("Revitge");
                playerShip.Revive();
                playerShip.OnRestartGame?.Invoke();
                FSM.TransitioToState("PlayState");
            }
            if (Input.IsActionJustPressed("Up"))
            {
                GD.Print("test up");
            }
        }
    }
}
