using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Denier.mainContent.rifleCase {
    public class caseBottom : ModProjectile {
        public override void SetDefaults() {
            Projectile.width = 66;
            Projectile.height = 16;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.friendly = true;
            Projectile.netImportant = true;
        }
        public override void AI() {
            Projectile.netUpdate = true;
            Player player = Main.player[Projectile.owner];
            Projectile.ai[0]++;
            if(!booling)
                Projectile.velocity = new Vector2(0, (Projectile.ai[0]));
            else
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, Vector2.Zero, 0.5f);
            if (caseTop.canBePicked && Projectile.Hitbox.Intersects(player.getRect()))
                Projectile.Kill();
        }
        bool booling = false;
        public override bool OnTileCollide(Vector2 oldVelocity) {
            booling = true;
            return false;
        }
        public override void Kill(int timeLeft) {
            Player player = Main.player[Projectile.owner];
            Item.NewItem(Projectile.GetSource_DropAsItem(), player.Center, ModContent.ItemType<rifle>());
        }
    }
}