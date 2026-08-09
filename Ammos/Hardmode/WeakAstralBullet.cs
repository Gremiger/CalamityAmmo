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
using CalamityMod;
using CalamityMod.Projectiles.Ranged;
using static CalamityAmmo.CAEUtils;
using CalamityAmmo.Projectiles.Hardmode;

namespace CalamityAmmo.Ammos.Hardmode
{
    public class WeakAstralBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
                // DisplayName.SetDefault("Weak Astral Bullet");
                //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "Dust Star Bullet");
                // Tooltip.SetDefault(IsChinese() ? "The power is not enough......\nEvolves after defeating the mechanical behemoth corrupted by the astral infection" : "The power is not enough...\nWill upgrade after Astrum Aureus is defeated");
               ////Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "Upgrades after defeating the Platinum Starship\nUpgrades again after defeating Astrum Deus");
                Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Слабая Астральная пуля ");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Мощности недостаточно...\nБудет обновляться после поражения Аструм Деуса");
        }
        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 1.5f;
            Item.value = Item.buyPrice(0, 0, 1, 0);
            Item.rare = 4;
            Item.shoot = ModContent.ProjectileType<_WeakAstralBullet>();
            Item.shootSpeed = 4f;
            Item.ArmorPenetration = 5;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(ModContent.ItemType<StarblightSoot> (), 1);
            recipe.AddIngredient(ItemID.EmptyBullet, 50);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
        
        public override void UpdateInventory(Player player)
        {
            int i = Item.stack;
            if (DownedBossSystem.downedAstrumAureus)
            {
             Item.SetDefaults(ModContent.ItemType<AstralBullet >(),true);
             Item.stack = i;
            }
        }

    }
}
