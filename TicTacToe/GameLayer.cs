using System;
using System.Collections.Generic;
using CocosSharp;

namespace TicTacToe
{
	public class GameLayer : CCLayer
	{
		Board _board;

		public GameLayer ()
		{
			// Load and instantate your assets here

			// Make any renderable node objects (e.g. sprites) children of this layer
		}

		protected override void AddedToScene ()
		{
			base.AddedToScene ();

			// Use the bounds to layout the positioning of our drawable assets
			CCRect bounds = VisibleBoundsWorldspace;

			_board = new Board(3);

			AddChild (_board);

			_board.DrawLines ();

			// Register for touch events
			var touchListener = new CCEventListenerTouchAllAtOnce ();
			touchListener.OnTouchesEnded = OnTouchesEnded;
			AddEventListener (touchListener, this);

			Schedule (t => {
				
				if (_board.Gameover) {
					EndGame ();
				}

			}, 1.0f);
		}

		void EndGame ()
		{
			// Stop scheduled events as we transition to game over scene
			UnscheduleAll();

			var gameOverScene = GameOverLayer.SceneWithScore (Window,5);
			var transitionToGameOver = new CCTransitionMoveInR (0.3f, gameOverScene);

			Director.ReplaceScene (transitionToGameOver);
		}

		void OnTouchesEnded (List<CCTouch> touches, CCEvent touchEvent)
		{
			if (touches.Count > 0) {
				var location = touches [0].Location;
				CCLog.Log ("Touched: {0},{1}", location.X, location.Y);
				_board.HandleTouch (location);
			}
		}

		public static CCScene GameScene (CCWindow mainWindow)
		{
			var scene = new CCScene (mainWindow);
			var layer = new GameLayer ();

			scene.AddChild (layer);

			return scene;
		}
	}
}
