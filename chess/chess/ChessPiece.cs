using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum PieceColor { White, Black }
public enum PieceType { Pawn, Rook, Knight, Bishop, Queen, King }

public abstract class ChessPiece
{
    public PieceColor Color { get; }
    public PieceType Type { get; }

    public ChessPiece(PieceColor color, PieceType type)
    {
        Color = color;
        Type = type;
    }


    public abstract bool IsValidMove(int startX, int startY, int endX, int endY, ChessPiece[,] board);
}