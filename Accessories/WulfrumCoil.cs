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

namespace CalamityAmmo.Accessories
{
    public class WulfrumCoil : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Wulfrum Coil ");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "钨钢线圈");
            // Tooltip.SetDefault("10% increased ranged damage\nSlightly increases all ranged projectile velocity");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "远程伤害+7%\n略微提升远程弹幕的飞行速度");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }
        public override void ExtractinatorUse(int extractinatorBlockType, ref int resultType, ref int resultStack)
        {
            resultType = ModContent.ItemType<WulfrumMetalScrap>();
            resultStack = Main.rand.Next(3, 6);
            if (Utils.NextFloat(Main.rand) > 0.8f)
            {
                resultStack = 1;
                resultType = ModContent.ItemType<EnergyCore>();
            }
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            modplayer.Coil = true;
            player.GetDamage<RangedDamageClass>() += 0.07f;
        }
        
    }
}
