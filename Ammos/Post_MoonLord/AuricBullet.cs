using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Ammo;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Ammos.Post_MoonLord
{
	public class AuricBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}
		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.knockBack = 2.5f;
			Item.value = Item.buyPrice(0, 0, 20, 0);
			Item.rare = ItemRarityID.Purple;
			Item.shoot = ModContent.ProjectileType<CalamityMod.Projectiles.Ranged.AuricBullet>();
			Item.shootSpeed = 4f;
			Item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(333);
			recipe.AddIngredient(ModContent.ItemType<AuricBar>(), 1);
			recipe.AddTile(ModContent.TileType<CosmicAnvil>());
			recipe.Register();
		}


	}
	
}
