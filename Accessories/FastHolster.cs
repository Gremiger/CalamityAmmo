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
using CalamityMod.Items.Armor.Wulfrum;
using CalamityMod.CalPlayer;

namespace CalamityAmmo.Accessories
{
    public class FastHolster : ModItem
    {
        public override void SetStaticDefaults()
        {
			// DisplayName.SetDefault("Fast Holster ");
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "快拔枪套");
			/* Tooltip.SetDefault("Decrease usetime of guns by 10%\nDerease ranged critchance by 4%\n" +
                "Don't worry about your accuracy. Take enough shots, and you're guaranteed to land something!"); 
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), 
                "枪械的使用时间减少10%\n远程暴击率减少4%\n" +
                "不要在乎打得准不准，只要打得多，就是打得准！");*/
		}

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 32;
            Item.value = Item.buyPrice(0, 18, 0, 0);
            Item.rare = 3;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            modplayer.Holster= true;
		}

    }
}
