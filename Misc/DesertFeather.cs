using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityAmmo.Misc
{
	public class DesertFeather : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 5;
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.maxStack = 9999;
			Item.value = Item.sellPrice(0, 0, 0, 20);
			Item.rare = ItemRarityID.Blue;
		}
	}
}
