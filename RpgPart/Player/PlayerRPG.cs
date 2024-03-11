using Commons.Components;
using Commons.FiniteStateMachine;
using Godot;
using System;

namespace RpgPart.Playerer
{
	public partial class PlayerRPG : CharacterBody2D
	{
		[ExportGroup("Player needs")]
		[Export] public float speed { get; set; } = 300f;
		[Export] public HitboxComponent hitbox { private get; set; }
        [Export] public HealthComponent health { private get; set; }
		[Export] private Fsm playerFSM { get; set; }
        [ExportGroup("Planks")]
        [Export] public int maxPlanks = 5, planksCount = 0;

        public Action<int> OnAreaRepair;

		private Vector2 vel;

        public bool IsOnPlankableArea;

        public int plancableAreaID;

        public void Move(Vector2 input)
        {
            vel = input * speed;
            Velocity = vel;
            MoveAndSlide();
        }

        public Vector2 GetWASDInput()
        {
            return Input.GetVector("Left", "Right", "Up", "Down").Normalized();
        }

        public void Repair()
        {
            OnAreaRepair?.Invoke(plancableAreaID);
        }

        public override void _Ready()
        {
			vel = Velocity;
        }


    }
}
