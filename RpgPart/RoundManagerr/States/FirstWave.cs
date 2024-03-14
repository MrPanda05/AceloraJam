using Commons.FiniteStateMachine;
using Godot;
using System;

namespace RpgPart.RoundManagerr.States
{
    //First wave
    //Player must press enter
    public partial class FirstWave : State
    {
        private RoundManager roundManager;
        
        public override void Readys()
        {
            roundManager = GetParent().GetParent() as RoundManager;
        }

        public override void FixUpdate(float delta)
        {
            if (Input.IsActionJustPressed("Enter"))
            {
                roundManager.startLabel.Visible = false;
                FSM.TransitioToState("Wave");
            }
        }
    }
}
