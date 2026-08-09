using CalamityMod;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace CalamityAmmo.Projectiles.Post_MoonLord
{

	public class _GoldenFeatherArrow : ModProjectile
	{
		// Token: 0x06002DBA RID: 11706 RVA: 0x00178D40 File Offset: 0x00176F40
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("GoldenFeather Arrow");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Золотого пера Стрела");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "Golden Feather Arrow");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x00178D78 File Offset: 0x00176F78
		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			Projectile.light = 0f;
			Projectile.extraUpdates = 3;
			Projectile.arrow = true;
			Projectile.aiStyle = 1;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
			SoundEngine.PlaySound(SoundID.Item8, new Vector2?(Projectile.position));
			return true;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Projectile.type], lightColor, 1, null, true);
			return false;
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (!hit.Crit)
			{
				Projectile.NewProjectile(Projectile.GetSource_FromThis(),
				  target.Center + new Vector2(Main.rand.Next(-4, 4), -400),
				  new Vector2(0, 10), ModContent.ProjectileType<_RedLightning>(), Projectile.damage, Projectile.knockBack,
				  Main.myPlayer);
			}


		}
		public override void OnSpawn(IEntitySource source)
		{

			base.OnSpawn(source);
		}
		public override bool PreAI()
		{

			Projectile.ai[0]++;
			Projectile.rotation = Utils.ToRotation(Projectile.velocity) + MathHelper.ToRadians(90f);
			Projectile.spriteDirection = Projectile.direction;
			if (Projectile.ai[0] == 45 && Main.rand.NextBool(5))
			{
				int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(Main.rand.Next(-300, 300), Main.rand.Next(-300, 300)), Projectile.velocity, ModContent.ProjectileType<MiniatureFolly>(), (int)(Projectile.damage * 0.5f), Projectile.knockBack, Main.myPlayer);
				Main.projectile[proj].tileCollide = false;
				Main.projectile[proj].netUpdate = true;
			}
			return false;
		}
		public override void OnKill(int timeLeft)
		{

			
		}

	}
}





