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
    public class FossilArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fossil Arrow");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "化石箭");
            // Tooltip.SetDefault(IsChinese() ? "坚固的化石击碎敌人的防御\n酸腐的化石释放积年的余毒" : "Solid fossils smash the enemy's defense and cause sulphuric poisoning");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Ископаемая Стрела");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Прочные окаменелости сокрушают оборону врага \nГниющие в кислоте окаменелости высвобождают годы остаточного яда");
        }
        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 2.5f;
            Item.value = Item.buyPrice(0, 0, 14, 0);
            Item.rare = 5;
            Item.shoot = ModContent.ProjectileType<_FossilArrow>();
            Item.shootSpeed = 3.85f;
            Item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(25);
            recipe.AddRecipeGroup(RecipeGroupID.Wood,25);
            recipe.AddIngredient(ModContent.ItemType<CorrodedFossil>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

    }
}

