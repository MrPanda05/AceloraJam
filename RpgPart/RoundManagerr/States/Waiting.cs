using Commons.FiniteStateMachine;
using Godot;
using System;

namespace RpgPart.RoundManagerr.States
{
    //Waiting state between waves
    public partial class Waiting : State
    {
        private RoundManager roundManager;
        private Timer timer;

        public static Action OnWaitingEnter, OnWaitingExit;

        public void OnTimerTimeout()
        {
            GD.Print("Time over");
            FSM.TransitioToState("Wave");
        }
        public override void Readys()
        {
            roundManager = GetParent().GetParent() as RoundManager;
            timer = GetNode<Timer>("Timer");
        }

        public override void Enter()
        {
            OnWaitingEnter?.Invoke();
            timer.Start();
            roundManager.IncreaseDifficulty();
            roundManager.Reset();
        }
        public override void Exit()
        {
            OnWaitingExit?.Invoke();
        }
    }
}
