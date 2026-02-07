using CalamityMod.Items.Accessories;
using CalamityMod.Items.Mounts;
using CalamityMod.Items.SummonItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using CalamityMod.Items.Placeables.Furniture;

namespace CalamityAmmo.CAESystem
{
	public class ExtraCalamityRecipe:ModSystem
	{
		public override void AddRecipes()
		{
			Recipe UnstableGraniteCore = Recipe.Create(ModContent.ItemType<UnstableGraniteCore>());
			UnstableGraniteCore.AddIngredient(ItemID.Geode, 1);
			UnstableGraniteCore.AddIngredient(ItemID.Granite, 8);
			UnstableGraniteCore.AddTile(TileID.Anvils);
			UnstableGraniteCore.Register();

			Recipe LuxorsGift = Recipe.Create(ModContent.ItemType<LuxorsGift>());
			LuxorsGift.AddIngredient(ItemID.Geode, 1);
			LuxorsGift.AddIngredient(ItemID.Ruby, 1);
			LuxorsGift.AddIngredient(ItemID.Sapphire, 1);
			LuxorsGift.AddIngredient(ItemID.Amber, 1);
			LuxorsGift.AddIngredient(ItemID.Emerald, 1);
			LuxorsGift.AddIngredient(ItemID.Amethyst, 1);
			LuxorsGift.AddTile(TileID.Anvils);
			LuxorsGift.Register();

			Recipe TrinketofChi = Recipe.Create(ModContent.ItemType<TrinketofChi>());
			TrinketofChi.AddRecipeGroup(RecipeGroupID.Wood, 2);
			TrinketofChi.AddIngredient(ItemID.RedDye, 1);
			TrinketofChi.AddTile(TileID.HeavyWorkBench);
			TrinketofChi.Register();

			Recipe TundraLeash = Recipe.Create(ModContent.ItemType<TundraLeash>());
			TundraLeash.AddIngredient(ItemID.Chain, 1);
			TundraLeash.AddIngredient(ItemID.FlinxFur, 2);
			TundraLeash.AddTile(TileID.WorkBenches);
			TundraLeash.Register();

			Recipe OnyxExcavatorKey = Recipe.Create(ModContent.ItemType<OnyxExcavatorKey>());
			OnyxExcavatorKey.AddIngredient(ItemID.ShadowKey, 1);
			OnyxExcavatorKey.AddIngredient(ItemID.Obsidian, 5);
			OnyxExcavatorKey.AddIngredient(ItemID.DemonTorch, 1);
			OnyxExcavatorKey.AddTile(TileID.WorkBenches);
			OnyxExcavatorKey.Register();

			Recipe GladiatorsLocket = Recipe.Create(ModContent.ItemType<GladiatorsLocket>());
			GladiatorsLocket.AddIngredient(ItemID.GoldWatch, 1);
			GladiatorsLocket.AddIngredient(ItemID.Gladius, 2);
			GladiatorsLocket.AddTile(TileID.Anvils);
			GladiatorsLocket.Register();

			Recipe FungalSymbiote = Recipe.Create(ModContent.ItemType<FungalSymbiote>());
			FungalSymbiote.AddIngredient(ItemID.GlowingMushroom, 50);
			FungalSymbiote.AddTile(TileID.TinkerersWorkbench);
			FungalSymbiote.Register();

			Recipe CorruptionEffigy = Recipe.Create(ModContent.ItemType<CorruptionEffigy>());
			CorruptionEffigy.AddIngredient(ItemID.EbonstoneBlock, 50);
			CorruptionEffigy.AddTile(TileID.DemonAltar);
			CorruptionEffigy.Register();

			Recipe CrimsonEffigy = Recipe.Create(ModContent.ItemType<CrimsonEffigy>());
			CrimsonEffigy.AddIngredient(ItemID.CrimstoneBlock, 50);
			CrimsonEffigy.AddTile(TileID.DemonAltar);
			CrimsonEffigy.Register();

			Recipe Terminus = Recipe.Create(ModContent.ItemType<Terminus>());
			Terminus.AddIngredient(ItemID.StoneBlock, 49);
			Terminus.Register();
		}
		public override void PostSetupContent()
		{
			

			/**/
		}
	}
}
