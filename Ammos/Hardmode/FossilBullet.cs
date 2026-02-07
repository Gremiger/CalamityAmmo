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
using CalamityAmmo.Projectiles.Post_MoonLord;
using CalamityMod.Projectiles.Melee;

namespace CalamityAmmo.Ammos.Hardmode
{
    public class FossilBullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fossil Bullet");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "化石子弹");
            // Tooltip.SetDefault(IsChinese() ? "坚固的化石击碎敌人的防御\n酸腐的化石释放积年的余毒" : "Solid fossils smash the enemy's defense and cause sulphuric poisoning");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Ископаемая пуля");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Прочные окаменелости сокрушают оборону врага \nГниющие в кислоте окаменелости высвобождают годы остаточного яда");
        }
        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 8;
            Item.height = 8;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 1f;
            Item.value = Item.buyPrice(0, 0, 7, 0);
            Item.rare = 5;
            Item.shoot = ModContent.ProjectileType<_FossilBullet>();
            Item.shootSpeed = 5f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(ItemID.EmptyBullet, 50);
            recipe.AddIngredient(ModContent.ItemType<CorrodedFossil>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

    }
}

