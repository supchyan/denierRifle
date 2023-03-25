using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Denier.mainContent.spiritalCircle {
    public class squaresOut : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 128;
            Projectile.height = 128;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.scale = 1f;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
            Projectile.tileCollide = false;
            Projectile.Opacity = 2f;
        }
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Forbitten Magic");
        }
        float lifeTime = 30f;
        public override void AI() {
            Projectile.timeLeft = 2;
            if (Main.myPlayer == Projectile.owner)
                Projectile.netUpdate = true;
            Player player = Main.player[Projectile.owner];
            Projectile.position = player.Center - new Vector2(Projectile.width/2, Projectile.height/2);
            Projectile.rotation = squares.oldRot;

            double lerpValue = Projectile.ai[1]/lifeTime;
            if (Projectile.ai[1] <= lifeTime) {
                Projectile.scale = MathHelper.Lerp(Projectile.scale, 2f, (float)Math.Sqrt(lerpValue)/2);
                Projectile.Opacity = MathHelper.Lerp(Projectile.Opacity, 0, (float)Math.Sqrt(lerpValue)/2);
            }
            if (Projectile.Opacity <= 0.1f) {
                Projectile.Kill();
            }
            Projectile.ai[1]++;
        }
        public override void OnSpawn(IEntitySource source) {
            Player player = Main.player[Projectile.owner];
            Projectile.rotation = squares.oldRot;
            Projectile.position = player.Center - new Vector2(Projectile.width/2, Projectile.height/2);
        }
        public override Color? GetAlpha(Color lightColor) {
            return new Color(255, 0, 0, 255) * Projectile.Opacity;

		}
    }
}