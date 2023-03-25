using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Denier.mainContent.rifleCase {
    public class caseTop : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 66;
            Projectile.height = 20;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.friendly = true;
            Projectile.netImportant = true;
        }
        public static bool canBePicked = false;
        public static float t = 20f;
        public override void AI() {
            Projectile.netUpdate = true;
            Player player = Main.player[Projectile.owner];
            Projectile.ai[0]++;
            if(!booling)
                Projectile.velocity = new Vector2(0, (Projectile.ai[0]));
            else if (Projectile.ai[1] <= 60) {
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Vector2.Zero, 0.5f);
                Projectile.ai[1]++;
            }
            else {
                t -= 0.5f;
                while (t >= 0) {
                    Projectile.velocity = Projectile.Center - new Vector2(Projectile.Center.X, (Projectile.Center.Y + t));
                    Projectile.rotation += MathHelper.ToRadians(5);
                    break;
                }
            }
            if (t < 1)
                Projectile.Kill();
        }
        public override void Kill(int timeLeft) {
            canBePicked = true;
            t = 20f;
            Player player = Main.player[Projectile.owner];
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileID.SolarWhipSwordExplosion, 0, 0, player.whoAmI);
        }
        bool booling = false;
        public override bool OnTileCollide(Vector2 oldVelocity) {
            booling = true;
            return false;
        }
    }
}