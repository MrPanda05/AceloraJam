using Godot;
using SingleToons;
using System;

public partial class FindRealPath : Node2D
{

    private int? beastSeen;

    public void FindWhatPath()
    {
        if (beastSeen == null || beastSeen == 0)
        {
            GetTree().ChangeSceneToFile("res://SpaceGame/SpaceGame.tscn");

        }
        else if (beastSeen != null && beastSeen == 1)
        {
            GetTree().ChangeSceneToFile("res://RpgPart/LeRpgTittleScreen.tscn");
        }
    }
    public override void _Ready()
    {
        beastSeen = SaveSystem.GetValue("hasSeenTheBeast");
        FindWhatPath();

    }
}
