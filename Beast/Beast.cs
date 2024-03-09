using Godot;
using System;
using TheSpaceGame;
namespace BeastOfRevelations 
{ 
    public partial class Beast : Node2D
    {
        [Export] Marker2D pointA, pointB;

        public bool active = false;

        private float Time = 0;


        public async void EnableMe()
        {
            await ToSignal(GetTree().CreateTimer(8.5f), SceneTreeTimer.SignalName.Timeout);
            active = true;
        }
        public override void _Ready()
        {
            SpaceGame.OnBeastArrive += EnableMe;
        }
        public override void _PhysicsProcess(double delta)
        {
            if(!active) return;
            Time += (float) delta * 0.004f;


            Position = Position.Lerp(pointB.Position, Time);
        }
        public override void _EnterTree()
        {
            SpaceGame.OnBeastArrive -= EnableMe;
        }
    }
}
