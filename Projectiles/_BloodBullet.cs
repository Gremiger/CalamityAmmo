using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles
{
	public class _BloodBullet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 1;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
		}
		public override void SetDefaults()
		{
			Projectile.width = 4;
			Projectile.height = 14;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = true;
			Projectile.usesLocalNPCImmunity = true;//NPC是不是按照弹幕ID来获取无敌帧？（如果设定为true，玩家发射8个该弹幕同时击中敌人，则八个都能击中，不骗伤，原版夜明弹的反骗伤就是如此）
			Projectile.localNPCHitCooldown = 15;//上一个设定为true则被调用，NPC按照弹幕ID来获取多少无敌帧
			Projectile.usesIDStaticNPCImmunity = false;//NPC是不是按照弹幕类型来获取无敌帧？（如果设定为true，玩家发射8个该弹幕同时击中敌人，则只能击中一次，其余的会穿透，原版用它来控制喽啰的输出上限）
			Projectile.idStaticNPCHitCooldown = 10;//上一个设定为true则被调用，NPC按照弹幕类型来获取多少无敌帧
			Projectile.netImportant = false;
			Projectile.extraUpdates = 2;
			
		}
		public override bool? CanCutTiles() => true;
		public override void AI()
		{
			Player p = Main.player[Projectile.owner];
			Main.NewText("lifeSteal="+p.lifeSteal);
		}


		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			Player p = Main.player[Projectile.owner];
			float healamount = Projectile.damage * 0.2f;
			if (target.type != NPCID.TargetDummy && Main.myPlayer == Projectile.owner )
			{
				if(p.lifeSteal > 0)
				{
					p.lifeSteal -= healamount;
					int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ProjectileID.VampireHeal, 0, 0, Projectile.owner, Projectile.owner, healamount);
					Main.projectile[proj].timeLeft = 300;
					Main.projectile[proj].netUpdate = true;
				}
				else
				{
					p.AddBuff(ModContent.BuffType<BurningBlood>(), 300);
				}
			}
			target.AddBuff(ModContent.BuffType<BurningBlood>(), 300);
		}
		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{

		}
		public override bool PreDraw(ref Color lightColor)
		{
			Projectile.rotation = (float)(Projectile.velocity.ToRotation() + Math.PI / 2);
			return true;
		}

		public override bool PreKill(int timeLeft)
		{

			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);

			return base.PreKill(timeLeft);
		}

	}
}


