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
using CalamityMod.Buffs.StatBuffs;

namespace CalamityAmmo.Accessories
{
    public class MarvelousMycelium : ModItem
    {
        public override void SetStaticDefaults()
        {
			// DisplayName.SetDefault("Marvelous Mycelium");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "Marvelous Mycelium");
			/* Tooltip.SetDefault("10% increased ranged damage\nSlightly increases all ranged projectile velocity\n" +
                "Death goes life on, and life achieves death soon");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese),
                "While unhurt, reduces defense by 5 and life regen by 1, but increases ranged damage by 6% and ranged crit chance by 9%\n" +
                "After taking damage, grants a 5-second Mushroom Symbiosis buff, during which ranged damage and crit chance reductions of 6% and 9% apply instead, no longer reducing survivability stats\n"+
                "Death carries life onward, and life becomes the fulfillment of death");*/
		}

		public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
            Item.rare = 2;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            modplayer.Mycelium = true;
        }

    }
}

