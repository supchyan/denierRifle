using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace Denier.mainContent.rifleCase {
	public class caseItem : ModItem {
		public override void SetStaticDefaults() {

			DisplayName.SetDefault("Box Call");
			Tooltip.SetDefault(" ");
		}	
		public override void SetDefaults() {
			Item.width = 64;
			Item.height = 64;
			Item.scale = 1f;
			Item.rare = ItemRarityID.Gray;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.shoot = ItemID.Grenade;
			Item.useAnimation = 30;
			Item.useTime = 30;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			caseTop.canBePicked = false;
			Projectile.NewProjectile(source, player.Center + new Vector2 (70f*player.direction, -500f), velocity, ModContent.ProjectileType<caseBottom>(), damage, knockback, player.whoAmI);
			Projectile.NewProjectile(source, player.Center + new Vector2 (70f*player.direction, -500f), velocity, ModContent.ProjectileType<caseTop>(), damage, knockback, player.whoAmI);
			return false;
		}
	}
	public class gaymer : ModPlayer {
		
	}
}
