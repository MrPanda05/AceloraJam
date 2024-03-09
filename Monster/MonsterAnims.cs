using Godot;
using System;


namespace Monsters
{
    public partial class MonsterAnims : AnimatedSprite2D
    {
        public override void _Ready()
        {
            uint n = GD.Randi() % 2;
            switch (n)
            {
                case 0:
                    Play("YellowMonster");
                    break;
                case 1:
                    Play("RedMonster");
                    break;
            }
        }
    }
}
