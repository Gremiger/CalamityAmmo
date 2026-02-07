using CalamityMod;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles
{
	public class DeathMarkRound : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "CalamityMod/Projectiles/LaserProj";
			}
		}
		public override void SetStaticDefaults()
		{
		}
		public override void SetDefaults()
		{
			Projectile.width = 5;
			Projectile.height = 5;
			Projectile.friendly = true;
			Projectile.alpha = 255;
			Projectile.penetrate = 1;
			Projectile.extraUpdates = 2;
			Projectile.timeLeft = 300;
			Projectile.DamageType = DamageClass.Ranged;
		}
		public override void AI()
		{
			if (Projectile.alpha > 0)
			{
				Projectile.alpha -= 25;
			}
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			Lighting.AddLight((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16, 0.5f, 0f, 0.7f);
			float num55 = 30f;
			float num56 = 2f;
			if (Projectile.ai[1] == 0f)
			{
				Projectile.localAI[0] += num56;
				if (Projectile.localAI[0] > num55)
				{
					Projectile.localAI[0] = num55;
					return;
				}
			}
			else
			{
				Projectile.localAI[0] -= num56;
				if (Projectile.localAI[0] <= 0f)
				{
					Projectile.Kill();
				}
			}
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(new Color(200, 0, 250, 0));
		}
		public override bool PreDraw(ref Color lightColor)
		{
			return Projectile.DrawBeam(30f, 2f, lightColor, null);
		}
		public override void OnKill(int timeLeft)
		{
			int dustAmt = Main.rand.Next(3, 7);
			for (int d = 0; d < dustAmt; d++)
			{
				int purple = Dust.NewDust(Projectile.Center - Projectile.velocity / 2f, 0, 0, 173, 0f, 0f, 100, default(Color), 2.1f);
				Main.dust[purple].velocity *= 2f;
				Main.dust[purple].noGravity = true;
			}
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(ModContent.BuffType<MarkedforDeath>(), 360, false);
		}
		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			target.AddBuff(ModContent.BuffType<MarkedforDeath>(), 360, true);
		}
	}
}
