using System;
using CalamityAmmo.Ammos.Hardmode;
using CalamityAmmo.Projectiles.Hardmode;
using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace CalamityAmmo.Projectiles.Hardmode
{
    public class _Flower : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Perennial Flower");
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
                Projectile.height = 8;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 1200;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            base.Projectile.usesLocalNPCImmunity = true;
            base.Projectile.localNPCHitCooldown = 10;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void AI()
        {
            if (speed == 0f)
            {
                speed = Projectile.velocity.Length();
            }
            Projectile.rotation += 0.01f;
            CalamityUtils.HomeInOnNPC(Projectile, !Projectile.tileCollide, 500f, speed, 12f);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            

        }
        private float speed;
    }
}


