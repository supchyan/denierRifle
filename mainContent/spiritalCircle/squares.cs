using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace Denier.mainContent.spiritalCircle {
    public class squares : ModProjectile {
        SoundStyle tickSound = new SoundStyle("Denier/Sounds/tick");
        SoundStyle noteSound = new SoundStyle("Denier/Sounds/note");
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

        public static float oldRot;
        public static bool canShoot = false;
        public override void AI() {
            Projectile.timeLeft = 2;
            if (Main.myPlayer == Projectile.owner)
                Projectile.netUpdate = true;
            Player player = Main.player[Projectile.owner];
            Projectile.position = player.Center - new Vector2(Projectile.width/2, Projectile.height/2);
            Projectile.rotation = oldRot + MathHelper.ToRadians(Projectile.ai[0]);
            while(Projectile.ai[1] <= 20f) {
                Projectile.scale = Projectile.ai[1]/20f;
                break;
            }
            Projectile.ai[0]++;
            Projectile.ai[1]++;

            if ((Projectile.ai[0] + 2) % 45/2 == 0 && !canShoot) {
                canShoot = true;
                oldRot = Projectile.rotation;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center - new Vector2(Projectile.width/2, Projectile.height/2), Projectile.velocity, ModContent.ProjectileType<squaresOut>(), 0, 0, player.whoAmI);
                SoundEngine.PlaySound(noteSound with {Volume = 2f}, Main.MouseWorld);
            }
            if(canShoot) {
                Projectile.ai[0] = 0;
                Projectile.rotation = oldRot;
            }
        
            if(!Main.mouseRight || player.dead) {
                Projectile.Opacity -= 0.2f;
                Projectile.scale += 0.1f;
            }
                
            if(player.HeldItem.ModItem is not rifle || Projectile.Opacity <= 0.2f)
                Projectile.Kill();
        }
        public override Color? GetAlpha(Color lightColor) {
            if(canShoot)
			    return new Color(255, 0, 0, 255) * Projectile.Opacity;
            else
                return new Color(255, 255, 255, 255) * Projectile.Opacity;

		}
    }
}