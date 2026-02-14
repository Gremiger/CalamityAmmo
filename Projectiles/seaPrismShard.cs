using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles
{

	public class seaPrismShard : ModProjectile
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			Projectile.width = 6;
			Projectile.height = 6;
			Projectile.aiStyle = ProjAIStyleID.CrystalShard;
			Projectile.friendly = true;
			Projectile.penetrate = 1;
			Projectile.alpha = 50;
			Projectile.timeLeft = 300;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.extraUpdates = 1;
			Projectile.scale = 0.5f;
		}
		public override bool PreDraw(ref Color lightColor)
		{
			Projectile.rotation = (float)(Projectile.velocity.ToRotation() + Math.PI / 2);
			return true;
		}

	}
}
