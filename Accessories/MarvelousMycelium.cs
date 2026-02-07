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
			//DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "神奇菌丝");
			/* Tooltip.SetDefault("10% increased ranged damage\nSlightly increases all ranged projectile velocity\n" +
                "Death goes life on, and life achieves death soon"); 
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), 
                "未受伤时减少5点防御与1点生命再生，提升6%远程伤害与9%远程暴击率\n" +
                "受伤后给予5秒与菇共生buff，期间减少6%远程伤害与9%远程暴击，不再减少生存属性\n"+
                "死亡延续生命，生命成就死亡");*/
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

