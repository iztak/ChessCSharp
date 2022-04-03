using System;
using System.Collections.Generic;


namespace ChessCSharp.Piece
{
	public class pawn : Piece
	{
		public Tile currentTile = new Tile();
		public Tile nextTile = new Tile();

		bool IsValidMove(nextTile, currentTile)
        {
			if (Math.Abs(nextTile.position.x - currentTile.position.x) == 1 && Math.Abs(nextTile.position.y - currentTile.position.y) == 0)
			{
				return true;
			}

			else if (Math.Abs(nextTile.position.x - currentTile.position.x) == 1 && Math.Abs(nextTile.position.y - currentTile.position.y) == 1 && currentTile.IsEmpty == false)
			{
				return true;
			}
			else return false;

		}
	}
}
