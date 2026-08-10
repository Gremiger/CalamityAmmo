using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles
{
	public class FizzySplash : ModProjectile
	{
		public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

		public override void SetDefaults()
		{
			Projectile.width = 64;
			Projectile.height = 64;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 10;
			Projectile.tileCollide = false;
			Projectile.knockBack = 0f;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}

		public override void AI()
		{
			if (Projectile.localAI[0] == 0f)
			{
				Projectile.localAI[0] = 1f;
				int dustType = (int)Projectile.ai[0];
				for (int i = 0; i < 12; i++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 0, default(Color), 1.2f);
					dust.velocity = Main.rand.NextVector2Circular(4f, 4f);
					dust.noGravity = true;
				}
			}
		}
	}
}
