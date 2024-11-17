using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.Pieces
{
    internal class Knight : ChessPiece
    {
        public Knight(PieceColor color) : base(color, PieceType.Knight) { }

        public override bool IsValidMove(int startX, int startY, int endX, int endY, ChessPiece[,] board)
        {
            int dx = Math.Abs(startX - endX);
            int dy = Math.Abs(startY - endY);
            return (dx == 2 && dy == 1) || (dx == 1 && dy == 2);
        }
    }
}