using Godot;
using Player.SpaceShip;
using SingleToons;
using Spawners;
using System;

namespace TheSpaceGame
{
    public partial class SpaceGame : Node2D
    {
        [ExportGroup("Spawners")]
        [Export] private MonsterSpawner monsterSpawner;
        [Export] private RocksSpawner rockSpawner;
        [Export] private HealingSpawner healSpawner;
        [Export] private CanvasLayer canvasLayer;
        [Export] private ColorRect backGround;
        [Export] private PackedScene particleEvadeFreeze;
        [Export] private Label restartLabel;


        private SpaceShip player;

        private Timer timerBeast, timerWait;

        private Label timerLabel;

        public bool invokeBeast = true;
        private bool isPlayerDeath = false;

        public static Action OnBeastArrive;



        public void StopBeast()
        {
            invokeBeast = false;
            timerBeast.Stop();
        }

        public async void FadeToBlack()
        {
            while(backGround.Color.A <= 1)
            {
                backGround.Color += new Color(0, 0, 0, 0.005f);
                await ToSignal(GetTree().CreateTimer(0.003f), SceneTreeTimer.SignalName.Timeout);
            }
            await ToSignal(GetTree().CreateTimer(1.5f), SceneTreeTimer.SignalName.Timeout);
            GetTree().ChangeSceneToFile("res://Beast/BeastMonologue.tscn");
        }
        public void OnDeath()
        {
            isPlayerDeath = true;
            double timer = Mathf.Round(timerBeast.TimeLeft);
            GD.Print(timer);
            StopBeast();
            timerLabel.Text = timer.ToString();
            restartLabel.Visible = true;
        }
        public void RestartGame()
        {
            isPlayerDeath = false;
            timerBeast.WaitTime = 200f;//default 200
            timerBeast.Start();
            invokeBeast = true;
            restartLabel.Visible = false;
        }
        public void OnTimerTimeout()
        {
            if (!invokeBeast) return;
            OnBeastArrive?.Invoke();
            canvasLayer.Visible = false;
            timerWait.Start();
            player.hitbox.healthComponent.health += 100;
            player.playerFSM.ForceNullState();
            monsterSpawner.working = false;
            rockSpawner.working = false;
            healSpawner.working = false;
            GD.PrintErr("The beast is here");
        }

        public void OnTimerWaitTimeout()
        {
            GD.Print("bruh");
            FadeToBlack();
        }
        public override void _Ready()
        {
            SaveSystem.Add("hasSeenTheBeast", 0);
            GetTree().Paused = true;
            player = GetNode<SpaceShip>("SpaceShip");
            timerBeast = GetNode<Timer>("TimerOfTheBeast");
            timerWait = GetNode<Timer>("TimerWait");
            timerLabel = GetNode<Label>("CanvasLayer/Label");
            timerLabel.Text = Mathf.Round(timerBeast.TimeLeft).ToString();
            player.OnPlayerDeath += OnDeath;
            player.OnRestartGame += RestartGame;
            Node2D newParticle = particleEvadeFreeze.Instantiate() as Node2D;
            AddChild(newParticle);
            newParticle.Position = new Vector2(-200, 0);
        }
        public override void _ExitTree()
        {
            player.OnPlayerDeath -= OnDeath;
            player.OnRestartGame -= RestartGame;
        }



        public override void _PhysicsProcess(double delta)
        {
            if (isPlayerDeath) return;
            timerLabel.Text = Mathf.Round(timerBeast.TimeLeft).ToString();
        }
    
    }
}
