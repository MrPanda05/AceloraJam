using Commons.FiniteStateMachine;
using Godot;
using RpgPart.Playerer;
using System;

namespace RpgPart.States
{
    public partial class Neutral : State
    {
        private PlayerRPG player;

        public override void Readys()
        {
            player = GetTree().GetFirstNodeInGroup("Player") as PlayerRPG;
        }
        public override void Enter()
        {
            PlayerRPG.OnPlayerDeath += SetGameOver;
        }
        public override void Exit()
        {
            PlayerRPG.OnPlayerDeath -= SetGameOver;//May change
        }

        private void SetGameOver()
        {
            FSM.TransitioToState("GameOver");
        }
        public override void FixUpdate(float delta)
        {
            if (Input.IsActionJustPressed("Menu"))
            {
                FSM.TransitioToState("PauseState");
            }
        }
    }
}
