using System;
using System.Collections.Generic;
using CocosSharp;

namespace TicTacToe
{
	public class GameOverLayer : CCLayerColor
	{

		string scoreMessage = string.Empty;

		public GameOverLayer (Board.State? winner)
		{

			var touchListener = new CCEventListenerTouchAllAtOnce ();
			touchListener.OnTouchesEnded = (touches, ccevent) => Window.DefaultDirector.ReplaceScene (GameLayer.GameScene (Window));

			AddEventListener (touchListener, this);

			if (!winner.HasValue) {
				scoreMessage = "Game Over, but it ended Remis.";
			} else {
				scoreMessage = String.Format ("Game Over. {0} has won!", winner.Value.ToString());
			}

			Color = new CCColor3B (CCColor4B.Black);

			Opacity = 255;
		}
			
		protected override void AddedToScene ()
		{
			base.AddedToScene ();

			Scene.SceneResolutionPolicy = CCSceneResolutionPolicy.ShowAll;

			var scoreLabel = new CCLabel (scoreMessage, "arial", 50) {
				Position = new CCPoint (VisibleBoundsWorldspace.Size.Center.X, VisibleBoundsWorldspace.Size.Center.Y + 50),
				Color = new CCColor3B (CCColor4B.Yellow),
				HorizontalAlignment = CCTextAlignment.Center,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddle
			};

			AddChild (scoreLabel);

			var playAgainLabel = new CCLabel("Tap to Play Again", "arial", 50) {
				Position = VisibleBoundsWorldspace.Size.Center,
				Color = new CCColor3B (CCColor4B.Green),
				HorizontalAlignment = CCTextAlignment.Center,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddle,
			};

			AddChild (playAgainLabel);

		}

		public static CCScene SceneWithWinner (CCWindow mainWindow, Board.State? winner)
		{
			var scene = new CCScene (mainWindow);
			var layer = new GameOverLayer (winner);

			scene.AddChild (layer);

			return scene;
		}
	}
}