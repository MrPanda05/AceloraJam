using Commons.FiniteStateMachine;
using Godot;
using RpgPart.Playerer;
using System;

namespace RpgPart.Enemies.States
{
    public partial class Follow : State
    {

        private PlayerRPG player;

        private Enemy myself;

        public override void Readys()
        {
            player = GetTree().GetFirstNodeInGroup("Player") as PlayerRPG;
            myself = GetParent().GetParent() as Enemy;
        }
        private void MakePath()
        {
            myself.navigationAgent.TargetPosition = player.GlobalPosition;
        }
        public void OnTimerPathTimeout()
        {
            MakePath();
        }
        public override void FixUpdate(float delta)
        {
            //myself.Velocity = myself.Position.DirectionTo(player.Position) * myself.speed;
            ////myself.LookAt(player.Position);
            Vector2 dir = myself.ToLocal(myself.navigationAgent.GetNextPathPosition()).Normalized();
            myself.Velocity = dir * myself.speed;
            myself.MoveAndSlide();

        }

    }
}
