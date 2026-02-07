using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Rockets
{
	public class PlaguenadeLauncher : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault(" Plaguenade Launcher");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "瘟疫蜜蜂掷弹筒");
			// Tooltip.SetDefault("Fires a brittle spiky ball\nEvery four attack will shoot 6 sand blasts");
			Item.ResearchUnlockCount = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 42;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 86;
			Item.height = 34;
			Item.useTime = 55;
			Item.useAnimation = 55;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 2f;
			Item.value = Item.buyPrice(0, 60, 0, 0);
			Item.rare = ItemRarityID.Green;
			Item.UseSound = new SoundStyle?(SoundID.Item40);
			Item.autoReuse = true;
			Item.shoot = ProjectileID.Beenade;
			Item.shootSpeed = 6f;
			Item.useAmmo = ItemID.Beenade;
		}

		public override void ModifyWeaponCrit(Player player, ref float crit)
		{

		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(0f, 0f));
		}
		public override bool? UseItem(Player player)
		{

			return null;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{

			if (source.AmmoItemIdUsed == ModContent.ItemType<Plaguenade>())
			{
				int grenadeType = ModContent.ProjectileType<PlaguenadeProj>();
				Projectile.NewProjectile(source, position, velocity * 2 / 3, grenadeType, damage, knockback, player.whoAmI);
				return false;
			}
			return true;
		}
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.RocketLauncher, 1);
			recipe.AddIngredient(ModContent.ItemType<InfectedArmorPlating>(), 8);
			recipe.AddIngredient(ModContent.ItemType<PlagueCellCanister>(), 15);
			recipe.AddTile(ModContent.TileType<PlagueInfuser>());
			recipe.Register();
		}
	}
}


