using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles
{
	public class rottenBulletProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 1;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
		}
		public override void SetDefaults()
		{
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = false;
			Projectile.usesLocalNPCImmunity = true;//Does the NPC get invincibility frames based on projectile ID? (If set to true, and the player fires 8 of this projectile that all hit an enemy at once, all eight will land damage without the "fake damage" reduction — this is how vanilla's Night Bullet avoids the anti-fake-damage mechanic.)
			Projectile.localNPCHitCooldown = 15;//If the above is set to true, this gets used: how many invincibility frames the NPC gets, based on projectile ID
			Projectile.usesIDStaticNPCImmunity = false;//Does the NPC get invincibility frames based on projectile type? (If set to true, and the player fires 8 of this projectile that all hit an enemy at once, only one hit will land and the rest will pass through — vanilla uses this to cap the damage output of minions.)
			Projectile.idStaticNPCHitCooldown = 10;//If the above is set to true, this gets used: how many invincibility frames the NPC gets, based on projectile type
			Projectile.netImportant = false;
		}
		public override bool? CanCutTiles() => true;
		public override void AI()
		{
			//Projectile.ai[0]++;

		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(ModContent.BuffType<BrainRot>(), 180);
			if (target.life > 0 || !target.IsAnEnemy(false, true, true))
			{
				return;
			}
			OnHitEffects(Main.player[Projectile.owner], target, 0);
		}
		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{

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

			return true;
		}
		private void OnHitEffects(Player player, Entity target, float kb)
		{
			//SoundEngine.PlaySound(ref SoundID.Item74, new Vector2?(target.Center), null);
			for (int i = 0; i < 15; i++)
			{
				int idx = Dust.NewDust(target.position, target.width, target.height, DustID.CorruptPlants, 0f, 0f, 100, default(Color), 2f);
				Main.dust[idx].velocity *= 3f;
				if (Utils.NextBool(Main.rand))
				{
					Main.dust[idx].scale = 0.5f;
					Main.dust[idx].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
				}
			}
			for (int j = 0; j < 25; j++)
			{
				int idx2 = Dust.NewDust(target.position, target.width, target.height, 18, 0f, 0f, 100, default(Color), 3f);
				Main.dust[idx2].noGravity = true;
				Main.dust[idx2].velocity *= 5f;
				idx2 = Dust.NewDust(target.position, target.width, target.height, 24, 0f, 0f, 100, default(Color), 2f);
				Main.dust[idx2].velocity *= 2f;
			}
			bool useDiagonal = Main.rand.NextBool(); // true = diagonal direction, false = orthogonal direction

			Vector2[] directions;

			if (!useDiagonal) // 50% chance: orthogonal direction
			{
				directions = new Vector2[]
				{
			new Vector2(0, -1),   // up
            new Vector2(0, 1),    // down
            new Vector2(-1, 0),   // left
            new Vector2(1, 0)     // right
				};
			}
			else // 50% chance: diagonal direction
			{
				// Normalize the diagonal direction
				float diagonalNormal = 0.7071f; // 1/√2 ≈ 0.7071
				directions = new Vector2[]
				{
			new Vector2(diagonalNormal, -diagonalNormal),  // northeast
            new Vector2(diagonalNormal, diagonalNormal),   // southeast
            new Vector2(-diagonalNormal, -diagonalNormal), // northwest
            new Vector2(-diagonalNormal, diagonalNormal)   // southwest
				};
			}
			// Spawn 4 projectiles, each aimed in one of the four directions
			for (int i = 0; i < 4; i++)
			{
				Projectile.NewProjectile(
					Projectile.GetSource_FromThis(),
					target.Center,
					directions[i] * Projectile.velocity.Length(), // use the predefined direction
					ModContent.ProjectileType<rottenBulletProj>(),
					Projectile.damage/2,
					kb,
					player.whoAmI,
					Projectile.knockBack
				);
			}
		}

	}
}


