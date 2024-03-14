using Godot;
using System;

namespace RpgPart.Playerer.UpgradeStation 
{
    public partial class PanelShop : Panel
	{
		[Export] private string price, name;
        private Label priceLabel, nameLabel;
        public int myCost;
        public override void _Ready()
		{
			priceLabel = GetNode<Label>("Cost");
            nameLabel = GetNode<Label>("Tittle");
            priceLabel.Text = "Points:" + price;
            nameLabel.Text = name;
            myCost = price.ToInt();
        }

        public override void _Process(double delta)
        {
            if (Engine.IsEditorHint())
            {
                priceLabel.Text = "Points:"+price;
                nameLabel.Text = name;
            }
        }


    }
}

