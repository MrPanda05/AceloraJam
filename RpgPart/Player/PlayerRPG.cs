using Commons.Components;
using Commons.FiniteStateMachine;
using Godot;
using RpgPart.States;
using System;

namespace RpgPart.Playerer
{
	public partial class PlayerRPG : CharacterBody2D
	{
		[ExportGroup("Player needs")]
		[Export] public float speed { get; set; } = 300f;
		[Export] public HitboxComponent hitbox { get; set; }
        [Export] public HealthComponent health { get; set; }
		[Export] private Fsm playerFSM { get; set; }
        [ExportGroup("Planks")]
        [Export] public int maxPlanks = 5, planksCount = 0;

        public Action<int> OnAreaRepair;
        public Action OnPlayerHit;
        public static Action OnPlayerDeath;

        private AnimatedSprite2D anim;

		private Vector2 vel;

        public bool IsOnPlankableArea;

        public int plancableAreaID;

        public void Reset()
        {
            GlobalPosition = new Vector2(972, 586);
            maxPlanks = 5;
            planksCount = 0;
            health.health = 5;
            health.maxHealth = 5;
        }
        public void Move(Vector2 input)
        {
            if(input == Vector2.Zero)
            {
                anim.Play("Idle");
            }
            else
            {
                anim.Play("Walking");
            }
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
            anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        }


    }
}
