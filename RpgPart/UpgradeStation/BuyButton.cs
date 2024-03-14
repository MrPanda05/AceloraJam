using Godot;
using System;

namespace RpgPart.Playerer.UpgradeStation
{
	public partial class BuyButton : Button
	{
		[Export] protected PanelShop myPanel;
		[Export] protected ShopUi shopUI;
		protected Upgrader upgrader;
    }
}
