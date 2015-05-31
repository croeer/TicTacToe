using System;
using System.Collections.Generic;
using CocosSharp;

namespace TicTacToe
{
	public class Board : CCLayerColor
	{
		GameLayer _layer;
		int _size;

		public Board(GameLayer layer, int size) : base()
		{
			_layer = layer;
			_size = size;

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

		public void HandleTouch(CCPoint location) {
			var symbol = new CCSprite ("icon-x.png");
			symbol.AnchorPoint = CCPoint.AnchorMiddle;
			symbol.Scale = 5f;
			symbol.Position = location;

			AddChild (symbol);

		}

	}

}
