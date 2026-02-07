using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles.Hardmode
{
	public class _FossilShard : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("化石碎片");
		}

		public override void SetDefaults()
		{
			Projectile.width = 6;
			Projectile.height = 6;
			Projectile.aiStyle = 24;
			Projectile.friendly = true;
			Projectile.penetrate = 2;
			Projectile.alpha = 50;
			Projectile.timeLeft = 600;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.scale = 1f;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			return true;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 60, false);
			target.AddBuff(ModContent.BuffType<SulphuricPoisoning>(), 90, false);
		}
		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 60, true, false);
			target.AddBuff(ModContent.BuffType<SulphuricPoisoning>(), 90, true, false);
		}
	}
	public class _FossilShard2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("化石碎片");
		}

		public override void SetDefaults()
		{
			Projectile.width = 6;
			Projectile.height = 6;
			Projectile.aiStyle = 24;
			Projectile.friendly = true;
			Projectile.penetrate = 1;
			Projectile.alpha = 50;
			Projectile.timeLeft = 600;
			Projectile.DamageType = DamageClass.Ranged;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			if (base.Projectile.ai[0] == 1f)
			{
				Texture2D texture = ModContent.Request<Texture2D>("CalamityAmmo/Projectiles/Hardmode/_FossilShard2", (ReLogic.Content.AssetRequestMode)2).Value;
				Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2(texture.Width / 2f, texture.Height / 2f), base.Projectile.scale, 0, 0f);
				return false;
			}
			if (base.Projectile.ai[0] == 2f)
			{
				Texture2D texture2 = ModContent.Request<Texture2D>("CalamityAmmo/Projectiles/Hardmode/_FossilShard", (ReLogic.Content.AssetRequestMode)2).Value;
				Main.spriteBatch.Draw(texture2, base.Projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, 0, texture2.Width, texture2.Height)), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2(texture2.Width / 2f, texture2.Height / 2f), base.Projectile.scale, 0, 0f);
				return false;
			}
			return true;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (Main.rand.NextBool(2))
			{
				target.AddBuff(ModContent.BuffType<Irradiated>(), 90, false);
				target.AddBuff(70, 60, false);
			}

		}
		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			if (Main.rand.NextBool(2))
			{
				target.AddBuff(ModContent.BuffType<Irradiated>(), 90, false);
				target.AddBuff(70, 60, false);
			}
		}

	}
}
