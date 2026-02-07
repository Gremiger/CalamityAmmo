using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.DataStructures;
using CalamityMod;
using Terraria.Audio;
using CalamityMod.EntitySources;
using CalamityMod.Projectiles.Typeless;

namespace CalamityAmmo.Rockets
{ 
    public class Aerocket : ModItem
    {
        public override void SetStaticDefaults()
        {
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
            // DisplayName.SetDefault("Aerocket");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "天蓝浮雷");
            // Tooltip.SetDefault("Right click to switch projectile into air-staying booby trap");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "会爆炸出跟踪羽毛的滞空诡雷");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.RocketI);
            Item.rare = ItemRarityID.Lime; // Assign this item a rarity level of Pink
            Item.value = Item.buyPrice(0, 0, 80, 0); // The number and type of coins item can be sold for to an NPC
            Item.width = 30;
            Item.height = 16;
            Item.damage = 25;
            Item.noUseGraphic = true;
            Item.knockBack = 6f;
            Item.noMelee = true;
            Item.ammo = AmmoID.Rocket;
            Item.DamageType = DamageClass.Ranged;
            Item.maxStack = 9999;
            Item.consumable = true;
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {


        }
        public override void PickAmmo(Item weapon, Player player, ref int type, ref float speed, ref StatModifier damage, ref float knockback)
        {

            switch (weapon.type)
            {
                case ItemID.Celeb2:
                    type = ProjectileID.Celeb2Rocket;
                    break;
                case ItemID.GrenadeLauncher:
                    type = ModContent.ProjectileType<AeroGrenade>();
                    break;
                case ItemID.RocketLauncher or ItemID.FireworksLauncher:
                    type = ModContent.ProjectileType<AeroRocket>();
                    break;
                case ItemID.ProximityMineLauncher:
                    type = ModContent.ProjectileType<AeroMine>();
                    break;
                case ItemID.SnowmanCannon:
                    type = ModContent.ProjectileType<AeroSnowman>();
                    break;
            }
        }

        public abstract class Aerocket_Proj : ModProjectile
        {
            public override bool PreDraw(ref Color lightColor)
            {
                Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityAmmo/Rockets/Rocket_Aero").Value,
                Projectile.Center - Main.screenPosition, new Rectangle(0, Projectile.frame * 80, 30, 80), lightColor * (1 - (float)Projectile.alpha / 255), Projectile.rotation, new Vector2(15, 40), Projectile.scale,
                Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
                return false;
            }
            public override void SetStaticDefaults()
            {
                // DisplayName.SetDefault("Aerocket");
                //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "天蓝火箭");
                Main.projFrames[Projectile.type] = 4;
            }
   
            public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
            {
                if (projHitbox.Intersects(targetHitbox))
                {
                    Projectile.type = AIType;
                    return true;
                }
                return false;
            }
            public override bool PreKill(int timeLeft)
            {
                Projectile.type = AIType;
                return true;
            }
            public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
            {
                // if (AIType == ProjectileID.RocketI && target.Hitbox.Intersects(new Rectangle((int)Projectile.Center.X - 12, (int)Projectile.Center.Y - 12, 24, 24))) damage *= 2;
                //if (AIType == ProjectileID.ProximityMineI && Projectile.velocity.Length() < 1f && Projectile.alpha < 255) damage *= 3;
            }
            public override bool OnTileCollide(Vector2 oldVelocity)
            {
                if (AIType == ProjectileID.RocketI || AIType == ProjectileID.RocketSnowmanI) Projectile.type = AIType;
                return true;
            }
            public override bool PreAI()
            {
               
               
                return true;
            }
            public virtual void Inai()
            {
                
            }
            public override void AI()
            {
                Projectile.ai[1]++;
                Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI * 0.5f;
                if (Main.myPlayer == Projectile.owner)
                {
                    Inai();
                    if (Utils.NextBool(Main.rand, 5))
                    {
                        Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 187, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f, 100, new Color(53, Main.DiscoG, 255), 1f);
                    }
                    if (Utils.NextBool(Main.rand, 5))
                    {
                        Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 16, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f, 0, default(Color), 1f);
                    }
                    if (Utils.NextBool(Main.rand))
                    {
                        int smoke = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 31, 0f, 0f, 100, default(Color), 1f);
                        Main.dust[smoke].scale = 0.1f + Utils.NextFloat(Main.rand, 0f, 0.5f);
                        Main.dust[smoke].fadeIn = 1.5f + Utils.NextFloat(Main.rand, 0f, 0.5f);
                        Main.dust[smoke].noGravity = true;
                        Main.dust[smoke].position = base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, (float)(-(float)base.Projectile.height) / 2f), (double)base.Projectile.rotation, default(Vector2)) * 1.1f;
                        int fire = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 1f);
                        Main.dust[fire].scale = 1f + Utils.NextFloat(Main.rand, 0f, 0.5f);
                        Main.dust[fire].noGravity = true;
                        Main.dust[fire].position = base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, (float)(-(float)base.Projectile.height) / 2f), (double)base.Projectile.rotation, default(Vector2)) * 1.1f;
                    }
                }
                
            }
                
            
            public override void Kill(int timeLeft)
            {
                if (Main.myPlayer == Projectile.owner)
                {
                    Projectile.type = AIType;
                    Rectangle Explosion = new Rectangle((int)Projectile.Center.X - 100, (int)Projectile.Center.Y - 100, 200, 200);
                    Player player = Main.player[Projectile.owner];
                    Projectile.ExpandHitboxBy(200);
                    Projectile.maxPenetrate = (Projectile.penetrate = -1);
                    Projectile.usesLocalNPCImmunity = true;
                    Projectile.localNPCHitCooldown = 10;
                    //ProjectileSource_AerospecSetFeathers source = new ProjectileSource_AerospecSetFeathers(player);
                    for (int j = 0; j < 3; j++)
                    {
                        int featherDamage = (int)player.GetDamage<RangedDamageClass>().ApplyTo(18);
                        Vector2 spawnPos = Projectile.Center - new Vector2(0, 600).RotatedByRandom(Math.PI / 4);
                        int star = Projectile.NewProjectile(Projectile.GetSource_FromThis(), spawnPos, Vector2.Normalize(Projectile.Center  - spawnPos) * 24f, ModContent.ProjectileType<StickyFeatherAero>(), featherDamage, Projectile.knockBack, Main.myPlayer);
                    }
                    Projectile.Damage();
                    SoundEngine.PlaySound(SoundID.Item14, new Vector2?(Projectile.Center));
                    for (int d = 0; d < 40; d++)
                    {
                        int smoke = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
                        Main.dust[smoke].velocity *= 3f;
                        if (Utils.NextBool(Main.rand, 2))
                        {
                            Main.dust[smoke].scale = 0.5f;
                            Main.dust[smoke].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                        }
                    }
                    for (int d2 = 0; d2 < 70; d2++)
                    {
                        int fire = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
                        Main.dust[fire].noGravity = true;
                        Main.dust[fire].velocity *= 5f;
                        fire = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
                        Main.dust[fire].velocity *= 2f;
                    }
                    if (Main.netMode != 2)
                    {
                        Vector2 goreSource = Projectile.Center;
                        int goreAmt = 3;
                        Vector2 source =new Vector2 (goreSource.X - 24f, goreSource.Y - 24f);
                        for (int goreIndex = 0; goreIndex < goreAmt; goreIndex++)
                        {
                            float velocityMult = 0.33f;
                            if (goreIndex < goreAmt / 3)
                            {
                                velocityMult = 0.66f;
                            }
                            if (goreIndex >= 2 * goreAmt / 3)
                            {
                                velocityMult = 1f;
                            }
                            int type = Main.rand.Next(61, 64);
                            int smoke2 = Gore.NewGore(Projectile.GetSource_Death(null), source, default(Vector2), type, 1f);
                            Gore gore = Main.gore[smoke2];
                            gore.velocity *= velocityMult;
                            gore.velocity.X = gore.velocity.X + 1f;
                            gore.velocity.Y = gore.velocity.Y + 1f;
                            type = Main.rand.Next(61, 64);
                            smoke2 = Gore.NewGore(Projectile.GetSource_Death(null), source, default(Vector2), type, 1f);
                            Gore gore2 = Main.gore[smoke2];
                            gore2.velocity *= velocityMult;
                            gore2.velocity.X = gore2.velocity.X - 1f;
                            gore2.velocity.Y = gore2.velocity.Y + 1f;
                            type = Main.rand.Next(61, 64);
                            smoke2 = Gore.NewGore(Projectile.GetSource_Death(null), source, default(Vector2), type, 1f);
                            Gore gore3 = Main.gore[smoke2];
                            gore3.velocity *= velocityMult;
                            gore3.velocity.X = gore3.velocity.X + 1f;
                            gore3.velocity.Y = gore3.velocity.Y - 1f;
                            type = Main.rand.Next(61, 64);
                            smoke2 = Gore.NewGore(Projectile.GetSource_Death(null), source, default(Vector2), type, 1f);
                            Gore gore4 = Main.gore[smoke2];
                            gore4.velocity *= velocityMult;
                            gore4.velocity.X = gore4.velocity.X - 1f;
                            gore4.velocity.Y = gore4.velocity.Y - 1f;
                        }
                    }
                if (Explosion.Intersects(player.Hitbox))
                    {
                        player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI, Projectile.whoAmI), Projectile.damage, Math.Sign(player.Center.X - Projectile.Center.X));
                    }
                }
                return;
            }
        }
        internal class AeroRocket : Aerocket_Proj
        {
            public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
            public override void SetDefaults()
            {
                Projectile.CloneDefaults(ProjectileID.RocketI); AIType = ProjectileID.RocketI;
                Projectile.frame = 0;
                Projectile.timeLeft = 600;
            }
            public override void Inai()
            {
                if (Projectile.owner == Main.myPlayer)
                {
                    Projectile.velocity *= 0.9f;
                }
            }
        }
        internal class AeroGrenade : Aerocket_Proj
        {
            public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
            public override void SetDefaults()
            {
                Projectile.CloneDefaults(ProjectileID.GrenadeI); AIType = ProjectileID.GrenadeI;
                Projectile.frame = 1;
                Projectile.timeLeft = 600;
            }
            public override void Inai()
            {
                if (Projectile.owner == Main.myPlayer)
                {
                    if (Projectile.ai[1] > 30)
                    {
                        Projectile.velocity.Y *= 0;
                    }
                    Projectile.velocity *= 0.99f;
                }
            }
        }
        internal class AeroMine : Aerocket_Proj
        {
            public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
            public override void SetDefaults()
            {
                Projectile.CloneDefaults(ProjectileID.ProximityMineI); AIType = ProjectileID.ProximityMineI;
                Projectile.frame = 2;
                Projectile.timeLeft = 3600;
            }
            public override void Inai()
            {
                if (Projectile.owner == Main.myPlayer)
                {
                    if (Projectile.ai[1]>40)
                    {
                        Projectile.velocity.Y *= 0;
                    }
                    Projectile.velocity *= 0.999f;
                }
            }
        }
        internal class AeroSnowman : Aerocket_Proj
        {
            public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
            public override void SetDefaults()
            {
                Projectile.CloneDefaults(ProjectileID.RocketSnowmanI); AIType = ProjectileID.RocketSnowmanI;
                Projectile.frame = 3;
                Projectile.timeLeft = 600;
                //.velocity.X = 0;
            }
            public override void Inai()
            {
                if (Projectile.owner == Main.myPlayer)
                {
                    if (Projectile.ai[1]>30)
                    {
                        Projectile.velocity = new Vector2(0,-0);
                    }
                }
            }
        }
    }
}



