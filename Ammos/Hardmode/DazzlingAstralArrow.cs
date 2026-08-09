using CalamityAmmo.Projectiles.Hardmode;
using CalamityMod;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Ammos.Hardmode
{
	public class DazzlingAstralArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dazzling Astral Arrow");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "Dazzling Star Arrow");
			// Tooltip.SetDefault(IsChinese() ? "The stars are aligned!\nAre you sure?" : "Look to the skies , The stars align");
			////Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "Upgrades after defeating the Platinum Starship\nUpgrades again after defeating the Star God Dragon");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Ослепительная Астральная Стрела");
			//Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "БЗвезды расставлены!\n вы уверены?");
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
			Item.shoot = ModContent.ProjectileType<_DazzlingAstralArrow>();
			Item.shootSpeed = 5f;
			Item.ArmorPenetration = 5;
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
