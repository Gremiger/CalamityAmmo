using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Ammo;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Ammos.Post_MoonLord
{
	public class DivineBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}
		public override void SetDefaults()
		{
			Item.damage = 24;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.knockBack = 2.5f;
			Item.value = Item.buyPrice(0, 0, 3, 0);
			Item.rare = ItemRarityID.Purple;
			Item.shoot = ModContent.ProjectileType<DivineBullet_Proj>();
			Item.shootSpeed = 4f;
			Item.ArmorPenetration = 5;
			Item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(100);
			recipe.AddIngredient(ModContent.ItemType<DivineGeode>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HolyFireBullet>(), 100);
			recipe.AddTile(ModContent.TileType<ProfanedCrucible>());
			recipe.Register();
		}


	}
	public class DivineBullet_Proj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 8;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
		}

		public override void SetDefaults()
		{
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.MaxUpdates = 5;
			Projectile.timeLeft = 600;
			
		}

		public override void AI()
		{
			Projectile.rotation = Utils.ToRotation(Projectile.velocity) + MathHelper.ToRadians(90f);
			Projectile.spriteDirection = Projectile.direction;
			Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 4f && Utils.NextBool(Main.rand))
			{
				float scale = Utils.NextFloat(Main.rand, 0.6f, 1.6f);
				int dustID = Dust.NewDust(Projectile.Center, 1, 1, 244, 0f, 0f, 0, default(Color), 1f);
				Main.dust[dustID].position = Projectile.Center;
				Main.dust[dustID].noGravity = true;
				Main.dust[dustID].scale = scale;
				float angleDeviation = 0.17f;
				float angle = Utils.NextFloat(Main.rand, -angleDeviation, angleDeviation);
				Vector2 sprayVelocity = Utils.RotatedBy(Projectile.velocity, (double)angle, default(Vector2)) * 0.6f;
				Main.dust[dustID].velocity = sprayVelocity;
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(DivineBullet_Proj.Alpha);
		}

		public override bool PreDraw(ref Color lightColor)
		{
			CalamityUtils.DrawAfterimagesFromEdge(Projectile, 0, lightColor, null);
			return false;
		}

		public override void OnKill(int timeLeft)
		{
			if (Projectile.owner == Main.myPlayer)
			{
				int blastDamage = (int)(Projectile.damage * 0.5f);
				float scale = 1f + Utils.NextFloat(Main.rand) * 1.15f;
				int boom = Projectile.NewProjectile(Projectile.GetSource_FromThis(null), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<FuckYou>(), blastDamage, Projectile.knockBack, Projectile.owner, 0f, scale);
				if (boom.WithinBounds(1000) && Projectile.CountsAsClass<RangedDamageClass>())
				{
					Main.projectile[boom].DamageType = DamageClass.Ranged;
				}
			}
			for (int i = 0; i < 4; i++)
			{
				float scale2 = Utils.NextFloat(Main.rand, 1.4f, 1.8f);
				int dustID = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 244, 0f, 0f, 0, default(Color), 1f);
				Main.dust[dustID].noGravity = false;
				Main.dust[dustID].scale = scale2;
				float angleDeviation = 0.25f;
				float angle = Utils.NextFloat(Main.rand, -angleDeviation, angleDeviation);
				float velMult = Utils.NextFloat(Main.rand, 0.08f, 0.14f);
				Vector2 shrapnelVelocity = Utils.RotatedBy(Projectile.oldVelocity, (double)angle, default(Vector2)) * velMult;
				Main.dust[dustID].velocity = shrapnelVelocity;
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(ModContent.BuffType<HolyFlames>(), 300, false);
		}




		private const int Lifetime = 600;
		private static readonly Color Alpha = new Color(1f, 1f, 1f, 0f);
	}
}
