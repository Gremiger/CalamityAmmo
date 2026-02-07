using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles.GenerateByAccessories
{
	public class BrimstoneHellblast : ModProjectile, ILocalizedModType, IModType
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 4;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
		}
		public override void SetDefaults()
		{
			Projectile.width = 40;
			Projectile.height = 40;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.penetrate = 1;
			Projectile.scale = 0.6f;
			Projectile.Opacity = 0f;
			Projectile.timeLeft = 255;
			Projectile.usesLocalNPCImmunity = true;//NPC是不是按照弹幕ID来获取无敌帧？（如果设定为true，玩家发射8个该弹幕同时击中敌人，则八个都能击中，不骗伤，原版夜明弹的反骗伤就是如此）
			Projectile.localNPCHitCooldown = 15;//上一个设定为true则被调用，NPC按照弹幕ID来获取多少无敌帧
			Projectile.usesIDStaticNPCImmunity = false;//NPC是不是按照弹幕类型来获取无敌帧？（如果设定为true，玩家发射8个该弹幕同时击中敌人，则只能击中一次，其余的会穿透，原版用它来控制喽啰的输出上限）
			Projectile.idStaticNPCHitCooldown = 10;//上一个设定为true则被调用，NPC按照弹幕类型来获取多少无敌帧
			//CooldownSlot = 1;
		}
		public override void AI()
		{
			Projectile.frameCounter++;
			Projectile.ai[0]++;
			if (Projectile.frameCounter >= 10)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame > 3)
			{
				Projectile.frame = 0;
			}
			Lighting.AddLight(Projectile.Center, 0.9f * Projectile.Opacity, 0f, 0f);
			//Main.NewText(Projectile.numHits);
			if (Projectile.Opacity < 1f) Projectile.Opacity += 0.05f;
			else if (Projectile.timeLeft < 51)//|| Projectile.numHits > 0)
			{
				Projectile.Opacity -= 0.05f;
				if (Projectile.Opacity <= 0f) Projectile.penetrate = 0;
			}
			if (Projectile.ai[1] == 0f)
			{
				Projectile.ai[1] = 1f;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2?(Projectile.Center), null);
			}
			if (Projectile.velocity.Length() < 18f&&Projectile.ai[0]>7)
			{
				Projectile.velocity *= 1.05f;
			}
			if (Projectile.velocity.X < 0f)
			{
				Projectile.spriteDirection = -1;
				Projectile.rotation = (float)Math.Atan2((double)(-(double)Projectile.velocity.Y), (double)(-(double)Projectile.velocity.X));
				return;
			}
			Projectile.spriteDirection = 1;
			Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X);
		}
		public override bool PreDraw(ref Color lightColor)
		{
			lightColor.R = (byte)(255f * Projectile.Opacity);
			lightColor.G = (byte)(255f * Projectile.Opacity);
			lightColor.B = (byte)(255f * Projectile.Opacity);

			CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Projectile.type], lightColor, 1, null, true);
			return false;
		}
		public override bool? CanHitNPC(NPC target)
		{
			return Projectile.ai[0]>=30;
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (hit.Damage <= 0)
			{
				return;
			}
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 90);
		}
		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item20, new Vector2?(Projectile.Center), null);
			for (int dust = 0; dust <= 5; dust++)
			{
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.LifeDrain, 0f, 0f, 0, default(Color), 1f);
			}
		}
	}
}
