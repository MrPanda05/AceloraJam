using Godot;
using System;


namespace Particles
{
    public partial class HitParticle : Node2D
    {
        [Export]private CpuParticles2D particle;

        public async void EnableParticle()
        {
            particle.Emitting = true;
            await ToSignal(GetTree().CreateTimer(1.5f), SceneTreeTimer.SignalName.Timeout);
            QueueFree();
        }
        public override void _Ready()
        {
            EnableParticle();
            GD.Print("particles");
        }
    }
}
