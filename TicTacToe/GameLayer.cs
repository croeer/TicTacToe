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

			_board = new Board(this,3);

			AddChild (_board);

			_board.DrawLines ();

			// Register for touch events
			var touchListener = new CCEventListenerTouchAllAtOnce ();
			touchListener.OnTouchesEnded = OnTouchesEnded;
			AddEventListener (touchListener, this);
		}

		void OnTouchesEnded (List<CCTouch> touches, CCEvent touchEvent)
		{
			if (touches.Count > 0) {
				var location = touches [0].Location;
				CCLog.Log ("Touched: {0},{1}", location.X, location.Y);
				_board.HandleTouch (location);
			}
		}
	}
}
