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
using CalamityMod.Projectiles.Melee;
using CalamityAmmo.Projectiles.Post_MoonLord;
using Terraria.Audio;
using CalamityAmmo.Ammos.Hardmode;
using CalamityMod.Items.Weapons.Ranged;
using System.Threading;
using System.Timers;

namespace CalamityAmmo.Ammos.Post_MoonLord
{

    public class MirageArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mirage Arrow");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "幻境箭");
            // Tooltip.SetDefault("Another two,another two, another two,and another two");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "我给你留了（至少）两箭！");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Золотого пера Стрела");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Может привлечь что-то маленькое \nвызвать красную молнию, когда не нанесен критический удар");
        }
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 2.5f;
            Item.value = Item.buyPrice(0, 0, 5, 0);
            Item.rare = ItemRarityID.Purple;
            Item.shoot = ModContent.ProjectileType<MirageArrow_Proj>();
            Item.shootSpeed = 6f;
            Item.ammo = AmmoID.Arrow;

        }
        
        public override void AddRecipes()
        {
            CreateRecipe(333)
            .AddIngredient(ModContent.ItemType<Polterplasm>(), 3)
            .AddIngredient(1508,5)
            // .AddIngredient(ModContent.ItemType<RuinousSoul > (), 1)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
        }

    
    }
    public class MirageArrow_Proj : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mirage Arrow");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = ProjectileID.WoodenArrowFriendly;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.light = 0f;
            Projectile.extraUpdates = 1;
            //Projectile.arrow = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item8, new Vector2?(Projectile.position));
            return true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + Projectile.velocity*4, Projectile.velocity, ModContent.ProjectileType<MirageArrow_Proj2>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
            if (Main.rand.NextBool(3))
            {
                float spawnPos = Main.rand.NextFloat(-100, 101);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(spawnPos, spawnPos), Projectile.velocity, ModContent.ProjectileType<MirageArrow_Proj>(), Projectile.damage,Projectile.knockBack, Main.myPlayer);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center + new Vector2(spawnPos, spawnPos) + Projectile.velocity*2, Projectile.velocity, ModContent.ProjectileType<MirageArrow_Proj2>(), Projectile.damage , Projectile.knockBack, Main.myPlayer);
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[Projectile.owner];
            if (player.HeldItem.type == ModContent.ItemType<TheStorm>())
            {
                modifiers.FinalDamage *= 0.33f;
            }
            else modifiers.FinalDamage *= 0.66f;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            Projectile.rotation = Utils.ToRotation(Projectile.velocity) + MathHelper.ToRadians(90f);
            CalamityUtils.HomeInOnNPC(Projectile, !Projectile.tileCollide, 500f, Projectile.velocity.Length(), 12f);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return base.PreDraw(ref lightColor);
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
        }
    }
    public class MirageArrow_Proj2 : ModProjectile
    {
        public override string Texture => "CalamityAmmo/Ammos/Post_MoonLord/MirageArrow_Proj";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mirage Arrow");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = ProjectileID.WoodenArrowFriendly;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.light = 0f;
            Projectile.extraUpdates = 1;
            //Projectile.arrow = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item8, new Vector2?(Projectile.position));
            return true;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            
            Player player = Main.player[Projectile.owner];
            if (player.HeldItem.type==ModContent.ItemType<TheStorm>())
            {
				modifiers.FinalDamage*= 0.33f;
            }
            else modifiers.FinalDamage *= 0.66f;

		}
        public override void OnSpawn(IEntitySource source)
        {

            base.OnSpawn(source);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return base.PreDraw(ref lightColor);
        }
        public override void AI()
        {
            Projectile.rotation = Utils.ToRotation(Projectile.velocity) + MathHelper.ToRadians(90f);
            CalamityUtils.HomeInOnNPC(Projectile, !Projectile.tileCollide, 500f, Projectile.velocity.Length(), 12f);
        }
    }
}


