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
			Projectile.usesLocalNPCImmunity = true;//Does the NPC gain immunity frames based on projectile ID? (If set to true, when a player fires 8 of this projectile and they all hit the enemy at the same time, all 8 will register damage without being counted as fake damage — this is how the vanilla Nightglow Bullet's anti-fake-damage works)
			Projectile.localNPCHitCooldown = 15;//If the previous one is set to true, this is used: how many immunity frames the NPC gets based on projectile ID
			Projectile.usesIDStaticNPCImmunity = false;//Does the NPC gain immunity frames based on projectile type? (If set to true, when a player fires 8 of this projectile and they hit the enemy at the same time, only one will register a hit and the rest will pass through — vanilla uses this to cap the damage output of low-tier minion enemies)
			Projectile.idStaticNPCHitCooldown = 10;//If the previous one is set to true, this is used: how many immunity frames the NPC gets based on projectile type
			Projectile.netImportant = false;
			Projectile.extraUpdates = 2;
			
		}
		public override bool? CanCutTiles() => true;
		public override void AI()
		{
			Player p = Main.player[Projectile.owner];
			//Main.NewText("lifeSteal="+p.lifeSteal);
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


