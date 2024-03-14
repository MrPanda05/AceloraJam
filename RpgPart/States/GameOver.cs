using Commons.FiniteStateMachine;
using Godot;
using System;

namespace RpgPart.States
{
    public partial class GameOver : State
    {
        public static Action OnGameOverEnter, OnGameOverExit;

        public static Action RestartGame;
        private void Restart()
        {
            FSM.TransitioToState("Neutral");
        }
        
        public override void Enter()
        {
            RestartGame += Restart;
            GetTree().Paused = true;
            OnGameOverEnter?.Invoke();
        }

        public override void Exit()
        {
            RestartGame -= Restart;
            GetTree().Paused = false;
            OnGameOverExit?.Invoke();
        }
    }
}
