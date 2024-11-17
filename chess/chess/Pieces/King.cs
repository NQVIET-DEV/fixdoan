using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.Pieces
{
    internal class King : ChessPiece
    {
        public King(PieceColor color) : base(color, PieceType.King) { }

        public override bool IsValidMove(int startX, int startY, int endX, int endY, ChessPiece[,] board)
        {
            int dx = Math.Abs(startX - endX);
            int dy = Math.Abs(startY - endY);
            return (dx == 1 && dy == 0) || (dx == 0 && dy == 1) || (dx == 1 && dy == 1);
        }
    }
}