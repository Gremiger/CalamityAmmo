using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles
{
	public class _PrismBullet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("PrsimBullet");
			//DisplayName.AddTranslation(Terraria.Localization.GameCulture.FromCultureName(Terraria.Localization.GameCulture.CultureName.Chinese), "Prism Bullet");
			Main.projFrames[Projectile.type] = 1;
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Морская призматическая пуля");
		}
		public override void SetDefaults()
		{
			Projectile.width = 14;
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

		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{

		}
		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{

		}
		public override bool PreDraw(ref Color lightColor)
		{
			Projectile.rotation = (float)(Projectile.velocity.ToRotation() + Math.PI / 2);
			return base.PreDraw(ref lightColor);
		}

		public override bool PreKill(int timeLeft)
		{
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			return base.PreKill(timeLeft);
		}
		public override void OnKill(int timeLeft)
		{
			if (Projectile.owner == Main.myPlayer)
			{
				for(int i=0; i<2; i++)
				{
					float vectorX = -Projectile.velocity.X * Main.rand.NextFloat(0.4f, 0.7f) + Main.rand.NextFloat(-8f, 8.4f);
					float vectorY=-Projectile.velocity.Y * Main.rand.NextFloat(0.4f, 0.7f) + Main.rand.NextFloat(-8f, 8.4f);
					Projectile.NewProjectile(
					Projectile.GetSource_FromThis(),
					Projectile.position.X + vectorX,
					Projectile.position.Y+vectorY,
					vectorX,
					vectorY,
					ModContent.ProjectileType<seaPrismShard>(),
					(int)(Projectile.damage * 0.5f),
					0f,
					Projectile.owner
				);
				}
				
			}

		}
	}
}
