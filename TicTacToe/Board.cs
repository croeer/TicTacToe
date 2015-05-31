using System;
using System.Collections.Generic;
using CocosSharp;
using System.Linq;

namespace TicTacToe
{
	public class Board : CCLayerColor
	{
		public enum State
		{
			Player1,
			Player2
		};

		int _size;
		public State? GameState { get; set; }

		State?[,] _boardState = new State? [3, 3];

		public bool Gameover { get; set; }

		public Board (int size) : base ()
		{
			Gameover = false;
			_size = size;
			GameState = State.Player1;
		}

		public void DrawLines ()
		{
			addHorizontalLines ();
			addVerticalLines ();
		}

		void addHorizontalLines ()
		{
			var l = new CCDrawNode ();

			for (int x = 1; x < _size; ++x) {
				var y = VisibleBoundsWorldspace.LowerLeft.Offset (
					        0, VisibleBoundsWorldspace.MaxY / _size * x);
				l.DrawSegment (y,
					y.Offset (VisibleBoundsWorldspace.MaxX, 0),
					5f,
					new CCColor4F (255f, 0f, 0f, 1f)); 
			}
			AddChild (l);

		}

		void addVerticalLines ()
		{
			var l = new CCDrawNode ();

			for (int x = 1; x < _size; ++x) {
				var y = VisibleBoundsWorldspace.LowerLeft.Offset (
					        VisibleBoundsWorldspace.MaxX / _size * x, 0);
				l.DrawSegment (y,
					y.Offset (0, VisibleBoundsWorldspace.MaxY),
					5f,
					new CCColor4F (255f, 0f, 0f, 1f)); 
			}
			AddChild (l);

		}

		public void SwitchState ()
		{
			GameState = (GameState == State.Player1) ? State.Player2 : State.Player1;
		}

		public void HandleTouch (CCPoint location)
		{

			var clickedCell = clickedPosition (location);
			if (!isValidCell (clickedCell)) {
				return;
			}

			_boardState [clickedCell.Item1, clickedCell.Item2] = GameState;

			DrawCurrentMove (cellToLocation (clickedCell));

			if (CheckForWin ()) {
				DrawWinningLine ();
				Gameover = true;
				return;
			}

			SwitchState ();
		}

		void DrawWinningLine() {
			if (!GameState.HasValue) {
				return;
			}

			// Draw
		}

		bool CheckForWin ()
		{
			for (int y = 0; y < 3; ++y) {
				if ((_boardState [0,y] == _boardState [1,y]) &&
					(_boardState [1,y] == _boardState [2,y])) {
					if (_boardState [0,y].HasValue) {
						return true;
					}
				}
			}

			for (int x = 0; x < 3; ++x) {
				if ((_boardState [x,0] == _boardState [x,1]) &&
				     (_boardState [x,0] == _boardState [x,2])) {
					if (_boardState [x,0].HasValue) {
						return true;
					}
				}
			}

			// check diagonals
			if ((_boardState [0, 0] == _boardState [1, 1]) &&
			    (_boardState [0, 0] == _boardState [2, 2])) {
				if (_boardState [0,0].HasValue) {
					return true;
				}
			}

			if ((_boardState [0, 2] == _boardState [1, 1]) &&
				(_boardState [0, 2] == _boardState [2, 0])) {
				if (_boardState [0,2].HasValue) {
					return true;
				}
			}

			// every field occupied? remis
			if (!_boardState.Cast<State?> ().Any (t => !t.HasValue)) {
				GameState = null;
				return true;
			}

			return false;
		}

		void DrawCurrentMove (CCPoint location)
		{
			var filename = (GameState == State.Player1) ? "icon-x.png" : "icon-o.png";
			var symbol = new CCSprite (filename);
			symbol.AnchorPoint = CCPoint.AnchorMiddle;
			symbol.Scale = 5f;
			symbol.Position = location;
			AddChild (symbol);
		}

		CCPoint cellToLocation (Tuple<int,int> cell)
		{
			return new CCPoint (100f, 100f).Offset (cell.Item1 * 200f, cell.Item2 * 200f);
		}

		private Tuple<int,int> clickedPosition (CCPoint location)
		{
			int x = (int)(location.X / 200f);
			int y = (int)(location.Y / 200f);

			return new Tuple<int,int> (x, y);
		}

		private bool isValidCell (Tuple<int,int> cell)
		{
			return !_boardState [cell.Item1, cell.Item2].HasValue;
		}

	}

}
