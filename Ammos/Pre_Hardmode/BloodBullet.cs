using CalamityAmmo.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Ammos.Pre_Hardmode
{
	public class BloodBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}
		public override void SetDefaults()
		{
			Item.damage = 5;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.knockBack = 1.5f;
			Item.value = Item.buyPrice(0, 0, 3, 0);
			Item.rare = 3;
			Item.shoot = ModContent.ProjectileType<_BloodBullet>();
			Item.shootSpeed = 4f;
			Item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
			/*Recipe recipe = CreateRecipe(100);
            recipe.AddIngredient(ModContent.ItemType<BloodOrb>(), 8);
            recipe.AddIngredient(ItemID.MusketBall, 100);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();*/
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			return true;
		}
	}
}
