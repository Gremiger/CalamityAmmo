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
using CalamityMod.Items.Placeables;
using Terraria.Audio;
using CalamityMod.Items;
using CalamityMod;
using CalamityMod.NPCs.NormalNPCs;
using static Humanizer.In;

namespace CalamityAmmo.Accessories
{
    public class InfectedCrabGill : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Infected Crab Gill");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "感染蟹腮");
            /* Tooltip.SetDefault("Summons spores over time that will damage enemies\n" +
                "Probability shoot out extra fungal rounds that split on death\n"+
                "Decrease liferegen by 1"); 
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "随着时间推移召唤可以伤害敌人的孢子\n" +
                "发射远程弹幕时有概率额外射出命中后分裂的真菌弹\n" +
                "减少1点生命再生");*/
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.value = CalamityGlobalItem.Rarity2BuyPrice;
            Item.rare = 2;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            modplayer.Spore = true;
        }
    }
    public class Crabulon_Spore : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crabulon's Spore");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "菌生蟹的孢子");
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.light = 0f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
        }
        public override void AI()
        {
            if (++Projectile.frameCounter >= 8)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
            }
            if (Projectile.timeLeft < 60)
            {
                Projectile.ai[0] = 0;
                Projectile.ai[1] += 1;
                Projectile.alpha = (int)((Projectile.ai[1] / 60) * 255);
            }
            if (Main.rand.NextBool(2))
            {
                int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), 14, 10, DustID.BlueFairy, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity = -Projectile.velocity * 0.5f;
                CalamityUtils.HomeInOnNPC(Projectile, !Projectile.tileCollide, 500f, 2f, 12f);
            }
                
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            int SPdamage = (int)player.GetDamage<RangedDamageClass>().ApplyTo(10);
            if (modplayer.MUN)
            {
                SPdamage = (int)player.GetDamage<RangedDamageClass>().ApplyTo(50);
            }
            int randomSP = Utils.SelectRandom(Main.rand, new int[]
                {
                    ModContent.ProjectileType<Spore1>(),
                    ModContent.ProjectileType<Spore2>(),
                    ModContent.ProjectileType<Spore3>(),
                });
            int proj=Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity * 0.1f, randomSP,
                SPdamage, 1.5f, player.whoAmI);
            Main.projectile[proj].usesIDStaticNPCImmunity = false;
            Main.projectile[proj].usesLocalNPCImmunity = true;
            Main.projectile[proj].localNPCHitCooldown = 15;
            Main.projectile[proj].CritChance = (int)player.GetCritChance<RangedDamageClass>();
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 56, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, 0, default(Color), 1f);
            }
        }
    }
    public class Spore1:ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spore Cloud");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "孢子云");
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 300;
            Projectile.light = 0f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
        }
        public override void AI()
        {
            if (Projectile.timeLeft < 60)
            {
                Projectile.ai[0] = 0;
                Projectile.ai[1] += 1;
                Projectile.alpha = (int)((Projectile.ai[1] / 60) * 255);
            }
            if (Main.rand.NextBool(2))
            {
                int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BlueFairy, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity = -Projectile.velocity * 0.5f;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 56, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, 0, default(Color), 1f);
            }
        }
    }
    public class Spore2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spore Cloud");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "孢子云");
        }
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 36;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 300;
            Projectile.light = 0f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
        }
        public override void AI()
        {
            if (Projectile.timeLeft < 60)
            {
                Projectile.ai[0] = 0;
                Projectile.ai[1] += 1;
                Projectile.alpha = (int)((Projectile.ai[1] / 60) * 255);
            }
            if(Main.rand.NextBool(2))
            {
                int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BlueFairy, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity = -Projectile.velocity * 0.5f;
            }
           
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 56, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, 0, default(Color), 1f);
            }
        }
    }
    public class Spore3 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spore Cloud");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "孢子云");
        }
        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 32;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 5;
            Projectile.timeLeft = 300;
            Projectile.light = 0f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 15;
        }
        public override void AI()
        {
            if (Projectile.timeLeft < 60)
            {
                Projectile.ai[0] = 0;
                Projectile.ai[1] += 1;
                Projectile.alpha = (int)((Projectile.ai[1] / 60) * 255);
            }
            if (Main.rand.NextBool(2))
            {
                int num469 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.BlueFairy, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity = -Projectile.velocity * 0.5f;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, 56, Projectile.oldVelocity.X * 0.5f, Projectile.oldVelocity.Y * 0.5f, 0, default(Color), 1f);
            }
        }
    }
}
