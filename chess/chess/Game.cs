using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess
{
    public class Game
    {
        public string[,] Board { get; private set; }

        public Game()
        {
            Board = new string[8, 8];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            // Khởi tạo quân cờ
            Board[0, 0] = "black_rook.png";
            Board[0, 1] = "black_knight.png";
            Board[0, 2] = "black_bishop.png";
            Board[0, 3] = "black_queen.png";
            Board[0, 4] = "black_king.png";
            Board[0, 5] = "black_bishop.png";
            Board[0, 6] = "black_knight.png";
            Board[0, 7] = "black_rook.png";
            for (int i = 0; i < 8; i++) Board[1, i] = "black_pawn.png";

            Board[7, 0] = "white_rook.png";
            Board[7, 1] = "white_knight.png";
            Board[7, 2] = "white_bishop.png";
            Board[7, 3] = "white_queen.png";
            Board[7, 4] = "white_king.png";
            Board[7, 5] = "white_bishop.png";
            Board[7, 6] = "white_knight.png";
            Board[7, 7] = "white_rook.png";
            for (int i = 0; i < 8; i++) Board[6, i] = "white_pawn.png";
        }

        public void MovePiece(string from, string to)
        {
            int fromRow = 8 - int.Parse(from[1].ToString());
            int fromCol = from[0] - 'a';
            int toRow = 8 - int.Parse(to[1].ToString());
            int toCol = to[0] - 'a';

            // Di chuyển quân cờ từ vị trí cũ sang vị trí mới
            Board[toRow, toCol] = Board[fromRow, fromCol];
            Board[fromRow, fromCol] = null; // Xóa quân cờ ở vị trí cũ
        }

        public string GetPieceAtPosition(int row, int col)
        {
            return Board[row, col];
        }
    }
}
