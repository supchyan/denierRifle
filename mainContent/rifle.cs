using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using System.Collections.Generic;
using Denier.mainContent.rifleMusicBiome;
using Denier.mainContent.spiritalCircle;
using Denier;
using System.IO;
using Terraria.ModLoader.IO;

namespace Denier.mainContent {
    public class rifle : ModItem {

        SoundStyle shotSound = new SoundStyle("Denier/Sounds/shot");
        SoundStyle bassSound = new SoundStyle("Denier/Sounds/bass");
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Denier T.D.S.R.");
            Tooltip.SetDefault(
            "Uses ∞ mana." +
            "\nUses ∞ life." +
            "\nReduces enemy's current HP by 38%." +
            "\nUnlike normal guns, this one can shoot only when in [c/c12120:Scoping Mode]." +
            "\n[c/c12120:Scoping Mode]: Hold Right Click to charge a weapon. Left Click to take a shot." +
            "\nOwner can't move while in [c/c12120:Scoping Mode]." +
            "\nTaking a shot grants a player [c/c12120:Dash Point] (upon to " + dashCount + "). Killing an enemy maxes owner's [c/c12120:Dash Points]." +
            "\n[c/c12120:Dash Point]: Left Click when not in [c/c12120:Scoping Mode] to spend a [c/c12120:Dash Point] to make a dash." +
            "\nThis weapon was born from the ashes of war. The Fallen Angel had been using this until you found it." +
            "\nBest magic invention of the Divine Blacksmith." +
            "\n[c/c12120:It's going to feel a pain!]");
        }
        public override void SetDefaults() {
            Item.damage = 44;
            Item.width = 65;
            Item.height = 17;
            Item.scale = 2f;
            Item.DamageType = DamageClass.Magic;
            Item.crit = 95;
            Item.noMelee = true;
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.knockBack = 10;
            Item.rare = 0;
            Item.shoot = ModContent.ProjectileType<rifleBullet>();
            Item.shootSpeed = 40f;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips) {            
            foreach (TooltipLine line2 in tooltips) {
				if (line2.Mod == "Terraria" && line2.Name == "Damage") {
					line2.OverrideColor = Main.errorColor;
                    line2.Text = "∞ magic damage";
				}
			}
        }
        public static int dashCount = 10;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            
            Item.autoReuse = false;
            
            if (Main.mouseRight && player.statMana >= 10 && squares.canShoot) {
                player.statMana -= 50;
                squares.canShoot = false;
                SoundEngine.PlaySound(shotSound with {MaxInstances = 3});
                return true;
            }
            else if (!Main.mouseRight && dashCount > 0) {
                dashCount--;
                for (int i = 0; i < 50; i++) {
                    Vector2 gigaVelocity = Main.rand.NextVector2Unit((player.Center - Main.MouseWorld).ToRotation() - MathHelper.Pi/8, (float)MathHelper.Pi / 4) * 20;
                    Dust dashDust = Dust.NewDustPerfect(player.Center, DustID.PortalBolt, -gigaVelocity, 255, Color.White, 1f);
                    dashDust.noGravity = true;
                }
                player.velocity = player.velocity.DirectionTo((player.Center - Main.MouseWorld))*20f;
                SoundEngine.PlaySound(bassSound with {MaxInstances = 3});
                return false;
            }
            else
                return false;
        }
        public override void HoldItem(Player player) {
            if (player.HeldItem.ModItem is not rifle)
                return;
            if (Main.mouseRight) {
                Main.cursorScale = 0;
                if(player.statMana > 10) {
                    player.statMana--;
                }   
                else {
                    player.statLife--;
                    if (player.manaFlower)
                        player.QuickMana();
                }
                if(player.statLife <= 0)
                    player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " were slain..."), 9999, player.direction);
                if (squares.canShoot)                
                    Lighting.AddLight(Main.MouseWorld, 1f, 0f, 0f);
                else
                    Lighting.AddLight(Main.MouseWorld, 1f, 1f, 1f);
                if (player.velocity != Vector2.Zero)
                    player.velocity.Normalize();
                player.position = player.oldPosition;
                zoom = true;
                if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[ModContent.ProjectileType<mainCircle>()] <= 0)
                    Projectile.NewProjectile(Projectile.GetSource_None(), player.Center, player.velocity, ModContent.ProjectileType<mainCircle>(), 10, 10, player.whoAmI);                    
            }
            else if(!Main.mouseRight && squares.canShoot) {
                squares.canShoot = false;
            }
        }
        public static bool zoom = false;
        public override Vector2? HoldoutOffset() {
			return new Vector2(-13f, -1f);
		}
        public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.RangerEmblem)
				.AddIngredient(ItemID.DemonHeart)
				.AddTile(TileID.Anvils)
				.Register();
		}
    }
    public class playerStuff : ModPlayer {
        public override void ModifyScreenPosition() {
            if (rifle.zoom) {
            Main.screenPosition = Main.MouseWorld - new Vector2(Main.screenWidth/2, Main.screenHeight/2);
            rifle.zoom = false;
            }
        }
        public override void PreUpdate() {
            if (Main.LocalPlayer.HeldItem.ModItem is not rifle) {
                rifle.dashCount = 0;
            }
        }
        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer) {
			ModPacket packet = Mod.GetPacket();
			packet.Write((byte)Player.whoAmI);
		    packet.Send(toWho, fromWho);
		}
        public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo) {

        }
        ////////////////////////////////////////////////////////////////////
        //    HERE IS NON WORKING METRONOME HAHA LOSER LOL KEEK ... ;-;   //
        ////////////////////////////////////////////////////////////////////
        // static float bpm = 145f / 2;
        // static float bpt = bpm /  Main.frameRate / Main.frameRate;
        // float beatInterval = bpt; // which cooldown between hitSounds while song is playing
        // static float beatTick = 0;
        // static int counter = 0;
        // static float timer = 0;
        // public override void PreUpdate() {

        //     if (rifleBiome.musicStarted) { // musicStarted had removed from biome class
        //         if (beatTick >= beatInterval) {
        //             beatTick -= beatInterval;
        //         }
        //         else {
        //             beatTick++;
        //             SoundEngine.PlaySound(SoundID.DrumKick, Main.LocalPlayer.Center);
        //         }
        //     }
        // }
        ////////////////////////////////////////////////////////////////////
    }
}