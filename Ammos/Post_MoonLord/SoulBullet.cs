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
using CalamityMod.Projectiles.Rogue;
using CalamityMod.NPCs.Bumblebirb;
using CalamityMod.Dusts;
using CalamityMod.NPCs.NormalNPCs;

namespace CalamityAmmo.Ammos.Post_MoonLord
{
    public class SoulBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Soul Bullet");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "魂弹");
            // Tooltip.SetDefault("May attract something small \nSummon red lightning when not critically hit");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "噢！如此悲惨的结局！");
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
            Item.shoot = ModContent.ProjectileType<SoulBullet_Proj>();
            Item.shootSpeed = 6f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            CreateRecipe(333)
            .AddIngredient(ModContent.ItemType<Necroplasm>(), 3)
            .AddIngredient(ItemID.Ectoplasm, 5)
            // .AddIngredient(ModContent.ItemType<RuinousSoul > (), 1)
            .AddTile(TileID.LunarCraftingStation)
            .Register();
        }


    }
    public class SoulBullet_Proj : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Phantom Spirit");
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 1;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ArmorPenetration = 30;
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.alpha = 55;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 300;
            Projectile.usesLocalNPCImmunity= true;
            Projectile.localNPCHitCooldown = 10;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Player player = Main.player[Projectile.owner];
            if (Main.rand.Next(66) == 0)
            {
                int randomSoul = Utils.SelectRandom<int>(Main.rand, new int[]
                {
                    ModContent.NPCType<PhantomSpirit>(),
                    ModContent.NPCType<PhantomSpiritM>(),
                    ModContent.NPCType<PhantomSpiritS>(),
                    ModContent.NPCType<PhantomSpiritL>(),
                });
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, randomSoul);
                }
                else
                {
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, -1, -1, null, player.whoAmI, randomSoul, 0f, 0f, 0, 0, 0);
                }
            }
        }
        public override void AI()
        {
            Projectile.ai[0]++;
            Projectile.rotation += 5f;
            if (Projectile.ai[0]>4)
            {
                for(int i = 0; i < 2; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position+(new Vector2(Main.rand.Next(-6,-2), Main.rand.Next(-6, -2))), Projectile.width, Projectile.height, 297, 0f, 0f, 0, default(Color), 1f);
                    dust.velocity *= 0.1f;
                    dust.scale = 2f;
                    dust.noGravity = true;
                    dust.noLight = false;
                    dust.rotation += 5f;
                }
     
            }
            CalamityUtils.HomeInOnNPC(Projectile, !Projectile.tileCollide, 600f, 12f, 12f);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
			modifiers.FinalDamage *= 1f - 0.25f*(3 - Projectile.penetrate);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return base.GetAlpha(lightColor);
        }

    }
}



