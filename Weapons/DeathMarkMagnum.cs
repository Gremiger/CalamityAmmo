using System;
using CalamityAmmo.Projectiles;
using CalamityMod;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Weapons
{
	public class DeathMarkMagnum : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 1;
		}
		public override void SetDefaults()
		{
			Item.damage = 40;
			Item.width = 50;
			Item.height = 24;
			Item.useTime = 22;
			Item.useAnimation = 22;
			Item.reuseDelay = 22;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Ranged;
			Item.knockBack = 3f;
			Item.value = Item.buyPrice(0, 20, 0, 0);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item41;
			Item.autoReuse = false;
			Item.shootSpeed = 16f;
			Item.crit = 5;
			Item.shoot = ModContent.ProjectileType<DeathMarkRound>();
			Item.useAmmo = AmmoID.Bullet;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-5f, 0f));
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<DeathMarkRound>(), damage, knockback, player.whoAmI, 0f, 0f);
			return false;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(1);
			recipe.AddIngredient(ItemID.Revolver, 1);
			recipe.AddIngredient(ItemID.Deathweed, 3);
			recipe.AddIngredient(ItemID.DeathweedSeeds, 6);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}
