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

namespace CalamityAmmo.Ammos.Hardmode
{
    public class DazzlingAstralBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dazzling Astral Bullet");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "炫星子弹");
            // Tooltip.SetDefault(IsChinese() ? "行星，恒星，力量就在其中！" : "planet，star，how powerful they are");
            ////Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "在打败白金星舰后升级\n在打败星神游龙会再次升级");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Ослепительная Астральная пуля");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "планета, звезда, насколько они могущественны");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }
        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.value = Item.buyPrice(0, 0, 1, 0);
            Item.rare = 9;
            Item.rare = ItemRarityID.Lime;
            Item.shoot = ModContent.ProjectileType<_DazzlingAstralBullet>();
            Item.shootSpeed = 4f;
            Item.ArmorPenetration = 5;
            Item.ammo = AmmoID.Bullet;
        }

        public override void UpdateInventory(Player player)
        {
            int i = Item.stack;
            if (!DownedBossSystem.downedAstrumDeus)
            {
                Item.SetDefaults(ModContent.ItemType<AstralBullet>(), true);
                Item.stack = i;
            }
        }


    }
}
