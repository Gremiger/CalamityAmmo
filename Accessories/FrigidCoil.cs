using System;

using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using System.Text;
using Terraria.ModLoader;
using ReLogic.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using ReLogic.Content;
using Terraria.GameContent;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Items.Materials;
using CalamityAmmo.Projectiles;
using CalamityMod.Items.Placeables;
using Terraria.Audio;
using CalamityMod.Items;
using CalamityMod;
using CalamityMod.Items.Accessories.Wings;

namespace CalamityAmmo.Accessories
{
	public class FrigidCoil : ModItem
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
			modplayer.icyCoil = true;
			player.GetAttackSpeed(DamageClass.Ranged) *= 0.9f;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<ModifiedCoil>());
			recipe.AddIngredient(ModContent.ItemType<SoulofCryogen>());
			recipe.AddIngredient(ModContent.ItemType<EssenceofEleum>(), 5);
			recipe.AddTile(TileID.IceMachine);
			recipe.Register();
		}
	}
}

