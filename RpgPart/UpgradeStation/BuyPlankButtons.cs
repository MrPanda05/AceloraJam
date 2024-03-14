using Godot;
using System;

namespace RpgPart.Playerer.UpgradeStation
{
    public partial class BuyPlankButtons : BuyButton
    {
        //[Export] protected PanelShop myPanel;
        //[Export] protected ShopUi shopUI;
        public override void _Ready()
        {
            upgrader = GetTree().GetFirstNodeInGroup("Upgrader") as Upgrader;
        }
        public void OnButtonDown()
        {
            if (shopUI.stats.points < myPanel.myCost)
            {
                return;
            }
            shopUI.stats.points -= myPanel.myCost;
            shopUI.UpdatePoints();
            upgrader.BuyPlanks();
        }

    }
}
