using Commons.FiniteStateMachine;
using Godot;
using System;

namespace RpgPart.States
{
    public partial class PauseState : State
    {
        public static Action OnPauseEnter, OnPauseExit;

        public override void Readys()
        {
            base.Readys();
        }
        public override void Enter()
        {
            GetTree().Paused = true;
            OnPauseEnter?.Invoke();
        }

        public override void Exit()
        {

            GetTree().Paused = false;
            OnPauseExit?.Invoke();
        }
        public override void FixUpdate(float delta)
        {
            if (Input.IsActionJustPressed("Menu"))
            {
                FSM.TransitioToState("Neutral");
            }
        }
    }
}
