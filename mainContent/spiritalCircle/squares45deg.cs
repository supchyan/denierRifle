using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Denier.mainContent.spiritalCircle {
    public class squares45deg : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 128;
            Projectile.height = 128;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.tileCollide = false;
            Projectile.Opacity = 0.8f;
        }
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Forbitten Magic");
        }

        float oldRot;
        public override void AI() {
            Projectile.timeLeft = 2;
            if (Main.myPlayer == Projectile.owner)
                Projectile.netUpdate = true;
            Player player = Main.player[Projectile.owner];
            Projectile.position = player.Center - new Vector2(Projectile.width/2, Projectile.height/2);
            Projectile.rotation = squares.oldRot + MathHelper.ToRadians(-Projectile.ai[0]) + MathHelper.ToRadians(45);
            while(Projectile.ai[1] <= 20f) {
                Projectile.scale = Projectile.ai[1]/20f;
                break;
            }

            Projectile.ai[0]++;
            Projectile.ai[1]++;

            if(squares.canShoot) {
                Projectile.ai[0] = 0;
            }

            Lighting.AddLight(Projectile.Center, 1f, 1f, 1f);
        
            if(!Main.mouseRight || player.dead) {
                Projectile.Opacity -= 0.2f;
                Projectile.scale += 0.1f;
            }
                
            if(player.HeldItem.ModItem is not rifle || Projectile.Opacity <= 0.2f)
                Projectile.Kill();
        }
        public override Color? GetAlpha(Color lightColor) {
            if(squares.canShoot)
			    return new Color(255, 0, 0, 255) * Projectile.Opacity;
            else
                return new Color(255, 255, 255, 255) * Projectile.Opacity;

		}
    }
}