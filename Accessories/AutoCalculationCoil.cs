using System.Collections.Generic;
using CalamityAmmo.CAESystem;
using CalamityMod;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Accessories
{
	public class AutoCalculationCoil : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 1;
		}
		public override void ModifyTooltips(List<TooltipLine> list) => list.IntegrateHotkey(CAEKeybinds.autoCoilKey);
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
			modplayer.autoCoil = true;
			//player.GetCritChance(DamageClass.Ranged) += 7f;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<ModifiedCoil>());
			recipe.AddIngredient(ModContent.ItemType<PlasmaDriveCore>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SuspiciousScrap>(), 3);
			recipe.AddIngredient(ModContent.ItemType<MysteriousCircuitry>(), 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}

