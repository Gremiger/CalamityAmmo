using System;
using CalamityMod;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static System.Formats.Asn1.AsnWriter;

namespace CalamityAmmo.Misc
{
    public class Focus: ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Focus");
            // Description.SetDefault("I can do this all day");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "专注");
            //Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "我可以耗上一整天");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            CaePlayer modplayer = player.GetModPlayer<CaePlayer>();
            if(player.HeldItem.useAmmo == AmmoID.Bullet)
            {
                player.scope = true;
                modplayer.Aimed = true;
            }
        }
    }
}
