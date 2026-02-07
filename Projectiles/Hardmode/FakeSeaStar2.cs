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
    public class FakeSeaStar2:ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Brittle Star");
        }

        public override void SetDefaults()
        {
            base.Projectile.width = 30;
            base.Projectile.height = 30;
            base.Projectile.netImportant = true;
            base.Projectile.friendly = true;
            base.Projectile.ignoreWater = true;
            base.Projectile.timeLeft = 300;
            base.Projectile.penetrate = 1;
            base.Projectile.tileCollide = false;
            base.Projectile.DamageType = DamageClass.Ranged;
        }
        public override void AI()
        {
            Projectile.rotation += base.Projectile.velocity.X * 0.04f;
        }
    }
}
