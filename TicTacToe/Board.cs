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
			//l.DrawSegment (p1, p2, 10.0f, new CCColor4F(255.0f, 0.0f,0.0f,1.0f));
			}
			AddChild (l);

		}

	}

}
