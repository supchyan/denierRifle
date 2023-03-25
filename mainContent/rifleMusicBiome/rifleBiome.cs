using Terraria;
using Terraria.ModLoader;
namespace Denier.mainContent.rifleMusicBiome {
	public class rifleBiome : ModBiome {
		public override int Music => MusicLoader.GetMusicSlot(Mod, " "); // "Music/littleVrumbling"
        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;
        public static bool musicStarted = false;
		public override bool IsBiomeActive(Player player) {
            
            if (player.HeldItem.ModItem is rifle && Main.mouseRight) {
                return true;
            }  
            else {
                return false;
            }           
		}
	}
}