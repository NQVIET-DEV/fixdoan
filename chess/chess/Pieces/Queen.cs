using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.Pieces
{
    internal class Queen : ChessPiece
    {
        public Queen(PieceColor color) : base(color, PieceType.Queen) { }

        public override bool IsValidMove(int startX, int startY, int endX, int endY, ChessPiece[,] board)
        {
            Rook rook = new Rook(Color);
            Bishop bishop = new Bishop(Color);
            return rook.IsValidMove(startX, startY, endX, endY, board) ||
                   bishop.IsValidMove(startX, startY, endX, endY, board);
        }
    }
}