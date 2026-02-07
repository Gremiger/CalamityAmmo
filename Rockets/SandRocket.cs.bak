using System;
using System.Collections.Generic;
using CalamityAmmo.Global;
using CalamityMod;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace CalamityAmmo.Rockets
{
    public class SandRocket : ModItem
    {
        public override void SetStaticDefaults()
        {
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
            // DisplayName.SetDefault("Earth-Penetrating Missile");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "钻地导弹");
            /* Tooltip.SetDefault("Can go through tiles and tracking enemies\n" +
                "Move faster in sand blocks"); */
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "在物块中自动寻的\n在沙子中移动更快");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.RocketI);
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.buyPrice(0, 0, 80, 0);
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
                    type = ModContent.ProjectileType<SandGrenade>();
                    break;
                case ItemID.RocketLauncher or ItemID.FireworksLauncher:
                    type = ModContent.ProjectileType<Sand_Rocket>();
                    break;
                case ItemID.ProximityMineLauncher:
                    type = ModContent.ProjectileType<SandMine>();
                    break;
                case ItemID.SnowmanCannon:
                    type = ModContent.ProjectileType<SandSnowman>();
                    break;
            }
        }
    }
    internal class Sand_Rocket : SandRocket_Proj
    {
        public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.RocketI); AIType = ProjectileID.RocketI;
            Projectile.frame = 0;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
        }
    }
    internal class SandGrenade : SandRocket_Proj
    {
        public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.GrenadeI); AIType = ProjectileID.GrenadeI;
            Projectile.frame = 1;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
        }
    }
    internal class SandMine : SandRocket_Proj
    {
        public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ProximityMineI); AIType = ProjectileID.ProximityMineI;
            Projectile.frame = 2;
            Projectile.timeLeft = 3600;
            Projectile.tileCollide = false;
        }
    }



    internal class SandSnowman : SandRocket_Proj
    {
        public override string Texture => "CalamityAmmo/Rockets/RocketPlaceholder";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.RocketSnowmanI); AIType = ProjectileID.RocketSnowmanI;
            Projectile.frame = 3;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            //.velocity.X = 0;
        }
    }
    public abstract class SandRocket_Proj : ModProjectile
    {
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + (float)Math.PI / 2;
            if (Projectile.frame < 12) Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityAmmo/Rockets/Rocket_Sand").Value,
            Projectile.Center - Main.screenPosition, new Rectangle(0, Projectile.frame * 80, 30, 60), lightColor * (1 - (float)Projectile.alpha / 255), Projectile.rotation, new Vector2(15, 4), Projectile.scale,
            Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            else Main.EntitySpriteDraw(ModContent.Request<Texture2D>(Texture).Value,
            Projectile.Center - Main.screenPosition, new Rectangle(0, 0, 32, 40), lightColor * (1 - (float)Projectile.alpha / 255), Projectile.rotation, new Vector2(16, 4), Projectile.scale,
            Projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0);
            return false;
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Earth-Penetrating Missile");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "钻地导弹");
            Main.projFrames[Projectile.type] = 12;
        }
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = false;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
            Init();
        }
        public virtual void Init()
        {
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            Projectile.alpha = Utils.Clamp(255 - (int)Projectile.ai[0] * 25, 0, 255);
            Tile tile = Framing.GetTileSafely((int)(Projectile.Center.X / 16), (int)(Projectile.Center.Y / 16));
            float maxSpeed = 2.5f;
            if (tile.HasTile && Main.tileSolid[tile.TileType])
            {
                CalamityUtils.HomeInOnNPC(Projectile, true, 300f, Projectile.velocity.Length(), 6f);
                if (Main.tileSand[tile.TileType])
                {
                    maxSpeed = 3.5f;
                }
            }
            Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 2f * maxSpeed;
            //Main.NewText(Projectile.velocity.Length());
            if (Main.rand.Next(1, 4) <= 2)
            {
                int dust = Dust.NewDust(Projectile.position + new Vector2(5, 5) - 16 * (Projectile.rotation - (float)Math.PI / 2).ToRotationVector2(), 0, 0, 6);
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0f;
            }
            else
            {
                int goreIndex = Dust.NewDust(Projectile.Center - new Vector2(3, 3), 0, 0, DustID.Smoke);
                Main.dust[goreIndex].alpha = 66;
                Main.dust[goreIndex].velocity = Vector2.Normalize(Projectile.velocity).RotatedByRandom(Math.PI / 10) * -1f;
                Main.dust[goreIndex].velocity *= Main.rand.NextFloat(-1, 2);
            }
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
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.immune[Projectile.owner] = 10;
        }
        public override void Kill(int timeLeft)
        {
            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.type = AIType;
                Projectile.width = 144;
                Projectile.height = 144;
                if (AIType == ProjectileID.RocketIII || AIType == ProjectileID.RocketIV)
                {
                    Projectile.width = 272;
                    Projectile.height = 272;
                }
                if (AIType == ProjectileID.MiniNukeRocketI || AIType == ProjectileID.MiniNukeRocketII)
                {
                    Projectile.width = 384;
                    Projectile.height = 384;
                }
                Projectile.position.X = Projectile.position.X - Projectile.width / 2;
                Projectile.position.Y = Projectile.position.Y - Projectile.height / 2;
                Projectile.Damage();
                Player player = Main.player[Projectile.owner];
                if (Projectile.Hitbox.Intersects(player.Hitbox))
                {
                    player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI, Projectile.whoAmI), Projectile.damage, Math.Sign(player.Center.X - Projectile.Center.X));
                }
            }
            return;
        }
    }
}

