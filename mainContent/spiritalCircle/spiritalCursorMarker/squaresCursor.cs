using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace Denier.mainContent.spiritalCircle.spiritalCursorMarker {
    public class squaresCursor : ModProjectile {
        SoundStyle tickSound = new SoundStyle("Denier/Sounds/tick");
        public override void SetDefaults() {
            Projectile.width = 128;
            Projectile.height = 128;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.tileCollide = false;
            Projectile.Opacity = 0.8f;
        }
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Forbitten Magic");
        }
        public static float oldRot;
        public override void AI() {
            Projectile.timeLeft = 2;
            if (Main.myPlayer == Projectile.owner)
                Projectile.netUpdate = true;
            Player player = Main.player[Projectile.owner];
            Projectile.position = Main.MouseWorld - new Vector2(Projectile.width/2, Projectile.height/2);
            Projectile.rotation = MathHelper.ToRadians(Projectile.ai[0]);
            while(Projectile.ai[1] <= 20f) {
                Projectile.scale = Projectile.ai[1]/20f;
                break;
            }
            oldRot = Projectile.rotation;
            Projectile.ai[0]++;
            Projectile.ai[1]++;

            if ((Projectile.ai[0] + 2) % 45/2 == 0) {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity, ModContent.ProjectileType<squaresOutCursor>(), 0, 0, player.whoAmI);
            }
            if(squares.canShoot) {
                Projectile.ai[0] = 0;
                Projectile.rotation = MathHelper.ToRadians(45);
            }
        
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