using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Tiles.FurnitureStatigel;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Accessories
{
	public class TransformerCoil : ModItem
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
			modplayer.electricCoil = true;
			player.buffImmune[BuffID.Electrified] = true;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<ModifiedCoil>());
			recipe.AddIngredient(ModContent.ItemType<TheTransformer>(), 1);
			recipe.AddIngredient(ModContent.ItemType<StormlionMandible>(), 2);
			recipe.AddTile(ModContent.TileType<StaticRefiner>());
			recipe.Register();
		}
	}
}


