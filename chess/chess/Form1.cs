using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace chess
{
    public partial class Form1 : Form
    {
        private ChessBoard chessBoard;
        private PictureBox[,] pictureBoxes;
        private (int, int)? selectedCell = null;
        private PieceColor currentPlayer = PieceColor.White;

        public Form1()
        {
            InitializeComponent();
            chessBoard = new ChessBoard();
            pictureBoxes = new PictureBox[8, 8];
            InitializeBoard();
            ResetBoard();
        }

        private void InitializeBoard()
        {
            tableLayoutPanel1.RowCount = 8;
            tableLayoutPanel1.ColumnCount = 8;
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();

            for (int i = 0; i < 8; i++)
            {
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5f));
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5f));

                for (int j = 0; j < 8; j++)
                {
                    PictureBox picBox = new PictureBox
                    {
                        Dock = DockStyle.Fill,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        BorderStyle = BorderStyle.FixedSingle,
                        Tag = (i, j)
                    };
                    picBox.BackColor = (i + j) % 2 == 0 ? Color.White : Color.Gray;
                    picBox.Click += OnCellClick;
                    pictureBoxes[i, j] = picBox;
                    tableLayoutPanel1.Controls.Add(picBox, j, i);
                }
            }
        }

        private void ResetBoard()
        {
            chessBoard.Reset();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    UpdatePictureBox(i, j);
                }
            }
            currentPlayer = PieceColor.White;
        }

        private void UpdatePictureBox(int row, int col)
        {
            ChessPiece piece = chessBoard.Board[row, col];
            if (piece != null)
            {
                string imagePath = GetPieceImagePath(piece);
                pictureBoxes[row, col].Image = Image.FromFile(imagePath);
            }
            else
            {
                pictureBoxes[row, col].Image = null;
            }
        }

        private string GetPieceImagePath(ChessPiece piece)
        {
            string color = piece.Color == PieceColor.White ? "white" : "black";
            string type = piece.Type.ToString().ToLower();
            return $"{color}_{type}.png";
        }

        private void OnCellClick(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            var (row, col) = ((int, int))clickedPictureBox.Tag;

            if (selectedCell == null)
            {
                // Chọn một quân cờ
                if (chessBoard.Board[row, col] != null && chessBoard.Board[row, col].Color == currentPlayer)
                {
                    selectedCell = (row, col);
                    pictureBoxes[row, col].BackColor = Color.LightBlue;
                    HighlightValidMoves(row, col);
                }
            }
            else
            {
                // Di chuyển quân cờ
                var (startX, startY) = selectedCell.Value;
                if (chessBoard.IsValidMove(startX, startY, row, col))
                {
                    // Thực hiện di chuyển
                    chessBoard.Board[row, col] = chessBoard.Board[startX, startY];
                    chessBoard.Board[startX, startY] = null;
                    UpdatePictureBox(startX, startY);
                    UpdatePictureBox(row, col);

                    // Kiểm tra kết thúc trò chơi
                    if (IsGameOver())
                    {
                        MessageBox.Show($"Game Over! {(currentPlayer == PieceColor.White ? "White" : "Black")} wins!");
                        ResetBoard();
                        return;
                    }

                    // Đổi lượt
                    currentPlayer = currentPlayer == PieceColor.White ? PieceColor.Black : PieceColor.White;

                    // Lượt của AI
                    if (currentPlayer == PieceColor.Black)
                    {
                        MakeAIMove();
                    }
                }

                selectedCell = null;
                ResetBoardColors();
            }
        }

        private void MakeAIMove()
        {
            List<(int, int, int, int)> validMoves = GetAllValidMoves(PieceColor.Black);

            if (validMoves.Count == 0) return;

            // Chọn một nước đi ngẫu nhiên (sẽ cải thiện bằng thuật toán MiniMax sau)
            Random random = new Random();
            var (startX, startY, endX, endY) = validMoves[random.Next(validMoves.Count)];

            // Thực hiện di chuyển
            chessBoard.Board[endX, endY] = chessBoard.Board[startX, startY];
            chessBoard.Board[startX, startY] = null;
            UpdatePictureBox(startX, startY);
            UpdatePictureBox(endX, endY);

            // Kiểm tra kết thúc trò chơi
            if (IsGameOver())
            {
                MessageBox.Show("Game Over! AI wins!");
                ResetBoard();
                return;
            }

            currentPlayer = PieceColor.White;
        }

        private List<(int, int, int, int)> GetAllValidMoves(PieceColor color)
        {
            List<(int, int, int, int)> moves = new List<(int, int, int, int)>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    ChessPiece piece = chessBoard.Board[i, j];
                    if (piece != null && piece.Color == color)
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            for (int y = 0; y < 8; y++)
                            {
                                if (chessBoard.IsValidMove(i, j, x, y))
                                {
                                    moves.Add((i, j, x, y));
                                }
                            }
                        }
                    }
                }
            }
            return moves;
        }

        private bool IsGameOver()
        {
            return !IsKingOnBoard(PieceColor.White) || !IsKingOnBoard(PieceColor.Black);
        }

        private bool IsKingOnBoard(PieceColor color)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    ChessPiece piece = chessBoard.Board[i, j];
                    if (piece != null && piece.Type == PieceType.King && piece.Color == color)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void ResetCellColor(int row, int col)
        {
            pictureBoxes[row, col].BackColor = (row + col) % 2 == 0 ? Color.White : Color.Gray;
        }

        private void ResetBoardColors()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    ResetCellColor(i, j);
                }
            }
        }

        private void HighlightValidMoves(int row, int col)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (chessBoard.IsValidMove(row, col, x, y))
                    {
                        pictureBoxes[x, y].BackColor = Color.LightGreen;
                    }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
