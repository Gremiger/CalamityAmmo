using CalamityAmmo.Projectiles.Hardmode;
using CalamityMod;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Ammos.Hardmode
{
	public class DazzlingAstralArrowNew : ModItem
	{
		public override void SetStaticDefaults()
		{
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}
		public override void SetDefaults()
		{
			Item.damage = 15;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.knockBack = 2.5f;
			Item.value = Item.buyPrice(0, 0, 1, 0);
			Item.rare = 9;
			Item.shoot = ModContent.ProjectileType<_DazzlingAstralArrowNew>();
			Item.shootSpeed = 5f;
			Item.ammo = AmmoID.Arrow;
		}
		public override void UpdateInventory(Player player)
		{
			int i = Item.stack;
			if (!DownedBossSystem.downedAstrumDeus)
			{
				Item.SetDefaults(ModContent.ItemType<AstralArrow>(), true);
				Item.stack = i;
			}
		}
	}
}
