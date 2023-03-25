using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace Denier.mainContent {
    public class rifleBullet : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 25;
            Projectile.height = 25;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.netImportant = true;
        }
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Denier Bullet");
        }
        public override void AI() {
            if (Main.myPlayer == Projectile.owner)
                Projectile.netUpdate = true;
            Player player = Main.player[Projectile.owner];
            Projectile.damage = 444;
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.velocity = Projectile.velocity - new Vector2(0,0).DirectionTo(Projectile.Center - Main.MouseWorld);
            Projectile.ai[0]++;
            Dust shotTrail = Dust.NewDustDirect(Projectile.Center, Projectile.width, 0, DustID.PortalBolt, Projectile.velocity.X/2, Projectile.velocity.Y/2, 255, Color.White, 1f);
            shotTrail.noGravity = true;
            while (Projectile.ai[0] <= 3) {
                Lighting.AddLight(Projectile.Center, 1f, 1f, 1f);
                break;
            }
            if(Projectile.ai[0] >= 60)
                Projectile.Kill();
        }
        public override void OnSpawn(IEntitySource source) {
            if(rifle.dashCount < 9)
                rifle.dashCount+=2;
            else if (rifle.dashCount == 9)
                rifle.dashCount++;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            target.life = (int)Math.Round((float)target.life*0.62f);
            if(!target.active) {
                for (int i = 0; i < 50; i++) {
                Vector2 gigaVelocity = Main.rand.NextVector2CircularEdge(target.width, target.width);
                Dust killDust = Dust.NewDustPerfect(target.Center, DustID.PortalBolt, gigaVelocity, 255, Color.White, 1f);
                killDust.noGravity = true;
                }
                if(rifle.dashCount < 10)
                rifle.dashCount = 10;
            }
            if(rifle.dashCount < 9)
                rifle.dashCount+=2;
            else if (rifle.dashCount == 9)
                rifle.dashCount++;
        }
        public override Color? GetAlpha(Color lightColor) {
			return new Color(255, 255, 255, 255) * Projectile.Opacity;
		}
    }
}