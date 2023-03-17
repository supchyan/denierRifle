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
    public class rifle : ModItem {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Denier T.D.S.R.");
            Tooltip.SetDefault(
            "Uses 100 mana or owner's life. Can't kill." +
            "\nThis weapon was born from the ashes of war. The Fallen Angel had been using this until you found it." +
            "\nBest ranged invention of The Divinities." +
            "\n[c/c12120:T.D.S.R. - The Divinity Sniper Rifle]");
        }
        public override void SetDefaults() {
            Item.damage = 6666;
            Item.width = 65;
            Item.height = 17;
            Item.scale = 2f;
            Item.DamageType = DamageClass.Ranged;
            Item.crit = 95;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.knockBack = 10;
            Item.rare = 10;
            Item.autoReuse = false;
            Item.shoot = ModContent.ProjectileType<rifleBullet>();
            Item.shootSpeed = 40f;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips) {            
            foreach (TooltipLine line2 in tooltips) {
				if (line2.Mod == "Terraria" && line2.Name == "Damage") {
					line2.OverrideColor = Main.errorColor;
                    line2.Text = "Scales from ranged damage";
				}
			}
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            if (player.statMana < 100) {
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " couldn't curb that power."), 9999, player.direction, false);
            }
            else {
                player.statMana -=100;
            }
            player.velocity = player.velocity.DirectionTo((player.Center - Main.MouseWorld))*20f;
            return true;
        }
        public override void HoldItem(Player player) {
            if (Main.LocalPlayer.HeldItem.ModItem is not rifle)
				return;
            if (Main.mouseRight) {
                Lighting.AddLight(Main.MouseWorld, 4,0,0);
                Main.LocalPlayer.velocity = Vector2.Zero;
                zoom = true;
            }
        }
        public static bool zoom = false;
        public override void UpdateInventory(Player player) {
            
        }
        public override Vector2? HoldoutOffset() {
			return new Vector2(-13f, -1f);
		}
    }
    public class playerStuff : ModPlayer {
        public override void ModifyScreenPosition() {
            if (rifle.zoom) {
            Main.screenPosition = Main.MouseWorld - new Vector2(Main.screenWidth/2, Main.screenHeight/2);
            rifle.zoom = false;
            }
        }
    }
}