using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.Localization;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityAmmo.Projectiles.Hardmode;

namespace CalamityAmmo.Projectiles.Post_MoonLord
{
    public class HealingStarcloud : ModProjectile
    {
        public Player Owner
        {
            get
            {
                return Main.player[Projectile.owner];
            }
        }
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Healing Nebula");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "治疗星云");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Исцеляющее Туманность");
            Main.projFrames[Projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            Projectile.width = 46; // The width of projectile hitbox
            Projectile.height = 26; // The height of projectile hitbox
            Projectile.friendly = false ; // Can the projectile deal damage to enemies?
            Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
            Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false; // Can the projectile collide with tiles?
            Projectile.penetrate = -1; // Look at comments ExamplePiercingProjectile
            Projectile.alpha = 0; // How transparent to draw this projectile. 0 to 255. 255 is completely transparent.
            Projectile.timeLeft = 300;
        }

        // Allows you to determine the color and transparency in which a projectile is drawn
        // Return null to use the default color (normally light and buff color)
        // Returns null by default.
        public override Color? GetAlpha(Color lightColor)
        {
            // return Color.White;
            return null;
        }

        public override void AI()
        {
            Projectile.ai[0] += 1;
            Projectile.velocity *= 0; 
            Vector2 destination = Owner.Center + Vector2.UnitY * (Owner.gfxOffY - 150f);
            if (Owner.gravDir == -1f)
            {
                destination.Y += 300f;
            }
            Projectile.Center = Vector2.Lerp(Projectile.Center, destination, 0.36f);
            Projectile.position = Utils.Floor(Projectile.position);
            if (Projectile.ai[0] % 10 == 0 && Main.myPlayer == Projectile.owner)
            {
                int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(),
                    Projectile.Center + new Vector2(Main.rand.Next(-8, 32), 14),
                    new Vector2(0, 10),ModContent.ProjectileType<HealingRain>(), Projectile.damage, Projectile.knockBack,
                    Main.myPlayer);
                Main.projectile[proj].timeLeft = 180;
                Main.projectile[proj].netUpdate = true;
            }

            if (Projectile.timeLeft < 60)
            {
                Projectile.ai[0] = 0;
                Projectile.ai[1] += 1;
                Projectile.alpha = (int)((Projectile.ai[1] / 60) * 255);
            }
            if (++Projectile.frameCounter >= 8)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = ++Projectile.frame % Main.projFrames[Projectile.type];
            }
            
        }

        
    }
    public class HealingRain : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Healing Rain");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "治疗之雨");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Целебный дождь");

        }

        public override void SetDefaults()
        {
            Projectile.width = 30; // The width of projectile hitbox
            Projectile.height = 30; // The height of projectile hitbox
            Projectile.friendly = false; // Can the projectile deal damage to enemies?
            Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
            Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false; // Can the projectile collide with tiles?
            Projectile.penetrate = -1; // Look at comments ExamplePiercingProjectile
            Projectile.alpha = 0; // How transparent to draw this projectile. 0 to 255. 255 is completely transparent.
            
        }
        public override void AI()
        {
                Rectangle selfbox = new((int)Projectile.position.X, (int)Projectile.position.Y,
                      Projectile.width, Projectile.height);
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player player = Main.player[i];
                    if (player.team == Main.player[Projectile.owner].team && player.active == true)
                    {
                        Rectangle targetbox = new((int)player.position.X, (int)player.position.Y,
                            player.width, player.height);
                        if (selfbox.Intersects(targetbox))
                        {
                        player.AddBuff(ModContent.BuffType<Heal>(), 120);
                        Projectile.penetrate = 0;
                    }
                    }
                
            }
        }
    }
    public class Heal:ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Healing");
            // Description.SetDefault("Life regeneration speed increased  \nYou are lucky today, worm... No, it's nothing. You misheard");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "治疗之雨");
            //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Исцеление");
            //Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Russian), "Увеличена скорость регенерации жизни\nТебе сегодня повезло, червяк... Нет, ничего особенного. Вы ослышались");
            //Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Chinese), "生命再生速度提高\n今天算你走运，虫......不，没什么，你听错了");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 8;
        }
    }
}
