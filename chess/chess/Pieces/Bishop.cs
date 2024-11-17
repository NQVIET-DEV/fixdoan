
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.Pieces
{
    internal class Bishop : ChessPiece
    {
        public Bishop(PieceColor color) : base(color, PieceType.Bishop) { }

        public override bool IsValidMove(int startX, int startY, int endX, int endY, ChessPiece[,] board)
        {
            if (Math.Abs(startX - endX) != Math.Abs(startY - endY)) return false;

            int stepX = (endX > startX) ? 1 : -1;
            int stepY = (endY > startY) ? 1 : -1;
            int x = startX + stepX, y = startY + stepY;

            while (x != endX && y != endY)
            {
                if (x < 0 || x > 7 || y < 0 || y > 7) return false;
                if (board[x, y] != null) return false;
                x += stepX;
                y += stepY;
            }
            return true;
        }
    }
}