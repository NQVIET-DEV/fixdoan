using chess.Pieces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class ChessBoard
{
    public ChessPiece[,] Board { get; } = new ChessPiece[8, 8];

    public ChessBoard()
    {
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        // Đặt quân tốt
        for (int i = 0; i < 8; i++)
        {
            Board[1, i] = new Pawn(PieceColor.Black);
            Board[6, i] = new Pawn(PieceColor.White);
        }

        // Đặt các quân khác
        Board[0, 0] = Board[0, 7] = new Rook(PieceColor.Black);
        Board[7, 0] = Board[7, 7] = new Rook(PieceColor.White);
        Board[0, 1] = Board[0, 6] = new Knight(PieceColor.Black);
        Board[7, 1] = Board[7, 6] = new Knight(PieceColor.White);
        Board[0, 2] = Board[0, 5] = new Bishop(PieceColor.Black);
        Board[7, 2] = Board[7, 5] = new Bishop(PieceColor.White);
        Board[0, 3] = new Queen(PieceColor.Black);
        Board[7, 3] = new Queen(PieceColor.White);
        Board[0, 4] = new King(PieceColor.Black);
        Board[7, 4] = new King(PieceColor.White);
    }

    public void Reset()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Board[i, j] = null;
            }
        }
        InitializeBoard();
    }

    public string SerializeBoard()
    {
        // Serialize the Board into a JSON string
        return JsonSerializer.Serialize(Board);
    }

    public void DeserializeBoard(string json)
    {
        // Deserialize the JSON string into a new ChessPiece array
        ChessPiece[,] newBoard = JsonSerializer.Deserialize<ChessPiece[,]>(json);

        // Cập nhật từng ô của Board thay vì gán lại toàn bộ
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Board[i, j] = newBoard[i, j];
            }
        }
    }

    public bool IsValidMove(int startX, int startY, int endX, int endY)
    {
        ChessPiece piece = Board[startX, startY];

        // Kiểm tra nếu ô bắt đầu không có quân cờ
        if (piece == null)
        {
            return false; // Không có quân cờ để di chuyển
        }

        // Kiểm tra xem quân cờ có thể di chuyển đến ô đích
        if (!piece.IsValidMove(startX, startY, endX, endY, Board))
        {
            return false; // Nước đi không hợp lệ theo quy tắc của quân cờ
        }

        // Kiểm tra nếu ô đích có quân cờ cùng màu
        ChessPiece destinationPiece = Board[endX, endY];
        if (destinationPiece != null && destinationPiece.Color == piece.Color)
        {
            return false; // Không thể di chuyển vào ô có quân cờ cùng màu
        }

        return true; 
    }

}