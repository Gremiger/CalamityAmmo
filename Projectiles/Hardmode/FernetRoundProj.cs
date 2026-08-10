using CalamityAmmo.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles.Hardmode
{
	public class FernetRoundProj : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			Projectile.light = 0f;
			Projectile.tileCollide = true;
		}

		public override void AI()
		{
			Projectile.rotation = Projectile.velocity.ToRotation();
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item8, new Vector2?(Projectile.position));
			return true;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			int splashDamage = (int)(damageDone * 0.4f);
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<FizzySplash>(), splashDamage, 0f, Projectile.owner, DustID.Torch, 0f);
		}
	}
}
