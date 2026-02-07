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
using CalamityMod.Items.Placeables;
using CalamityMod.Items.Placeables.FurnitureMonolith;
using CalamityMod.Items.Placeables.Astral;

namespace CalamityAmmo.Ammos.Hardmode
{
    public class WeakAstralArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("WeakAstralArrow");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "尘星箭");
            // Tooltip.SetDefault(IsChinese() ? "蠕虫说，“是的我有很多父亲”" : "The worm said, \"Yes, I have many fathers.\"");
            ////Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "在打败白金星舰后升级\n在打败星神游龙会再次升级");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Слабая Астральная Стрела");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Мощности недостаточно...\nБудет обновляться после поражения Аструм Деуса");
        }
        public override void SetDefaults()
        {
            Item.damage = 11;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 2.5f;
            Item.value = Item.buyPrice(0, 0, 1, 0);
            Item.rare = 4;
            Item.shoot = ModContent.ProjectileType<_WeakAstralArrow>();
            Item.shootSpeed = 4f;
            Item.ArmorPenetration = 5;
            Item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(33);
            recipe.AddIngredient(ModContent.ItemType< StarblightSoot > (), 1);
            recipe.AddIngredient(ModContent.ItemType<AstralStone>(), 1);
            recipe.AddIngredient(ModContent.ItemType<AstralMonolith>(), 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        public override void UpdateInventory(Player player)
        {
            int i = Item.stack;
            if (DownedBossSystem.downedAstrumAureus)
            {
                Item.SetDefaults(ModContent.ItemType<AstralArrow>(), true);
                Item.stack = i;
            }
        }

        
    }
}
