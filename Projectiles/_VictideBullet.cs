using System;
using System.IO;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using System.Text;
using Terraria.ModLoader;
using ReLogic.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using ReLogic.Content;
using Terraria.GameContent;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Items.Materials;
using CalamityAmmo.Projectiles;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Projectiles.Typeless;
using CalamityAmmo.Misc;

namespace CalamityAmmo.Projectiles
{
    public class _VictideBullet : ModProjectile
    {
		private bool hasLiquidBoost = false;
		private int originalExtraUpdates = 0;
		public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 8;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;
            Projectile.usesLocalNPCImmunity = true;//Does the NPC gain immunity frames based on projectile ID? (If set to true, when a player fires 8 of this projectile and they all hit the enemy at the same time, all 8 will register damage without being counted as fake damage — this is how the vanilla Nightglow Bullet's anti-fake-damage works)
            Projectile.localNPCHitCooldown = 20;//If the previous one is set to true, this is used: how many immunity frames the NPC gets based on projectile ID
            Projectile.usesIDStaticNPCImmunity = false;//Does the NPC gain immunity frames based on projectile type? (If set to true, when a player fires 8 of this projectile and they hit the enemy at the same time, only one will register a hit and the rest will pass through — vanilla uses this to cap the damage output of low-tier minion enemies)
            Projectile.idStaticNPCHitCooldown = 15;//If the previous one is set to true, this is used: how many immunity frames the NPC gets based on projectile type
            Projectile.netImportant = true;
        }
        public override bool? CanCutTiles() => true;
		public override void AI()
		{
			if (Projectile.wet && !hasLiquidBoost)
			{
				Projectile.extraUpdates = originalExtraUpdates + 1;
				hasLiquidBoost = true;

				if (Main.rand.NextBool(3))
				{
					Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
						DustID.Water, 0f, 0f, 100, default, 1f);
					dust.velocity = Projectile.velocity * 0.5f;
					dust.noGravity = true;
				}
			}
			else if (!Projectile.wet && hasLiquidBoost)
			{
				Projectile.extraUpdates = originalExtraUpdates;
				hasLiquidBoost = false;
			}

		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Main.player[Projectile.owner].AddBuff(ModContent.BuffType<tideOfVictory>(), 120);
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

    }
}


