using CalamityMod.Items.Accessories.Wings;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Accessories
{
	public class ScorchingCoil : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 32;
			Item.value = Item.buyPrice(0, 30, 0, 0);
			Item.rare = ItemRarityID.Pink;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
			modplayer.hotCoil = true;
			player.GetCritChance(DamageClass.Ranged) += 7f;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<ModifiedCoil>());
			recipe.AddIngredient(ItemID.HellstoneBar,5);
			recipe.AddIngredient(ModContent.ItemType<UnholyCore>(), 5);
			recipe.AddTile(TileID.Hellforge);
			recipe.Register();
		}
	}
}

