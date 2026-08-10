using CalamityAmmo.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Ammos.Pre_Hardmode
{
	public class PrittyRounds : ModItem
	{
		public override void SetStaticDefaults()
		{
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}

		public override void SetDefaults()
		{
			Item.damage = 7;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.knockBack = 1f;
			Item.value = Item.buyPrice(0, 0, 0, 60);
			Item.rare = ItemRarityID.Orange;
			Item.shoot = ModContent.ProjectileType<PrittyRoundProj>();
			Item.shootSpeed = 6f;
			Item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			CreateRecipe(50)
				.AddIngredient(ItemID.EmptyBullet, 50)
				.AddIngredient(ItemID.Bottle, 1)
				.AddIngredient(ItemID.Gel, 5)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}
