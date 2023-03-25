using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameContent;
using System.Collections.Generic;

namespace Denier.mainContent.rifleUI {
	internal class rUI : UIState {
		private UIText Counter;
		private UIElement area;

		public override void OnInitialize() {
			area = new UIElement();
			SetRectangle(area, 0f, 0f, Main.screenWidth, Main.screenHeight);
			area.HAlign = 0.5f;
			area.VAlign = 0.5f;

			Counter = new UIText(" ", 1.1f);
			Counter.HAlign = 0.5f;
			Counter.VAlign = 0.5f;
			SetRectangle(Counter, rifleUISystem.playerPos.X - Main.screenWidth/2, rifleUISystem.playerPos.Y - Main.screenHeight/2 - 70, 64, 64);	

			area.Append(Counter);
			Append(area);
		}
		public override void Update(GameTime gameTime) {
			SetRectangle(Counter, rifleUISystem.playerPos.X - Main.screenWidth/2, rifleUISystem.playerPos.Y - Main.screenHeight/2 - 70, 64, 64);
			Counter.SetText(rifle.dashCount.ToString());
			if(rifle.dashCount != 10)
				Counter.TextColor = Color.White;
			else
				Counter.TextColor = Color.Red;
		}

		private void SetRectangle(UIElement uiElement, float left, float top, float width, float height) {
			uiElement.Left.Set(left, 0f);
			uiElement.Top.Set(top, 0f);
			uiElement.Width.Set(width, 0f);
			uiElement.Height.Set(height, 0f);
		}
		public override void Draw(SpriteBatch spriteBatch) {
			if (Main.LocalPlayer.HeldItem.ModItem is rifle) {
				base.Draw(spriteBatch);
			}
		}
		
	}
	class rifleUISystem : ModSystem {
		private UserInterface rifleUserInterface;
		internal rUI rifleUI;
		public void ShowMyUI() {
			rifleUserInterface?.SetState(rifleUI);
		}
		public void HideMyUI() {
			rifleUserInterface?.SetState(null);
		}
		public override void Load() {
			if (!Main.dedServ) {
				rifleUI = new();
				rifleUserInterface = new();
				rifleUserInterface.SetState(rifleUI);
			}
		}
		public static Vector2 playerPos;
		public override void UpdateUI(GameTime gameTime) {
			playerPos = Main.LocalPlayer.Center - Main.screenPosition;
			rifleUserInterface?.Update(gameTime);
		}
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
			int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
			if (resourceBarIndex != -1) {
				layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
					"Denier: DashCounter",
					delegate {
						rifleUserInterface.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
			}
		}
	}
}