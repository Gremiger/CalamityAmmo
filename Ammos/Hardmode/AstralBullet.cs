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
    public class AstralBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
                // DisplayName.SetDefault("Astral Bullet");
                //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "幻星子弹");
                // Tooltip.SetDefault(IsChinese() ? "击败诞生于星辰之间的恐惧以完成进化" : "Will upgrade after Astrum Deus is defeated");
            ////Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "在打败白金星舰后升级\n在打败星神游龙会再次升级");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Астральная пуля");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Будет обновляться после поражения Аструм Деуса");
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
            Item.knockBack = 2.5f;
            Item.value = Item.buyPrice(0, 0, 1, 0);
            Item.rare = 7;
            Item.shoot = ModContent.ProjectileType < _AstralBullet>();
            Item.shootSpeed = 4f;
            Item.ArmorPenetration = 5;
            Item.ammo = AmmoID.Bullet;
        }

        public override void UpdateInventory(Player player)
        {
            int i = Item.stack;
            if (DownedBossSystem.downedAstrumDeus)
            {
                Item.SetDefaults(ModContent.ItemType<DazzlingAstralBullet>(), true);
                Item.stack = i;
            }
            if (!DownedBossSystem.downedAstrumAureus)
            {
                Item.SetDefaults(ModContent.ItemType<WeakAstralBullet>(), true);
                Item.stack = i;
            }
        }


    }
}
