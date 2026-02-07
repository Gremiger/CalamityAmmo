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


namespace CalamityAmmo.Ammos.Post_MoonLord
{
    public class GoldenFeatherArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Golden Feather Arrow");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "金羽箭");
            // Tooltip.SetDefault("May attract something small \nSummon red lightning when not critically hit");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "可能会吸引某些小东西\n击中敌人且未暴击时召唤红色的闪电");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Золотого пера Стрела");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Может привлечь что-то маленькое \nвызвать красную молнию, когда не нанесен критический удар");
        }
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 2.5f;
            Item.value = Item.buyPrice(0, 0, 3, 0);
            Item.rare = ItemRarityID.Purple;
            Item.shoot = ModContent.ProjectileType<_GoldenFeatherArrow>();
            Item.shootSpeed = 3.85f;
            Item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes()
        {
            CreateRecipe(142)
            .AddIngredient(ItemID.GoldBird, 1)
            .AddTile(TileID.LunarCraftingStation)
            .Register();

            var recipe = CreateRecipe();
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EffulgentFeather>(), 1);
            recipe.AddIngredient(ItemID.RichMahogany, 11);
            recipe.AddCondition(new Condition(Language.GetTextValue(""), () => DownedBossSystem.downedDragonfolly));
            recipe.ReplaceResult(ModContent.ItemType<GoldenFeatherArrow>(),233);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

    
    }
}


