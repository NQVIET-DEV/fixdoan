using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace chess.Pieces
{
    internal class Pawn : ChessPiece
    {
        public Pawn(PieceColor color) : base(color, PieceType.Pawn) { }
        public override bool IsValidMove(int startX, int startY, int endX, int endY, ChessPiece[,] board)
        {
            int direction = (Color == PieceColor.White) ? -1 : 1;

            // Đi thẳng 1 ô hoặc 2 ô nếu ở hàng đầu tiên
            if (startY == endY && board[endX, endY] == null)
            {
                if (endX == startX + direction) return true;
                if ((Color == PieceColor.White && startX == 6 || Color == PieceColor.Black && startX == 1) &&
                    endX == startX + 2 * direction) return true;
            }

            // Ăn chéo
            if (Math.Abs(startY - endY) == 1 && endX == startX + direction && board[endX, endY] != null)
                return true;

            return false;
        }
    }
}