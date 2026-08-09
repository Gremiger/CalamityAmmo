using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles.GenerateByAccessories
{
	public class ElectricStream : ModProjectile
	{
		public override void SetStaticDefaults()
		{
		}
		public override void SetDefaults()
		{
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.timeLeft = 100;
			Projectile.ignoreWater = true;
			Projectile.extraUpdates = 100;
			Projectile.penetrate = -1;
		}
		public override void AI()
		{
			// Emit red light
			Lighting.AddLight(Projectile.position, 0.0f, 0.0f, 0.2f);

			// Linear particle effect
			for (int i = 0; i < 2; i++)
			{
				Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Vortex, 0, 0, 100, Color.White, 1f);
				d.position = Projectile.Center;// - Projectile.velocity * i / 3f;
				d.velocity *= 0.2f;
				d.noGravity = true;
				d.scale = Main.rand.Next(90, 110) * 0.008f;
			}

			// Get the target NPC
			NPC target = Main.npc[(int)Projectile.ai[0]];
			// If the target NPC is alive
			if (target.active)
			{
				// Calculate the vector pointing toward the target
				Vector2 targetVec = target.Center - Projectile.Center;
				targetVec.Normalize();
				// The target vector is a vector of magnitude 20 pointing toward the target
				targetVec *= 6f;
				// Unit vector toward the NPC * 20 + 3.33% offset
				Projectile.velocity = (Projectile.velocity * 30f + targetVec) / (31f);
			}
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(BuffID.Electrified, 30);
		}
	}
}

