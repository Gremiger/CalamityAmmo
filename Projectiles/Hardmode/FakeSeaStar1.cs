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
     public class FakeSeaStar1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Aquatic Star");
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 32;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 300;
            Projectile.penetrate = 1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.X * 0.04f;
        }

    }
}

