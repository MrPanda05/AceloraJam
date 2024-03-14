using Commons.FiniteStateMachine;
using Godot;
using System;

namespace RpgPart.RoundManagerr.States
{
    public partial class Wave : State
    {
        //Wave state
        private RoundManager roundManager;

        public override void Readys()
        {
            roundManager = GetParent().GetParent() as RoundManager;
        }
        public override void Enter()
        {
            roundManager.canSpawn = true;
        }
        public override void Exit()
        {
            roundManager.canSpawn = false;
            roundManager.roundCount++;
            
        }
        public override void FixUpdate(float delta)
        {
            if (roundManager.enemysKilled == roundManager.maxEnemys)
            {
                FSM.TransitioToState("Waiting");
            }
        }
    }
}
