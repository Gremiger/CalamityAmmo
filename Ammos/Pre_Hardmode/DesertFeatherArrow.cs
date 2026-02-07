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
using CalamityAmmo.Misc;

namespace CalamityAmmo.Ammos.Pre_Hardmode
{
    public class DesertFeatherArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Desert Feather Arrow");
            //DisplayName.AddTranslation(Terraria.Localization.GameCulture.FromCultureName(Terraria.Localization.GameCulture.CultureName.Chinese), "荒漠羽箭");
            // Tooltip.SetDefault("When the feather was still on the vulture, it wasn't so fast ");
            //Tooltip.AddTranslation(Terraria.Localization.GameCulture.FromCultureName(Terraria.Localization.GameCulture.CultureName.Chinese), "“它们还在秃鹰身上的时候可没这么快啊”");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Стрела пустынного пера");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Когда перо все еще было на стервятнике, это было не так быстро");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }
        public override void SetDefaults()
        {
            Item.damage = 11;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 0f;
            Item.value = Item.buyPrice(0, 0, 0, 35);
            Item.rare = ItemRarityID.Green;
            Item.shoot = ModContent.ProjectileType<_DesertFeatherArrow>();
            Item.shootSpeed = 4.5f;
            Item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(20);
            recipe.AddIngredient(ItemID.WoodenArrow, 20);
            recipe.AddIngredient(ModContent.ItemType<DesertFeather>(), 1);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}
