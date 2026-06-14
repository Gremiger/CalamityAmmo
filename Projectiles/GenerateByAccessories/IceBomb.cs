using System;
using System.IO;
using CalamityMod;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Events;
using CalamityMod.NPCs.Cryogen;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles.GenerateByAccessories
{
	public class IceBomb : ModProjectile, ILocalizedModType, IModType
	{
		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.scale = 0.6f;
			Projectile.friendly= true;
			Projectile.coldDamage = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
		}
		public override bool? CanHitNPC(NPC target)
		{
			return Projectile.ai[0] >= 60f;
		}
		public override void AI()
		{
			Projectile.velocity *= 0.98f;
			if (Projectile.ai[0] < 60f)
			{
				Projectile.ai[0] += 1f;
				if (Projectile.ai[0] == 60f)
				{
					for (int i = 0; i < 8; i++)
					{
						int iceDust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 67, 0f, 0f, 100, default(Color), 2f);
						Main.dust[iceDust].velocity *= 3f;
						if (Utils.NextBool(Main.rand))
						{
							Main.dust[iceDust].scale = 0.5f;
							Main.dust[iceDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
						}
					}
					for (int j = 0; j < 14; j++)
					{
						int iceDust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 67, 0f, 0f, 100, default(Color), 3f);
						Main.dust[iceDust2].noGravity = true;
						Main.dust[iceDust2].velocity *= 5f;
						iceDust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 67, 0f, 0f, 100, default(Color), 2f);
						Main.dust[iceDust2].velocity *= 2f;
					}
					Projectile.scale = 1.2f;
					Projectile.ExpandHitboxBy((int)(30f * Projectile.scale));
					SoundEngine.PlaySound(SoundID.Item30, new Vector2?(Projectile.Center), null);
				}
			}
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(new Color(1f, 1f, 1f, 1f) * Projectile.Opacity);
		}
		public override bool PreDraw(ref Color lightColor)
		{
			Projectile.DrawProjectileWithBackglow(Cryogen.BackglowColor, lightColor, 4f, null, default(Rectangle?));
			return false;
		}
		public override void OnKill(int timeLeft)
		{
			if (Projectile.owner == Main.myPlayer)
			{
				int totalProjectiles = 8;
				float radians = 6.2831855f / (float)totalProjectiles;
				int type = ModContent.ProjectileType<IceRain>();
				int damage = (int)Math.Round(Projectile.damage * 0.34);
				float velocity = 1f;
				Vector2 spinningPoint=new(0f, -velocity);
				for (int i = 0; i < totalProjectiles; i++)
				{
					Vector2 vector255 = Utils.RotatedBy(spinningPoint*1.1f, (double)(radians * (float)i));
					Projectile.NewProjectile(Projectile.GetSource_FromThis(null), Projectile.Center, vector255, type, damage, 0f, Projectile.owner, 1f, 0f, 0f);
				}
			}
			for (int j = 0; j < 10; j++)
			{
				Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 67, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (hit.Damage <= 0)
			{
				return;
			}
			if (Projectile.ai[0] >= 120f)
			{
				target.AddBuff(BuffID.Frostburn, 180);
			}
		}
	}
	public class IceRain : ModProjectile, ILocalizedModType, IModType
	{
		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.scale = 1.2f;
			Projectile.friendly = true;
			Projectile.coldDamage = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 600;
		}
		public override void SendExtraAI(BinaryWriter writer)
		{
			writer.Write(Projectile.localAI[0]);
		}
		public override void ReceiveExtraAI(BinaryReader reader)
		{
			Projectile.localAI[0] = reader.ReadSingle();
		}
		public override void AI()
		{
			Lighting.AddLight((int)((Projectile.position.X + (float)(Projectile.width / 2)) / 16f), (int)((Projectile.position.Y + (float)(Projectile.height / 2)) / 16f), 0f, 0.25f, 0.25f);
			if (Projectile.ai[0] == 0f)
			{
				//
				{
					
				}
				Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.5707964f;
				for (int i = 0; i < 2; i++)
				{
					int icyDust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 92, Projectile.velocity.X, Projectile.velocity.Y, 50, default(Color), 0.6f);
					Main.dust[icyDust].noGravity = true;
					Main.dust[icyDust].velocity *= 0.3f;
				}
				return;
			}
			if (Projectile.velocity.Length() < 20f) 
			Projectile.velocity *= 1.02f;
			//Main.NewText(Projectile.velocity.Length());
			if (Projectile.ai[0] == 1f)
			{
				Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.5707964f;
				for (int j = 0; j < 2; j++)
				{
					int icyDust2 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 92, Projectile.velocity.X, Projectile.velocity.Y, 50, default(Color), 0.6f);
					Main.dust[icyDust2].noGravity = true;
					Main.dust[icyDust2].velocity *= 0.3f;
				}
				return;
			}
			if (Projectile.ai[0] == 2f)
			{
				Projectile projectile = Projectile;
				projectile.velocity.Y = projectile.velocity.Y + 0.1f;
				Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.5707964f;
				if (Projectile.velocity.Y > 6f)
				{
					Projectile.velocity.Y = 6f;
				}
			}
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(new Color(1f, 1f, 1f, 1f) * Projectile.Opacity);
		}
		public override bool PreDraw(ref Color lightColor)
		{
			Projectile.DrawProjectileWithBackglow(Cryogen.BackglowColor, lightColor, 4f, null, default(Rectangle?));
			return false;
		}
		public override void OnKill(int timeLeft)
		{
			for (int i = 0; i < 3; i++)
			{
				int snowDust = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 76, 0f, 0f, 0, default(Color), 1f);
				Main.dust[snowDust].noGravity = true;
				Main.dust[snowDust].noLight = true;
				Main.dust[snowDust].scale = 0.7f;
			}
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (hit.Damage <= 0)
			{
				return;
			}
			if (Projectile.ai[0] >= 120f)
			{
				target.AddBuff(BuffID.Frostburn, 120);
			}
		}
	}
}
