using System;
using CalamityAmmo;
using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.NPCs;
using CalamityMod.NPCs.AcidRain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Accessories
{
    // Token: 0x02000379 RID: 889
    public class RadioAura : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // base.DisplayName.SetDefault("Red Lightning Aura");
            ProjectileID.Sets.MinionSacrificable[base.Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            base.Projectile.width = 218;
            base.Projectile.height = 218;
            base.Projectile.ignoreWater = true;
            base.Projectile.minionSlots = 0f;
            base.Projectile.timeLeft = 18000;
            base.Projectile.tileCollide = false;
            base.Projectile.friendly = true;
            base.Projectile.timeLeft *= 5;
            base.Projectile.penetrate = -1;
            base.Projectile.usesLocalNPCImmunity = true;
            base.Projectile.localNPCHitCooldown = 30;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void AI()
        {
            base.Projectile.friendly = true;
            base.Projectile.hostile = false;
            base.Projectile.frameCounter++;
            if (base.Projectile.frameCounter > 3)
            {
                base.Projectile.localAI[0] += 1f;
                base.Projectile.frameCounter = 0;
            }
            if (base.Projectile.localAI[0] >= 6f)
            {
                base.Projectile.localAI[0] = 0f;
                base.Projectile.localAI[1] += 1f;
            }
            if (base.Projectile.localAI[1] >= 3f)
            {
                base.Projectile.localAI[1] = 0f;
            }
            Player player = Main.player[Projectile.owner];
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            Lighting.AddLight(Projectile.Center, 248, 255, 108);
            base.Projectile.Center = player.Center;
            if (player == null || player.dead)
            {
               modplayer.Radio= false;
                Projectile.Kill();
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(140, 234, 87, 200);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(144, 180, false);
  
        }

		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			target.AddBuff(144, 180, true, false);
		}

		public override bool PreDraw(ref Color lightColor)
        {
            Texture2D sprite = ModContent.Request<Texture2D>(this.Texture, (ReLogic.Content.AssetRequestMode)2).Value;
            Color drawColour = Color.White;
            Rectangle sourceRect = new (base.Projectile.width * (int)base.Projectile.localAI[1], base.Projectile.height * (int)base.Projectile.localAI[0], base.Projectile.width, base.Projectile.height);
            Vector2 origin  = new((float)(base.Projectile.width / 2), (float)(base.Projectile.height / 2));
            float opacity = 1f;
            int sparkCount = 0;
            int fadeTime = 20;
            if (base.Projectile.timeLeft < fadeTime)
            {
                opacity = (float)base.Projectile.timeLeft * (1f / (float)fadeTime);
                sparkCount = fadeTime - base.Projectile.timeLeft;
            }
            for (int i = 0; i < sparkCount * 2; i++)
            {
                int dustType = 132;
                if (Utils.NextBool(Main.rand))
                {
                    dustType = 264;
                }
                float rangeDiff = 2f;
                Vector2 dustPos=new (Utils.NextFloat(Main.rand, -1f, 1f), Utils.NextFloat(Main.rand, -1f, 1f));
                dustPos.Normalize();
                dustPos *= 98f + Utils.NextFloat(Main.rand, -rangeDiff, rangeDiff);
                int dust = Dust.NewDust(base.Projectile.Center + dustPos, 1, 1, dustType, 0f, 0f, 0, default(Color), 0.75f);
                Main.dust[dust].noGravity = true;
            }
            Main.EntitySpriteDraw(sprite, base.Projectile.Center - Main.screenPosition, new Rectangle?(sourceRect), drawColour * opacity, base.Projectile.rotation, origin, 1f, 0, 0);
            return false;
        }

        // Token: 0x06001709 RID: 5897 RVA: 0x0009A842 File Offset: 0x00098A42
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return new bool?(CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 98f, targetHitbox));
        }

        // Token: 0x0600170A RID: 5898 RVA: 0x0009A860 File Offset: 0x00098A60
        public override bool? CanHitNPC(NPC target)
        {
            if (target.catchItem != 0 && target.type != ModContent.NPCType<Radiator>())
            {
                return new bool?(false);
            }
            return default(bool?);
        }

        // Token: 0x0400044F RID: 1103
        private const float radius = 98f;

        // Token: 0x04000450 RID: 1104
        private const int framesX = 3;

        // Token: 0x04000451 RID: 1105
        private const int framesY = 6;
    }
}
