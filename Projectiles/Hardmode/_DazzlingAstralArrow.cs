using System;
using CalamityMod;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.Ranged;
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

    public class _DazzlingAstralArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dazzling Astral Arrow");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.arrow = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.light = 0f;
            Projectile.extraUpdates = 2;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
            SoundEngine.PlaySound(SoundID.Item8, new Vector2?(Projectile.position));
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 180);
            
        }
        public override bool PreAI()
        {
            Projectile.ai[0]++;
            Projectile.rotation = Utils.ToRotation(Projectile.velocity) + MathHelper.ToRadians(90f);
            Projectile.spriteDirection = Projectile.direction;
            Projectile.localAI[0] += 1f;
            if (Projectile.localAI[0] > 4f && Utils.NextBool(Main.rand, 2))
            {
                int randomDust = Utils.SelectRandom<int>(Main.rand, new int[]
                {
                    ModContent.DustType<AstralOrange>(),
                    ModContent.DustType<AstralBlue>()
                });
                int astral = Dust.NewDust(Projectile.position, 6, 6,randomDust, 0f, 0f, 0, default, 1f);
                Dust.NewDust(Projectile.position, 6, 6, randomDust, 0f, 0f, 0, default, 1f);
                Dust.NewDust(Projectile.position, 6, 6, randomDust, 0f, 0f, 0, default, 1f);
                Main.dust[astral].alpha = Projectile.alpha;
                Vector2 Normal = Projectile.velocity.RotatedBy(Math.PI / 2);
                Normal.Normalize();
                Main.dust[astral].velocity += Normal * (float)Math.Sin(Projectile.ai[0]);
                Main.dust[astral].noGravity = true;
            }
            

            return false;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[Projectile.owner];
            if (player.HeldItem.type == ModContent.ItemType<TheStorm>())
            {
                
            }
        }
        public override void OnKill(int timeLeft)
        {
			Player player = Main.player[Projectile.owner];
			Vector2 projSpawn = new(Projectile.Center.X + Main.rand.Next(144) - Main.rand.Next(144), player.Center.Y - 600);
            int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), projSpawn, Vector2.Normalize(Projectile.Center - projSpawn) * 18,
                                                ModContent.ProjectileType<_AstralStar>(), (int)(Projectile.damage * 0.5), 2, 0);
            Main.projectile[proj].netUpdate = true;
            
        }

    }
}




