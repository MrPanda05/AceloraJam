using Godot;
using RpgPart.Playerer;
using System;



namespace RpgPart.PlankAreas
{
    public partial class PlankableAreas : StaticBody2D
    {

        public PlayerRPG player;
        [Export] public int myID = -1;
        [Export] public int repairCost = 1;

        [Export]private CollisionShape2D coli;

        //Plankable grupo
        public void OnHitboxComponentAreaEntered(Area2D area)
        {
            player.IsOnPlankableArea = true;
            player.plancableAreaID = myID;
        }

        public void OnHitboxComponentAreaExited(Area2D area)
        {
            player.IsOnPlankableArea = false;
            player.plancableAreaID = -1;
        }
        public void Break()
        {
            coli.Disabled = true;
        }
        public void Repair(int id)
        {
            if (id != myID) return;
            if(player.planksCount < repairCost)
            {
                GD.Print("Not enough woods");
                return;
            }
            GD.Print("Reapering my part of Id " + id);
            player.planksCount -= repairCost;
            coli.Disabled = false;
        }

        public override void _Ready()
        {
            player = GetTree().GetFirstNodeInGroup("Player") as PlayerRPG;
            player.OnAreaRepair += Repair;
        }
    }
}
