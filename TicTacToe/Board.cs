using System;
using System.Collections.Generic;
using CocosSharp;
using System.Linq;

namespace TicTacToe
{
	public class Board : CCLayerColor
	{
		enum State {player1, player2};

		int _size;
		State _state;

		State?[,] _gameState = new State? [3, 3]; 

		public bool Gameover { get; set; }

		public Board(int size) : base()
		{
			_size = size;
			_state = State.player1;
		}

		public void DrawLines() {
			addHorizontalLines ();
			addVerticalLines ();
		}

		void addHorizontalLines() {
			var l = new CCDrawNode ();

			for (int x=1; x<_size; ++x)
			{
				var y = VisibleBoundsWorldspace.LowerLeft.Offset(
					0 ,VisibleBoundsWorldspace.MaxY / _size * x);
				l.DrawSegment (y,
					y.Offset(VisibleBoundsWorldspace.MaxX,0),
					5f,
					new CCColor4F (255f, 0f, 0f, 1f)); 
			}
			AddChild (l);

		}

		void addVerticalLines() {
			var l = new CCDrawNode ();

			for (int x=1; x<_size; ++x)
			{
				var y = VisibleBoundsWorldspace.LowerLeft.Offset(
					VisibleBoundsWorldspace.MaxX / _size * x, 0);
				l.DrawSegment (y,
					y.Offset(0,VisibleBoundsWorldspace.MaxY),
					5f,
					new CCColor4F (255f, 0f, 0f, 1f)); 
			}
			AddChild (l);

		}

		public void SwitchState() {
			_state = (_state == State.player1) ? State.player2 : State.player1;
		}

		public void HandleTouch(CCPoint location) {

			var clickedCell = clickedPosition (location);
			if (!isValidCell (clickedCell)) {
				return;
			}

			_gameState [clickedCell.Item1, clickedCell.Item2] = _state;

			DrawCurrentMove (cellToLocation(clickedCell));

			CheckForWin ();

			SwitchState ();
		}

		void CheckForWin() {
			Gameover = true;
		}

		void DrawCurrentMove (CCPoint location)
		{
			var filename = (_state == State.player1) ? "icon-x.png" : "icon-o.png";
			var symbol = new CCSprite (filename);
			symbol.AnchorPoint = CCPoint.AnchorMiddle;
			symbol.Scale = 5f;
			symbol.Position = location;
			AddChild (symbol);
		}

		CCPoint cellToLocation(Tuple<int,int> cell) {
			return new CCPoint (100f, 100f).Offset(cell.Item1*200f, cell.Item2*200f);
		}

		private Tuple<int,int> clickedPosition(CCPoint location) {
			int x = (int) (location.X / 200f);
			int y = (int) (location.Y / 200f);

			return new Tuple<int,int> (x, y);
		}

		private bool isValidCell(Tuple<int,int> cell) {
			return ! _gameState [cell.Item1, cell.Item2].HasValue;
		}

	}

}
