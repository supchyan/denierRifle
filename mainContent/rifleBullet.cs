using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Audio;
using Terraria.GameContent;
using ReLogic.Content;
using Terraria.ModLoader;
using System.Collections.Generic;
using ReLogic.Utilities;

namespace denier.mainContent {
    public class rifleBullet : ModProjectile {
        SoundStyle shotSound = new SoundStyle("denier/Sounds/shotSound");
        public override void SetDefaults() {
            Projectile.width = 25;
            Projectile.height = 5;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.netUpdate = true;
        }
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Denier Shot");
        }
        public override void AI() {
            Player player = Main.player[Projectile.owner];
            Projectile.damage = 444;
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.ai[0]++;
            
            while (Projectile.ai[0] <= 3) {
                if (player.statLife != 0) {
                    var manaDust = Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height, DustID.ManaRegeneration, Projectile.velocity.X/5f, Projectile.velocity.Y/5f, 255, default, 1f);
                }
                else {
                    var bloodDust = Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height, DustID.Blood, Projectile.velocity.X, Projectile.velocity.Y, 255, default, 1f);
                }
                    

                Lighting.AddLight(Projectile.Center, 2, 0, 0);
                break;
            }
            if(Projectile.ai[0] >= 60)
                Projectile.Kill();
        }
        public override void OnSpawn(IEntitySource source) {
            SoundEngine.PlaySound(shotSound);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.life = (int)Math.Round((float)target.life*0.75f);
        }
    }
}