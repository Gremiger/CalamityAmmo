using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles.Hardmode
{

	public class _WeakAstralArrow : ModProjectile
	{
		// Token: 0x06002DBA RID: 11706 RVA: 0x00178D40 File Offset: 0x00176F40
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Astral Bullet");
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x00178D78 File Offset: 0x00176F78
		public override void SetDefaults()
		{
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 300;
			Projectile.light = 0f;
			Projectile.arrow = true;
			//Projectile.aiStyle = -1;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
			SoundEngine.PlaySound(SoundID.Item8, new Vector2?(Projectile.position));
			return true;
		}


		public override void OnSpawn(IEntitySource source)
		{
		}
		public override void AI()
		{
			Projectile.ai[0]++;
			Projectile.rotation = Utils.ToRotation(Projectile.velocity) + MathHelper.ToRadians(90f);
			Projectile.spriteDirection = Projectile.direction;
			Projectile.localAI[0] += 1f;
			if (Projectile.localAI[0] > 4f && Utils.NextBool(Main.rand, 2))
			{
				int randomDust = Utils.SelectRandom<int>(Main.rand, new int[]
				{
					ModContent.DustType<AstralOrange>(),
					ModContent.DustType<AstralBlue>()
				});

				int astral = Dust.NewDust(Projectile.position, 6, 6, randomDust, 0f, 0f, 0, default, 1f);
				Dust.NewDust(Projectile.position, 6, 6, randomDust, 0f, 0f, 0, default, 1f);
				Dust.NewDust(Projectile.position, 6, 6, randomDust, 0f, 0f, 0, default, 1f);
				Main.dust[astral].alpha = Projectile.alpha;
				Vector2 Normal = Projectile.velocity.RotatedBy(Math.PI / 2);
				Normal.Normalize();
				Main.dust[astral].velocity += Normal * (float)Math.Sin(Projectile.ai[0]);
				Main.dust[astral].noGravity = true;
			}
		}
		


	}
}




