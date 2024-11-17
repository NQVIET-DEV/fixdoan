using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.Pieces
{
    internal class Rook : ChessPiece
    {
        public Rook(PieceColor color) : base(color, PieceType.Rook) { }

        public override bool IsValidMove(int startX, int startY, int endX, int endY, ChessPiece[,] board)
        {
            if (startX != endX && startY != endY) return false;

            // Kiểm tra các ô trên đường đi có bị chặn không
            if (startX == endX)
            {
                for (int y = Math.Min(startY, endY) + 1; y < Math.Max(startY, endY); y++)
                    if (board[startX, y] != null) return false;
            }
            else
            {
                for (int x = Math.Min(startX, endX) + 1; x < Math.Max(startX, endX); x++)
                    if (board[x, startY] != null) return false;
            }
            return true;
        }
    }
}