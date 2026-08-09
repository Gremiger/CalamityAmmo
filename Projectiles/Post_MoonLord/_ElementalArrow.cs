using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles.Post_MoonLord
{
	public class _ElementalArrow : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Elemental Arrow");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "Elemental Arrow");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Измельченная Небесная морковь");
			Main.projFrames[Projectile.type] = 1;
			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
		}
		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 300;
			Projectile.tileCollide = true;
			Projectile.ignoreWater = true;
			Projectile.scale = 0.5f;
			Projectile.usesLocalNPCImmunity = true;//Does the NPC get invincibility frames based on projectile ID? (If set to true, and the player fires 8 of this projectile that all hit an enemy at once, all eight will land damage without the "fake damage" reduction — this is how vanilla's Night Bullet avoids the anti-fake-damage mechanic.)
			Projectile.localNPCHitCooldown = 15;//If the above is set to true, this gets used: how many invincibility frames the NPC gets, based on projectile ID
			Projectile.usesIDStaticNPCImmunity = false;//Does the NPC get invincibility frames based on projectile type? (If set to true, and the player fires 8 of this projectile that all hit an enemy at once, only one hit will land and the rest will pass through — vanilla uses this to cap the damage output of minions.)
			Projectile.idStaticNPCHitCooldown = 10;//If the above is set to true, this gets used: how many invincibility frames the NPC gets, based on projectile type
			Projectile.netImportant = false;
			Projectile.extraUpdates = 2;
			Projectile.arrow = true;
			Projectile.aiStyle = -1;
		}
		public override bool? CanCutTiles() => true;
		public override void AI()
		{
			Projectile.ai[0]++;
			Lighting.AddLight(Projectile.Center, Main.DiscoColor.ToVector3());
			if (Utils.NextBool(Main.rand, 5))
			{
				int num250 = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, DustID.RainbowTorch, (Projectile.direction * 2), 0f, 150, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1f);
				Main.dust[num250].velocity *= 0.2f;
				Main.dust[num250].noGravity = true;
			}
		}
		public override void OnSpawn(IEntitySource source)
		{
			/*Vector2 armPosition = Main.player[Projectile.owner].RotatedRelativePoint(Main.player[Projectile.owner].MountedCenter, true, true);
			Vector2 tipPosition = armPosition + Projectile.velocity * Projectile.width * 0.8f;
			Vector2 newVel = Projectile.velocity * 9f;
			Vector2 newPos = tipPosition + Utils.SafeNormalize(Projectile.velocity, Vector2.UnitX) * 36f;
			Projectile.NewProjectile(Projectile.GetSource_FromThis(null), newPos, newVel, ModContent.ProjectileType<ElementalFlare>(), (int)(Projectile.damage / 3f), Projectile.knockBack, Projectile.owner, newVel.Length(), -1f, 0f);
			Projectile.NewProjectile(Projectile.GetSource_FromThis(null), newPos, newVel, ModContent.ProjectileType<ElementalFlare>(), (int)(Projectile.damage / 3f), Projectile.knockBack, Projectile.owner, newVel.Length(), 1f, 0f);*/
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.AddBuff(ModContent.BuffType<ElementalMix>(), 60);
		}
		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			if (Projectile.numHits < 4)
			{
				modifiers.FinalDamage *= (float)Math.Pow(1.1f,Projectile.numHits+1);
			}
			else modifiers.FinalDamage *= (float)Math.Pow(1.1f, 4);
		}
		public override bool PreDraw(ref Color lightColor)
		{
			Projectile.rotation = (float)(Projectile.velocity.ToRotation() + Math.PI / 2);
			CalamityUtils.DrawAfterimagesCentered(Projectile, ProjectileID.Sets.TrailingMode[Projectile.type], lightColor, 1, null, true);
			return true;
		}
		public override bool PreKill(int timeLeft)
		{

			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);

			return true;
		}

	}
}

