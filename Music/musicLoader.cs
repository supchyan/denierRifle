using Terraria.ModLoader;

namespace denier.Music {
	public sealed class ManualMusicRegistrationExample : ILoadable {
		public void Load(Mod mod) {

			MusicLoader.AddMusic(mod, "Music/littleVrumbling");

		}

		public void Unload() { }
	}
}