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
using CalamityAmmo.Projectiles;
using CalamityMod.Projectiles.Ranged;
using CalamityAmmo.Projectiles.Post_MoonLord;

namespace CalamityAmmo.Ammos.Post_MoonLord
{
	public class ElementalArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}
		public override void SetDefaults()
		{
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.knockBack = 3f;
			Item.value = Item.buyPrice(0, 0, 0, 35);
			Item.rare = ItemRarityID.Purple;
			Item.shoot = ModContent.ProjectileType<_ElementalArrow>();
			Item.shootSpeed = 5.5f;
			Item.ammo = AmmoID.Arrow;
			Item.damage = 16;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(333);
			recipe.AddIngredient(ItemID.LunarBar, 1);
			recipe.AddIngredient(ItemID.FragmentSolar, 1);
			recipe.AddIngredient(ItemID.FragmentVortex, 1);
			recipe.AddIngredient(ItemID.FragmentNebula, 1);
			recipe.AddIngredient(ItemID.FragmentStardust, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.Register();
		}

	}
}

