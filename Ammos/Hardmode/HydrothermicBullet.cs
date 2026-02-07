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
using CalamityMod;
using CalamityMod.Projectiles.Ranged;
using static CalamityAmmo.CAEUtils;
using CalamityAmmo.Projectiles.Hardmode;
using CalamityAmmo.Projectiles.Post_MoonLord;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Items.Placeables.DraedonStructures;
using Terraria.Audio;
using CalamityMod.Projectiles.Summon;

namespace CalamityAmmo.Ammos.Hardmode
{
    public class HydrothermicBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hydrothermic Bullet");
            // Tooltip.SetDefault("My body burns at over 1,400 degrees!");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "渊泉子弹");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "我的身体内部，可是一千四百度喔。");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Ископаемая пуля");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Прочные окаменелости сокрушают оборону врага \nГниющие в кислоте окаменелости высвобождают годы остаточного яда");
        }
        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 1f;
            Item.value = Item.buyPrice(0, 0, 3, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.shoot = ModContent.ProjectileType<HydrothermicBullet_Proj>();
            Item.shootSpeed = 7f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(200);
            recipe.AddIngredient(ItemID.EmptyBullet, 200);
            recipe.AddIngredient(ModContent.ItemType<RustedPipes>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ScoriaBar>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CoreofHavoc>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

    }
    public class HydrothermicBullet_Proj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hydrothermic Bullet");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.light = 0f;

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item8, new Vector2?(Projectile.position));
            return true;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(),new Vector2(target.Center.X, target.Center.Y+target.height/2),new Vector2 (0, 0), ModContent.ProjectileType<HydrothermicVolcano>(), Projectile.damage, 0f,Main.myPlayer);
        }
        public override void AI()
        {
            Projectile.rotation = Utils.ToRotation(Projectile.velocity) + MathHelper.ToRadians(90f);
            if (Utils.NextBool(Main.rand, 4))
            {
                int num469 = Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, Utils.NextBool(Main.rand, 3) ? 16 : 174, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 0f;
            }
        }
    }
    public class HydrothermicVolcano : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hydrothermic Volcano ");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 20;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 150;
            Projectile.light = 0f;

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item8, new Vector2?(Projectile.position));
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.ai[0]++;
            if (Projectile.owner == Main.myPlayer&& Projectile.ai[0]%10==0)
            {
                Projectile hotwater = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center - new Vector2(0, 24), new Vector2(0, -12), ProjectileID.GeyserTrap, Projectile.damage*2, 4f, Projectile.owner);
                hotwater.friendly = true;
                hotwater.hostile = false;
            }
            if(Main.rand.NextBool(15))
            {
                Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center - new Vector2(0, 24), new Vector2(0, -12)+new Vector2(Main.rand.Next(-10,11),0), ModContent.ProjectileType<VolcanicFireballRanged>(), Projectile.damage/4, 4f, Projectile.owner);
            }
            foreach (var proj in Main.projectile)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<HydrothermicVolcano>()] > 13 && proj.owner == player.whoAmI
                    &&proj.type==ModContent.ProjectileType< HydrothermicVolcano >())
                {
                    proj.penetrate = 0;
                }
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
          
            
        }

    }
    public class VolcanicFireballRanged : VolcanicFireballSummon
    {
        public override string Texture => "CalamityMod/Projectiles/Melee/VolcanicFireball";
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 2;
            Projectile.scale = 0.8f;
            Projectile.ArmorPenetration = 20;
            base.SetDefaults();
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Projectile.timeLeft < 60)
            {
                Projectile.ai[1] += 1;
                Projectile.alpha = (int)((Projectile.ai[1] / 60) * 255);
            }
            CalamityUtils.HomeInOnNPC(Projectile, !Projectile.tileCollide, 500f, 8, 12f);
        }
    }
}

