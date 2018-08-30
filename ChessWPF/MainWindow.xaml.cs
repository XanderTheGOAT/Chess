using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChessLibrary.Controllers;
using ChessLibrary.Models;

namespace ChessWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        bool blackCheck = false;
        bool whiteCheck = false;

        List<List<Board>> GUIBoard = new List<List<Board>>();
        bool endGame = false;
        Random r = new Random();
        bool isBlackTurn = true;
        Label emptySpot2 = new Label();
        ComboBox promotionStatus = new ComboBox();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region Assigns the board
            Program.board.Initialize();
            for (int i = 0; i < 8; i++)
            {
                List<Board> boardRow = new List<Board>();
                for (int j = 0; j < 8; j++)
                {
                    Board boardSpot = new Board();
                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                        {
                            boardSpot.cbxIsBlack.IsChecked = false;
                        }
                        else
                        {
                            boardSpot.cbxIsBlack.IsChecked = true;
                        }
                    }
                    else
                    {
                        if (j % 2 == 0)
                        {
                            boardSpot.cbxIsBlack.IsChecked = true;
                        }
                        else
                        {
                            boardSpot.cbxIsBlack.IsChecked = false;
                        }
                    }
                    boardRow.Add(boardSpot);
                }
                GUIBoard.Add(boardRow);
                #endregion
            }
            int firstPlayer = r.Next(0, 2);

            if (firstPlayer == 0)
            {
                isBlackTurn = false;
            }
            else if (firstPlayer == 1)
            {
                isBlackTurn = true;
            }
            BoardLogic.InitializeBoard();
            BasicChessBoard();
            DrawBoard();
        }

        private void DrawBoard()
        {
            char[] Columns = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            string[] promotionTypes = { "Queen", "Bishop", "Rook", "Knight" };
            #region Assigns the board
            promotionStatus.ItemsSource = promotionTypes;
            promotionStatus.SelectedIndex = 0;
            promotionStatus.FontSize = 20.0;
            ugChessBoard.Children.Add(promotionStatus);

            promotionStatus.VerticalContentAlignment = VerticalAlignment.Center;
            promotionStatus.HorizontalContentAlignment = HorizontalAlignment.Center;
            for (int i = 0; i < 8; i++)
            {
                Viewbox box = new Viewbox();
                Label column = new Label();
                column.HorizontalContentAlignment = HorizontalAlignment.Center;
                column.VerticalContentAlignment = VerticalAlignment.Bottom;
                column.Foreground = Brushes.White;
                column.FontSize = 20.0;
                column.Content = Columns[i].ToString();
                box.Child = column;
                ugChessBoard.Children.Add(box);
            }
            if (isBlackTurn)
            {
                emptySpot2.Content = "Black Turn";
            }
            else
            {
                emptySpot2.Content = "White Turn";
            }
            Viewbox viewbox = new Viewbox();
            emptySpot2.VerticalContentAlignment = VerticalAlignment.Center;
            emptySpot2.HorizontalContentAlignment = HorizontalAlignment.Center;
            emptySpot2.Foreground = Brushes.White;
            viewbox.Child = emptySpot2;
            ugChessBoard.Children.Add(viewbox);

            for (int i = 0; i < 8; i++)
            {
                Label rowNumber = new Label();
                rowNumber.HorizontalContentAlignment = HorizontalAlignment.Right;
                rowNumber.VerticalContentAlignment = VerticalAlignment.Center;
                rowNumber.Content = i + 1;
                rowNumber.Foreground = Brushes.White;
                Viewbox rowResize = new Viewbox();
                rowResize.Child = rowNumber;
                ugChessBoard.Children.Add(rowResize);
                for (int j = 0; j < 8; j++)
                {
                    GUIBoard[i][j].Name = $"{Columns[j]}{i + 1}";
                    GUIBoard[i][j].MouseLeftButtonDown += ImageLocation;
                    ugChessBoard.Children.Add(GUIBoard[i][j]);
                }
                Label endRowNumber = new Label();
                endRowNumber.VerticalContentAlignment = VerticalAlignment.Center;
                endRowNumber.Foreground = Brushes.White;
                endRowNumber.Content = i + 1;
                Viewbox rowBottomResize = new Viewbox();
                rowBottomResize.Child = endRowNumber;
                ugChessBoard.Children.Add(rowBottomResize);
            }
            Label emptySpot3 = new Label();
            ugChessBoard.Children.Add(emptySpot3);
            for (int i = 0; i < 8; i++)
            {
                Label column = new Label();
                column.HorizontalContentAlignment = HorizontalAlignment.Center;
                column.Foreground = Brushes.White;
                column.Content = Columns[i].ToString();
                Viewbox columnBottomResize = new Viewbox();
                columnBottomResize.Child = column;
                ugChessBoard.Children.Add(columnBottomResize);
            }
            Label emptySpot4 = new Label();
            ugChessBoard.Children.Add(emptySpot4);
            #endregion
            string packUri;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Program.board[i, j].Piece != null)
                    {
                        switch (Program.board[i, j].Piece.IsLight)
                        {
                            case true:
                                switch (Program.board[i, j].Piece.ToString())
                                {
                                    case "K":
                                        packUri = "pack://application:,,,/ChessWPF;component/Images/WhitePieces/WhiteKing.png";
                                        GUIBoard[i][j].imgChessPiece.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
                                        break;
                                    case "Q":
                                        packUri = "pack://application:,,,/ChessWPF;component/Images/WhitePieces/WhiteQueen.png";
                                        GUIBoard[i][j].imgChessPiece.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
                                        break;
                                    case "N":
                                        packUri = "pack://application:,,,/ChessWPF;component/Images/WhitePieces/WhiteKnight.png";
                                        GUIBoard[i][j].imgChessPiece.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
                                        break;
                                    case "B":
                                        packUri = "pack://application:,,,/ChessWPF;component/Images/WhitePieces/WhiteBishop.png";
                                        GUIBoard[i][j].imgChessPiece.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
                                        break;
                                    case "R":
                                        packUri = "pack://application:,,,/ChessWPF;component/Images/WhitePieces/WhiteRook.png";
                                        GUIBoard[i][j].imgChessPiece.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
                                        break;
                                    case "P":
                                        packUri = "pack://application:,,,/ChessWPF;component/Images/WhitePieces/WhitePawn.png";
                                        GUIBoard[i][j].imgChessPiece.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case false:
                                switch (Program.board[i, j].Piece.ToString())
                                {
                                    case "K":
                                        packUri = "pack://application:,,,/ChessWPF;component/Images/BlackPieces/BlackKing.png";
                                        GUIBoard[i][j].imgChessPiece.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
                                        break;
                                    case "Q":
                                        packUri = "pack://application:,,,/ChessWPF;component/Images/BlackPieces/BlackQueen.png";
                                        GUIBoard[i][j].imgChessPiece.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
                                        break;
                                    case "N":
                                        packUri = "pack://application:,,,/ChessWPF;component/Images/BlackPieces/BlackKnight.png";
                                        GUIBoard[i][j].imgChessPiece.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
                                        break;
                                    case "B":
                                        packUri = "pack://application:,,,/ChessWPF;component/Images/BlackPieces/BlackBishop.png";
                                        GUIBoard[i][j].imgChessPiece.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
                                        break;
                                    case "R":
                                        packUri = "pack://application:,,,/ChessWPF;component/Images/BlackPieces/BlackRook.png";
                                        GUIBoard[i][j].imgChessPiece.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
                                        break;
                                    case "P":
                                        packUri = "pack://application:,,,/ChessWPF;component/Images/BlackPieces/BlackPawn.png";
                                        GUIBoard[i][j].imgChessPiece.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                    }
                }
            }
        }

        private void BasicChessBoard()
        {
            //Black Pieces
            Program.board[0, 0].Piece = new Rook();
            Program.board[0, 1].Piece = new Knight();
            Program.board[0, 2].Piece = new Bishop();
            Program.board[0, 3].Piece = new King();
            Program.board[0, 4].Piece = new Queen();
            Program.board[0, 5].Piece = new Bishop();
            Program.board[0, 6].Piece = new Knight();
            Program.board[0, 7].Piece = new Rook();

            //White Pieces
            Program.board[7, 0].Piece = new Rook();
            Program.board[7, 0].Piece.IsLight = true;
            Program.board[7, 1].Piece = new Knight();
            Program.board[7, 1].Piece.IsLight = true;
            Program.board[7, 2].Piece = new Bishop();
            Program.board[7, 2].Piece.IsLight = true;
            Program.board[7, 3].Piece = new King();
            Program.board[7, 3].Piece.IsLight = true;
            Program.board[7, 4].Piece = new Queen();
            Program.board[7, 4].Piece.IsLight = true;
            Program.board[7, 5].Piece = new Bishop();
            Program.board[7, 5].Piece.IsLight = true;
            Program.board[7, 6].Piece = new Knight();
            Program.board[7, 6].Piece.IsLight = true;
            Program.board[7, 7].Piece = new Rook();
            Program.board[7, 7].Piece.IsLight = true;

            //Pawns
            for (int i = 0; i < 8; i++)
            {
                Program.board[6, i].Piece = new Pawn();
                Program.board[6, i].Piece.IsLight = true;
            }

            for (int i = 0; i < 8; i++)
            {
                Program.board[1, i].Piece = new Pawn();
                Program.board[1, i].Piece.IsLight = false;
            }
        }

        Board clickedLocation = new Board();
        Board savedLocation = new Board();
        int column = 0;
        int row = 0;

        ChessPiece savedPiece;
        ImageSource savedBoard;

        private void ValidateMovement()
        {
            bool validMove = true;
            bool check = false;
            #region WhitePiece Validation
            if (savedLocation.imgChessPiece.Source.ToString() != null && savedLocation.imgChessPiece.Source.ToString().Contains("WhitePieces"))
            {
                if (clickedLocation.imgChessPiece.Source == null || !clickedLocation.imgChessPiece.Source.ToString().Contains("WhitePieces"))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (GUIBoard[i][j].Name.ToString() == clickedLocation.Name.ToString())
                            {
                                if (savedLocation.imgChessPiece.Source.ToString().Contains("King"))
                                {
                                    King king = new King(true);
                                    validMove = king.ValidMovement(Program.board[column, row], Program.board[i, j]);
                                    if (validMove)
                                    {
                                        savedPiece = Program.board[i, j].Piece;
                                        savedBoard = GUIBoard[i][j].imgChessPiece.Source;

                                        Program.board[i, j].Piece = Program.board[column, row].Piece;
                                        Program.board[column, row].Piece = null;
                                        int k;
                                        int l;
                                        for (k = 0; k < 8; k++)
                                        {
                                            for (l = 0; l < 8; l++)
                                            {
                                                if (Program.board[k, l].Piece != null && !Program.board[k, l].Piece.IsLight && Program.board[k, l].Piece.Check(Program.board[k, l], Program.board[i, j]))
                                                {
                                                    MessageBox.Show("King cannot place himself in check");
                                                    validMove = false;
                                                    k = 9;
                                                    l = 9;
                                                }
                                            }
                                        }
                                        if (k != 10)
                                        {
                                            whiteCheck = false;
                                            validMove = true;
                                        }
                                        Program.board[column, row].Piece = Program.board[i, j].Piece;
                                        Program.board[i, j].Piece = savedPiece;
                                    }
                                }
                                else if (savedLocation.imgChessPiece.Source.ToString().Contains("Queen"))
                                {
                                    Queen queen = new Queen(true);
                                    validMove = queen.ValidMovement(Program.board[column, row], Program.board[i, j]);
                                    if (validMove)
                                    {
                                        savedPiece = Program.board[i, j].Piece;
                                        savedBoard = GUIBoard[i][j].imgChessPiece.Source;

                                        Program.board[i, j].Piece = Program.board[column, row].Piece;
                                        Program.board[column, row].Piece = null;
                                        int k;
                                        int l;
                                        for (k = 0; k < 8; k++)
                                        {
                                            for (l = 0; l < 8; l++)
                                            {
                                                if (Program.board[k, l].Piece != null && !Program.board[k, l].Piece.IsLight && Program.board[k, l].Piece.Check(Program.board[k, l], Program.board[i, j]))
                                                {
                                                    MessageBox.Show("Cannot place the king in check");
                                                    validMove = false;
                                                    k = 9;
                                                    l = 9;
                                                }
                                            }
                                        }
                                        if (k != 10)
                                        {
                                            whiteCheck = false;
                                            validMove = true;
                                        }
                                        Program.board[column, row].Piece = Program.board[i, j].Piece;
                                        Program.board[i, j].Piece = savedPiece;
                                    }
                                }
                                else if (savedLocation.imgChessPiece.Source.ToString().Contains("Knight"))
                                {
                                    Knight knight = new Knight(true);
                                    validMove = knight.ValidMovement(Program.board[column, row], Program.board[i, j]);
                                    if (validMove)
                                    {
                                        savedPiece = Program.board[i, j].Piece;
                                        savedBoard = GUIBoard[i][j].imgChessPiece.Source;

                                        Program.board[i, j].Piece = Program.board[column, row].Piece;
                                        Program.board[column, row].Piece = null;
                                        int k;
                                        int l;
                                        for (k = 0; k < 8; k++)
                                        {
                                            for (l = 0; l < 8; l++)
                                            {
                                                if (Program.board[k, l].Piece != null && !Program.board[k, l].Piece.IsLight && Program.board[k, l].Piece.Check(Program.board[k, l], Program.board[i, j]))
                                                {
                                                    MessageBox.Show("Cannot place the king in check");
                                                    validMove = false;
                                                    k = 9;
                                                    l = 9;
                                                }
                                            }
                                        }
                                        if (k != 10)
                                        {
                                            whiteCheck = false;
                                            validMove = true;
                                        }
                                        Program.board[column, row].Piece = Program.board[i, j].Piece;
                                        Program.board[i, j].Piece = savedPiece;
                                    }
                                }
                                else if (savedLocation.imgChessPiece.Source.ToString().Contains("Rook"))
                                {
                                    Rook rook = new Rook(true);
                                    validMove = rook.ValidMovement(Program.board[column, row], Program.board[i, j]);
                                    if (validMove)
                                    {
                                        savedPiece = Program.board[i, j].Piece;
                                        savedBoard = GUIBoard[i][j].imgChessPiece.Source;

                                        Program.board[i, j].Piece = Program.board[column, row].Piece;
                                        Program.board[column, row].Piece = null;
                                        int k;
                                        int l;
                                        for (k = 0; k < 8; k++)
                                        {
                                            for (l = 0; l < 8; l++)
                                            {
                                                if (Program.board[k, l].Piece != null && !Program.board[k, l].Piece.IsLight && Program.board[k, l].Piece.Check(Program.board[k, l], Program.board[i, j]))
                                                {
                                                    MessageBox.Show("Cannot place the king in check");
                                                    validMove = false;
                                                    k = 9;
                                                    l = 9;
                                                }
                                            }
                                        }
                                        if (k != 10)
                                        {
                                            whiteCheck = false;
                                            validMove = true;
                                        }
                                        Program.board[column, row].Piece = Program.board[i, j].Piece;
                                        Program.board[i, j].Piece = savedPiece;
                                    }
                                }
                                else if (savedLocation.imgChessPiece.Source.ToString().Contains("Bishop"))
                                {
                                    Bishop bishop = new Bishop(true);
                                    validMove = bishop.ValidMovement(Program.board[column, row], Program.board[i, j]);
                                    if (validMove)
                                    {
                                        savedPiece = Program.board[i, j].Piece;
                                        savedBoard = GUIBoard[i][j].imgChessPiece.Source;

                                        Program.board[i, j].Piece = Program.board[column, row].Piece;
                                        Program.board[column, row].Piece = null;
                                        int k;
                                        int l;
                                        for (k = 0; k < 8; k++)
                                        {
                                            for (l = 0; l < 8; l++)
                                            {
                                                if (Program.board[k, l].Piece != null && !Program.board[k, l].Piece.IsLight && Program.board[k, l].Piece.Check(Program.board[k, l], Program.board[i, j]))
                                                {
                                                    MessageBox.Show("Cannot place the king in check");
                                                    validMove = false;
                                                    k = 9;
                                                    l = 9;
                                                }
                                            }
                                        }
                                        if (k != 10)
                                        {
                                            whiteCheck = false;
                                            validMove = true;
                                        }
                                        Program.board[column, row].Piece = Program.board[i, j].Piece;
                                        Program.board[i, j].Piece = savedPiece;
                                    }
                                }
                                else if (savedLocation.imgChessPiece.Source.ToString().Contains("Pawn"))
                                {
                                    Pawn pawn = new Pawn(true);
                                    validMove = pawn.ValidMovement(Program.board[column, row], Program.board[i, j]);
                                    if (validMove)
                                    {
                                        savedPiece = Program.board[i, j].Piece;
                                        savedBoard = GUIBoard[i][j].imgChessPiece.Source;

                                        Program.board[i, j].Piece = Program.board[column, row].Piece;
                                        Program.board[column, row].Piece = null;
                                        int k;
                                        int l;
                                        for (k = 0; k < 8; k++)
                                        {
                                            for (l = 0; l < 8; l++)
                                            {
                                                if (Program.board[k, l].Piece != null && !Program.board[k, l].Piece.IsLight && Program.board[k, l].Piece.Check(Program.board[k, l], Program.board[i, j]))
                                                {
                                                    MessageBox.Show("Cannot place the king in check");
                                                    validMove = false;
                                                    k = 9;
                                                    l = 9;
                                                }
                                            }
                                        }
                                        if (k != 10)
                                        {
                                            whiteCheck = false;
                                            validMove = true;
                                        }
                                        Program.board[column, row].Piece = Program.board[i, j].Piece;
                                        Program.board[i, j].Piece = savedPiece;
                                    }
                                }
                                if (whiteCheck && validMove)
                                {
                                    int k;
                                    int l;
                                    for (k = 0; k < 8; k++)
                                    {
                                        for (l = 0; l < 8; l++)
                                        {
                                            if (Program.board[k, l].Piece != null && !Program.board[k, l].Piece.IsLight && Program.board[k, l].Piece.Check(Program.board[k, l], Program.board[i, j]))
                                            {
                                                if (Program.board[column, row].Piece.ValidMovement(Program.board[column, row], Program.board[k, l]) && Program.board[k, l] == Program.board[i, j])
                                                {
                                                    validMove = true;
                                                }
                                                else if (Program.board[column, row].Piece.ValidMovement(Program.board[column, row], Program.board[i, j]) && Program.board[k, l].Piece.ValidMoves.Contains(Program.board[i, j]))
                                                {
                                                    validMove = true;
                                                }
                                                else
                                                {
                                                    validMove = false;
                                                }
                                                //savedLocation.imgChessPiece.Source = GUIBoard[i][j].imgChessPiece.Source;
                                                //GUIBoard[i][j].imgChessPiece.Source = savedBoard;
                                                //Program.board[column, row].Piece = Program.board[i, j].Piece;
                                                //Program.board[i, j].Piece = savedPiece.Piece;
                                                l = 10;
                                                k = 10;
                                            }
                                        }
                                    }
                                    if (k == 10)
                                    {
                                        //GUIBoard[i][j].imgChessPiece.Source = savedLocation.imgChessPiece.Source;
                                        //Program.board[i, j].Piece = Program.board[column, row].Piece;
                                    }
                                }
                                if (!(bool)savedLocation.cbxIsBlack.IsChecked)
                                {
                                    savedLocation.background.Fill = Brushes.Pink;
                                }
                                else if ((bool)savedLocation.cbxIsBlack.IsChecked)
                                {
                                    savedLocation.background.Fill = Brushes.OrangeRed;
                                }
                                reset.ForEach(r =>
                                {
                                    if ((bool)GUIBoard[FileLogic.GetColumnFromChar(r.Column).GetHashCode()][r.Row].cbxIsBlack.IsChecked)
                                    {
                                        GUIBoard[FileLogic.GetColumnFromChar(r.Column).GetHashCode()][r.Row].background.Fill = Brushes.OrangeRed;
                                    }
                                    else
                                    {
                                        GUIBoard[FileLogic.GetColumnFromChar(r.Column).GetHashCode()][r.Row].background.Fill = Brushes.Pink;
                                    }
                                });
                                reset.Clear();
                                if (validMove)
                                {
                                    GUIBoard[i][j].imgChessPiece.Source = savedLocation.imgChessPiece.Source;
                                    Program.board[i, j].Piece = Program.board[column, row].Piece;
                                    check = Program.board[i, j].Piece.Check(Program.board[i, j], Program.board[i, j]);
                                    if (check)
                                    {
                                        MessageBox.Show("Black King is in check");
                                        blackCheck = true;
                                    }
                                    PromotionValidation();
                                    Program.board[column, row].Piece = null;
                                    savedLocation.imgChessPiece.Source = null;
                                    isBlackTurn = isBlackTurn ? false : true;
                                    whiteCheck = false;
                                }
                                savedLocation = new Board();
                            }

                        }
                    }

                }
            }
            #endregion
            #region BlackPiece Validation
            else if (savedLocation.imgChessPiece.Source.ToString() != null && savedLocation.imgChessPiece.Source.ToString().Contains("BlackPieces"))
            {
                string[] pieceNames = { "King", "Rook", "Queen", "Knight", "Bishop" };
                if (clickedLocation.imgChessPiece.Source == null || !clickedLocation.imgChessPiece.Source.ToString().Contains("BlackPieces"))
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (GUIBoard[i][j].Name.ToString() == clickedLocation.Name.ToString())
                            {
                                if (savedLocation.imgChessPiece.Source.ToString().Contains("King"))
                                {
                                    King king = new King();
                                    validMove = king.ValidMovement(Program.board[column, row], Program.board[i, j]);
                                    if (validMove)
                                    {
                                        savedPiece = Program.board[i, j].Piece;
                                        savedBoard = GUIBoard[i][j].imgChessPiece.Source;

                                        Program.board[i, j].Piece = Program.board[column, row].Piece;
                                        Program.board[column, row].Piece = null;
                                        int k;
                                        int l;
                                        for (k = 0; k < 8; k++)
                                        {
                                            for (l = 0; l < 8; l++)
                                            {
                                                if (Program.board[k, l].Piece != null && Program.board[k, l].Piece.IsLight && Program.board[k, l].Piece.Check(Program.board[k, l], Program.board[i, j]))
                                                {
                                                    MessageBox.Show("King cannot place himself in check");
                                                    validMove = false;
                                                    k = 9;
                                                    l = 9;
                                                }
                                            }
                                        }
                                        if (k != 10)
                                        {
                                            blackCheck = false;
                                            validMove = true;
                                        }
                                        Program.board[column, row].Piece = Program.board[i, j].Piece;
                                        Program.board[i, j].Piece = savedPiece;
                                    }
                                }
                                else if (savedLocation.imgChessPiece.Source.ToString().Contains("Queen"))
                                {
                                    Queen queen = new Queen();
                                    validMove = queen.ValidMovement(Program.board[column, row], Program.board[i, j]);
                                    if (validMove)
                                    {
                                        savedPiece = Program.board[i, j].Piece;
                                        savedBoard = GUIBoard[i][j].imgChessPiece.Source;

                                        Program.board[i, j].Piece = Program.board[column, row].Piece;
                                        Program.board[column, row].Piece = null;
                                        int k;
                                        int l;
                                        for (k = 0; k < 8; k++)
                                        {
                                            for (l = 0; l < 8; l++)
                                            {
                                                if (Program.board[k, l].Piece != null && Program.board[k, l].Piece.IsLight && Program.board[k, l].Piece.Check(Program.board[k, l], Program.board[i, j]))
                                                {
                                                    MessageBox.Show("Cannot place the king in check");
                                                    validMove = false;
                                                    k = 9;
                                                    l = 9;
                                                }
                                            }
                                        }
                                        if (k != 10)
                                        {
                                            blackCheck = false;
                                            validMove = true;
                                        }
                                        Program.board[column, row].Piece = Program.board[i, j].Piece;
                                        Program.board[i, j].Piece = savedPiece;
                                    }
                                }
                                else if (savedLocation.imgChessPiece.Source.ToString().Contains("Knight"))
                                {
                                    Knight knight = new Knight();
                                    validMove = knight.ValidMovement(Program.board[column, row], Program.board[i, j]);
                                    if (validMove)
                                    {
                                        savedPiece = Program.board[i, j].Piece;
                                        savedBoard = GUIBoard[i][j].imgChessPiece.Source;

                                        Program.board[i, j].Piece = Program.board[column, row].Piece;
                                        Program.board[column, row].Piece = null;
                                        int k;
                                        int l;
                                        for (k = 0; k < 8; k++)
                                        {
                                            for (l = 0; l < 8; l++)
                                            {
                                                if (Program.board[k, l].Piece != null && Program.board[k, l].Piece.IsLight && Program.board[k, l].Piece.Check(Program.board[k, l], Program.board[i, j]))
                                                {
                                                    MessageBox.Show("Cannot place the king in check");
                                                    validMove = false;
                                                    k = 9;
                                                    l = 9;
                                                }
                                            }
                                        }
                                        if (k != 10)
                                        {
                                            blackCheck = false;
                                            validMove = true;
                                        }
                                        Program.board[column, row].Piece = Program.board[i, j].Piece;
                                        Program.board[i, j].Piece = savedPiece;
                                    }
                                }
                                else if (savedLocation.imgChessPiece.Source.ToString().Contains("Rook"))
                                {
                                    Rook rook = new Rook();
                                    validMove = rook.ValidMovement(Program.board[column, row], Program.board[i, j]);
                                    if (validMove)
                                    {
                                        savedPiece = Program.board[i, j].Piece;
                                        savedBoard = GUIBoard[i][j].imgChessPiece.Source;

                                        Program.board[i, j].Piece = Program.board[column, row].Piece;
                                        Program.board[column, row].Piece = null;
                                        int k;
                                        int l;
                                        for (k = 0; k < 8; k++)
                                        {
                                            for (l = 0; l < 8; l++)
                                            {
                                                if (Program.board[k, l].Piece != null && Program.board[k, l].Piece.IsLight && Program.board[k, l].Piece.Check(Program.board[k, l], Program.board[i, j]))
                                                {
                                                    MessageBox.Show("Cannot place the king in check");
                                                    validMove = false;
                                                    k = 9;
                                                    l = 9;
                                                }
                                            }
                                        }
                                        if (k != 10)
                                        {
                                            blackCheck = false;
                                            validMove = true;
                                        }
                                        Program.board[column, row].Piece = Program.board[i, j].Piece;
                                        Program.board[i, j].Piece = savedPiece;
                                    }
                                }
                                else if (savedLocation.imgChessPiece.Source.ToString().Contains("Bishop"))
                                {
                                    Bishop bishop = new Bishop();
                                    validMove = bishop.ValidMovement(Program.board[column, row], Program.board[i, j]);
                                    if (validMove)
                                    {
                                        savedPiece = Program.board[i, j].Piece;
                                        savedBoard = GUIBoard[i][j].imgChessPiece.Source;

                                        Program.board[i, j].Piece = Program.board[column, row].Piece;
                                        Program.board[column, row].Piece = null;
                                        int k;
                                        int l;
                                        for (k = 0; k < 8; k++)
                                        {
                                            for (l = 0; l < 8; l++)
                                            {
                                                if (Program.board[k, l].Piece != null && Program.board[k, l].Piece.IsLight && Program.board[k, l].Piece.Check(Program.board[k, l], Program.board[i, j]))
                                                {
                                                    MessageBox.Show("Cannot place the king in check");
                                                    validMove = false;
                                                    k = 9;
                                                    l = 9;
                                                }
                                            }
                                        }
                                        if (k != 10)
                                        {
                                            blackCheck = false;
                                            validMove = true;
                                        }
                                        Program.board[column, row].Piece = Program.board[i, j].Piece;
                                        Program.board[i, j].Piece = savedPiece;
                                    }
                                }
                                else if (savedLocation.imgChessPiece.Source.ToString().Contains("Pawn"))
                                {
                                    Pawn pawn = new Pawn();
                                    validMove = pawn.ValidMovement(Program.board[column, row], Program.board[i, j]);
                                    if (validMove)
                                    {
                                        savedPiece = Program.board[i, j].Piece;
                                        savedBoard = GUIBoard[i][j].imgChessPiece.Source;

                                        Program.board[i, j].Piece = Program.board[column, row].Piece;
                                        Program.board[column, row].Piece = null;
                                        int k;
                                        int l;
                                        for (k = 0; k < 8; k++)
                                        {
                                            for (l = 0; l < 8; l++)
                                            {
                                                if (Program.board[k, l].Piece != null && Program.board[k, l].Piece.IsLight && Program.board[k, l].Piece.Check(Program.board[k, l], Program.board[i, j]))
                                                {
                                                    MessageBox.Show("Cannot place the king in check");
                                                    validMove = false;
                                                    k = 9;
                                                    l = 9;
                                                }
                                            }
                                        }
                                        if (k != 10)
                                        {
                                            blackCheck = false;
                                            validMove = true;
                                        }
                                        Program.board[column, row].Piece = Program.board[i, j].Piece;
                                        Program.board[i, j].Piece = savedPiece;
                                    }
                                }
                                if (blackCheck && validMove)
                                {
                                    int k;
                                    int l;
                                    for (k = 0; k < 8; k++)
                                    {
                                        for (l = 0; l < 8; l++)
                                        {
                                            if (Program.board[k, l].Piece != null && Program.board[k, l].Piece.IsLight && Program.board[k, l].Piece.Check(Program.board[k, l], Program.board[i, j]))
                                            {
                                                if (Program.board[column, row].Piece.ValidMovement(Program.board[column, row], Program.board[k, l]) && Program.board[k, l] == Program.board[i, j])
                                                {
                                                    validMove = true;
                                                }
                                                else if (Program.board[column, row].Piece.ValidMovement(Program.board[column, row], Program.board[i, j]) && Program.board[k, l].Piece.ValidMoves.Contains(Program.board[i, j]))
                                                {
                                                    validMove = true;
                                                }
                                                else
                                                {
                                                    validMove = false;
                                                }
                                                //savedLocation.imgChessPiece.Source = GUIBoard[i][j].imgChessPiece.Source;
                                                //GUIBoard[i][j].imgChessPiece.Source = savedBoard;
                                                //Program.board[column, row].Piece = Program.board[i, j].Piece;
                                                //Program.board[i, j].Piece = savedPiece.Piece;
                                                l = 10;
                                                k = 10;
                                            }
                                        }
                                    }
                                    if (k == 10)
                                    {
                                        //GUIBoard[i][j].imgChessPiece.Source = savedLocation.imgChessPiece.Source;
                                        //Program.board[i, j].Piece = Program.board[column, row].Piece;
                                    }
                                }
                                if (!(bool)savedLocation.cbxIsBlack.IsChecked)
                                {
                                    savedLocation.background.Fill = Brushes.Pink;
                                }
                                else if ((bool)savedLocation.cbxIsBlack.IsChecked)
                                {
                                    savedLocation.background.Fill = Brushes.OrangeRed;
                                }
                                reset.ForEach(r =>
                                {
                                    if ((bool)GUIBoard[FileLogic.GetColumnFromChar(r.Column).GetHashCode()][r.Row].cbxIsBlack.IsChecked)
                                    {
                                        GUIBoard[FileLogic.GetColumnFromChar(r.Column).GetHashCode()][r.Row].background.Fill = Brushes.OrangeRed;
                                    }
                                    else
                                    {
                                        GUIBoard[FileLogic.GetColumnFromChar(r.Column).GetHashCode()][r.Row].background.Fill = Brushes.Pink;
                                    }
                                });
                                reset.Clear();
                                if (validMove)
                                {
                                    GUIBoard[i][j].imgChessPiece.Source = savedLocation.imgChessPiece.Source;
                                    Program.board[i, j].Piece = Program.board[column, row].Piece;
                                    check = Program.board[i, j].Piece.Check(Program.board[i, j], Program.board[i, j]);
                                    if (check)
                                    {
                                        MessageBox.Show("White King is in check");
                                        whiteCheck = true;
                                    }
                                    PromotionValidation();
                                    Program.board[column, row].Piece = null;
                                    savedLocation.imgChessPiece.Source = null;
                                    isBlackTurn = isBlackTurn ? false : true;
                                    blackCheck = false;
                                }
                                savedLocation = new Board();
                            }
                        }
                    }
                }
            }
            #endregion
            endGame = Checkmate(!isBlackTurn);
            endGame = Checkmate(isBlackTurn);
            if(endGame)
            {
                this.Close();
            }
        }

        private bool Checkmate(bool isLight)
        {
            List<BoardLogic.ChessCoordinates> kingCheck = new List<BoardLogic.ChessCoordinates>();
            King king = null;
            BoardLogic.ChessCoordinates kingSpot = new BoardLogic.ChessCoordinates('a', -1, null);
            int threatenedLocations = 0;
            #region Check Surrounding King Locations
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Program.board[i, j].Piece != null)
                    {
                        if (Program.board[i, j].Piece.ToString().Equals("K"))
                        {

                            if (Program.board[i, j].Piece.IsLight == isLight)
                            {
                                king = (King)Program.board[i, j].Piece;
                                kingSpot = Program.board[i, j];
                                kingCheck.Add(Program.board[i, j]);
                                if (j - 1 >= 0)
                                {
                                    if (Program.board[i, j - 1].Piece == null)
                                    {
                                        kingCheck.Add(Program.board[i, j - 1]);
                                    }
                                    else if (Program.board[i, j - 1].Piece.IsLight != Program.board[i, j].Piece.IsLight)
                                    {
                                        kingCheck.Add(Program.board[i, j - 1]);
                                    }
                                    if (i + 1 <= 7)
                                    {
                                        if (Program.board[i + 1, j - 1].Piece == null)
                                        {
                                            kingCheck.Add(Program.board[i + 1, j - 1]);
                                        }
                                        else if (Program.board[i + 1, j - 1].Piece.IsLight != Program.board[i, j].Piece.IsLight)
                                        {
                                            kingCheck.Add(Program.board[i + 1, j - 1]);
                                        }
                                    }
                                }
                                if (j + 1 <= 7)
                                {
                                    if (Program.board[i, j + 1].Piece == null)
                                    {
                                        kingCheck.Add(Program.board[i, j + 1]);
                                    }
                                    else if (Program.board[i, j + 1].Piece.IsLight != Program.board[i, j].Piece.IsLight)
                                    {
                                        kingCheck.Add(Program.board[i, j + 1]);
                                    }
                                    if (i - 1 >= 0)
                                    {
                                        if (Program.board[i - 1, j + 1].Piece == null)
                                        {
                                            kingCheck.Add(Program.board[i - 1, j + 1]);
                                        }
                                        else if (Program.board[i - 1, j + 1].Piece.IsLight != Program.board[i, j].Piece.IsLight)
                                        {
                                            kingCheck.Add(Program.board[i - 1, j + 1]);
                                        }
                                    }
                                }
                                if (i - 1 >= 0)
                                {
                                    if (Program.board[i - 1, j].Piece == null)
                                    {
                                        kingCheck.Add(Program.board[i - 1, j]);
                                    }
                                    else if (Program.board[i - 1, j].Piece.IsLight != Program.board[i, j].Piece.IsLight)
                                    {
                                        kingCheck.Add(Program.board[i - 1, j]);
                                    }
                                    if (j - 1 >= 0)
                                    {
                                        if (Program.board[i - 1, j - 1].Piece == null)
                                        {
                                            kingCheck.Add(Program.board[i - 1, j - 1]);
                                        }
                                        else if (Program.board[i - 1, j - 1].Piece.IsLight != Program.board[i, j].Piece.IsLight)
                                        {
                                            kingCheck.Add(Program.board[i - 1, j - 1]);
                                        }
                                    }
                                }
                                if (i + 1 <= 7)
                                {
                                    if (Program.board[i + 1, j].Piece == null)
                                    {
                                        kingCheck.Add(Program.board[i + 1, j]);
                                    }
                                    else if (Program.board[i + 1, j].Piece.IsLight != Program.board[i, j].Piece.IsLight)
                                    {
                                        kingCheck.Add(Program.board[i + 1, j]);
                                    }
                                    if (j + 1 <= 7)
                                    {
                                        if (Program.board[i + 1, j + 1].Piece == null)
                                        {
                                            kingCheck.Add(Program.board[i + 1, j + 1]);
                                        }
                                        else if (Program.board[i + 1, j + 1].Piece.IsLight != Program.board[i, j].Piece.IsLight)
                                        {
                                            kingCheck.Add(Program.board[i + 1, j + 1]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            #region Checks Kings Possible Movemements for Threats
            kingSpot.Piece = null;
            ChessPiece pieceForLater;
            foreach (var location in kingCheck)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (Program.board[i, j].Piece != null)
                        {
                            if (Program.board[i, j].Piece.IsLight != isLight)
                            {
                                pieceForLater = Program.board[i, j].Piece;
                                Program.board[i, j].Piece = null;
                                if (Program.board[i, j].Piece.ValidMovement(Program.board[i, j], location))
                                {
                                    threatenedLocations++;
                                    i = 10;
                                    j = 10;
                                }
                                Program.board[i, j].Piece = pieceForLater;
                            }
                        }
                    }
                }
            }
            kingSpot.Piece = king;
            #endregion
            kingSpot.Piece = king;
            if (kingCheck.Count == threatenedLocations && kingCheck.Count != 0)
            {
                if (CheckAllPlaces(isLight))
                {
                    if (!isBlackTurn)
                        MessageBox.Show("Black Team Wins");
                    if (isBlackTurn)
                        MessageBox.Show("White Team Wins");
                }

            }

            return false;
        }

        private bool CheckAllPlaces(bool isLight)
        {
            ChessPiece pieceForLater = null;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Program.board[i, j].Piece != null && Program.board[i, j].Piece.IsLight == isLight)
                    {
                        Program.board[i, j].Piece.ValidMovement(Program.board[i, j], Program.board[i, j]);
                        foreach (var move in Program.board[i, j].Piece.ValidMoves)
                        {
                            pieceForLater = Program.board[FileLogic.GetColumnFromChar(move.Column).GetHashCode(), move.Row].Piece;
                            Program.board[FileLogic.GetColumnFromChar(move.Column).GetHashCode(), move.Row].Piece = Program.board[i, j].Piece;
                            Program.board[i, j].Piece = null;
                            int k;
                            int l;
                            for (k = 0; k < 8; k++)
                            {
                                for (l = 0; l < 8; l++)
                                {
                                    if (Program.board[k, l].Piece != null && Program.board[k, l].Piece.IsLight != isLight)
                                    {
                                        if (Program.board[k, l].Piece.Check(Program.board[k, l], Program.board[k, l]))
                                        {
                                            k = 9;
                                            l = 9;
                                        }
                                    }
                                }
                            }
                            Program.board[i, j].Piece = Program.board[FileLogic.GetColumnFromChar(move.Column).GetHashCode(), move.Row].Piece;
                            Program.board[FileLogic.GetColumnFromChar(move.Column).GetHashCode(), move.Row].Piece = pieceForLater;
                            if (k == 8)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        List<BoardLogic.ChessCoordinates> reset = new List<BoardLogic.ChessCoordinates>();

        private void ImageLocation(object sender, MouseButtonEventArgs e)
        {
            char[] Columns = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            clickedLocation = (Board)sender;
            #region Chooses a Living Piece
            if (savedLocation.imgChessPiece.Source == null)
            {
                if (clickedLocation.imgChessPiece.Source != null)
                {
                    if ((clickedLocation.imgChessPiece.Source.ToString().Contains("WhitePiece") && !isBlackTurn) || (clickedLocation.imgChessPiece.Source.ToString().Contains("BlackPiece") && isBlackTurn))
                    {
                        clickedLocation.background.Fill = Brushes.LightBlue;
                        savedLocation = clickedLocation;
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                if (GUIBoard[i][j] == savedLocation)
                                {
                                    column = i;
                                    row = j;
                                }
                            }
                        }
                        reset.ForEach(r =>
                        {
                            if ((bool)GUIBoard[FileLogic.GetColumnFromChar(r.Column).GetHashCode()][r.Row].cbxIsBlack.IsChecked)
                            {
                                GUIBoard[FileLogic.GetColumnFromChar(r.Column).GetHashCode()][r.Row].background.Fill = Brushes.OrangeRed;
                            }
                            else
                            {
                                GUIBoard[FileLogic.GetColumnFromChar(r.Column).GetHashCode()][r.Row].background.Fill = Brushes.Pink;
                            }
                        });
                        reset.Clear();
                        if (Program.board[column, row].Piece != null)
                        {
                            Program.board[column, row].Piece.ValidMovement(Program.board[column, row], Program.board[column, row]);
                            List<BoardLogic.ChessCoordinates> moves = Program.board[column, row].Piece.ValidMoves;
                            foreach (var move in moves)
                            {
                                GUIBoard[FileLogic.GetColumnFromChar(move.Column).GetHashCode()][move.Row].background.Fill = Brushes.LightBlue;
                                reset.Add(move);
                            }
                        }
                    }
                }
            }
            #endregion
            #region If same location is clicked
            else if (savedLocation.Name.ToString() == clickedLocation.Name.ToString())
            {
                if (!(bool)savedLocation.cbxIsBlack.IsChecked)
                {
                    savedLocation.background.Fill = Brushes.Pink;
                }
                else if ((bool)savedLocation.cbxIsBlack.IsChecked)
                {
                    savedLocation.background.Fill = Brushes.OrangeRed;
                }
                reset.ForEach(r =>
                {
                    if ((bool)GUIBoard[FileLogic.GetColumnFromChar(r.Column).GetHashCode()][r.Row].cbxIsBlack.IsChecked)
                    {
                        GUIBoard[FileLogic.GetColumnFromChar(r.Column).GetHashCode()][r.Row].background.Fill = Brushes.OrangeRed;
                    }
                    else
                    {
                        GUIBoard[FileLogic.GetColumnFromChar(r.Column).GetHashCode()][r.Row].background.Fill = Brushes.Pink;
                    }
                });
                reset.Clear();
                savedLocation = new Board();
            }
            #endregion
            #region if same color is clicked
            else if ((clickedLocation.imgChessPiece.Source != null && savedLocation.imgChessPiece.Source != null) && (savedLocation.imgChessPiece.Source.ToString().Contains("WhitePiece") && clickedLocation.imgChessPiece.Source.ToString().Contains("WhitePiece") || savedLocation.imgChessPiece.Source.ToString().Contains("BlackPiece") && clickedLocation.imgChessPiece.Source.ToString().Contains("BlackPiece")))
            {
                if (!(bool)savedLocation.cbxIsBlack.IsChecked)
                {
                    savedLocation.background.Fill = Brushes.Pink;
                }
                else if ((bool)savedLocation.cbxIsBlack.IsChecked)
                {
                    savedLocation.background.Fill = Brushes.OrangeRed;
                }
                savedLocation = new Board();
                clickedLocation.background.Fill = Brushes.LightBlue;
                savedLocation = clickedLocation;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (GUIBoard[i][j] == savedLocation)
                        {
                            column = i;
                            row = j;
                        }
                    }
                }
                reset.ForEach(r =>
                {
                    if ((bool)GUIBoard[FileLogic.GetColumnFromChar(r.Column).GetHashCode()][r.Row].cbxIsBlack.IsChecked)
                    {
                        GUIBoard[FileLogic.GetColumnFromChar(r.Column).GetHashCode()][r.Row].background.Fill = Brushes.OrangeRed;
                    }
                    else
                    {
                        GUIBoard[FileLogic.GetColumnFromChar(r.Column).GetHashCode()][r.Row].background.Fill = Brushes.Pink;
                    }
                });
                reset.Clear();
                if (Program.board[column, row].Piece != null)
                {
                    Program.board[column, row].Piece.ValidMovement(Program.board[column, row], Program.board[column, row]);
                    List<BoardLogic.ChessCoordinates> moves = Program.board[column, row].Piece.ValidMoves;
                    foreach (var move in moves)
                    {
                        GUIBoard[FileLogic.GetColumnFromChar(move.Column).GetHashCode()][move.Row].background.Fill = Brushes.LightBlue;
                        reset.Add(move);
                    }
                }
            }
            #endregion
            else
            {
                ValidateMovement();
            }
            if (isBlackTurn)
            {
                emptySpot2.Content = "Black Turn";
                emptySpot2.Foreground = Brushes.White;
            }
            else
            {
                emptySpot2.Content = "White Turn";
            }

        }

        int number = 0;

        private void PromotionValidation()
        {
            for (int i = 0; i < 8; i++)
            {
                if (GUIBoard[0][i].imgChessPiece.Source != null)
                {
                    if (GUIBoard[0][i].imgChessPiece.Source.ToString().Contains("Pawn"))
                    {
                        Board pawn = GUIBoard[0][i];
                        string packUri = "";
                        if (pawn.imgChessPiece.Source.ToString().Contains("WhitePiece"))
                        {
                            if (promotionStatus.Text.ToString().Contains("Queen"))
                            {
                                number = i;
                                Queen queen = new Queen(true);
                                Program.board[0, i].Piece = queen;
                                packUri = "pack://application:,,,/ChessWPF;component/Images/WhitePieces/WhiteQueen.png";
                            }
                            else if (promotionStatus.Text.ToString().Contains("Bishop"))
                            {
                                Bishop bishop = new Bishop(true);
                                Program.board[0, i].Piece = bishop;
                                packUri = "pack://application:,,,/ChessWPF;component/Images/WhitePieces/WhiteBishop.png";
                            }
                            else if (promotionStatus.Text.ToString().Contains("Rook"))
                            {
                                Rook rook = new Rook(true);
                                Program.board[0, i].Piece = rook;
                                packUri = "pack://application:,,,/ChessWPF;component/Images/WhitePieces/WhiteRook.png";
                            }
                            else if (promotionStatus.Text.ToString().Contains("Knight"))
                            {
                                Knight knight = new Knight(true);
                                Program.board[0, i].Piece = knight;
                                packUri = "pack://application:,,,/ChessWPF;component/Images/WhitePieces/WhiteKnight.png";
                            }
                            GUIBoard[0][i].imgChessPiece.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
                        }
                    }
                }
                if (GUIBoard[7][i].imgChessPiece.Source != null)
                {
                    if (GUIBoard[7][i].imgChessPiece.Source.ToString().Contains("Pawn"))
                    {
                        Board pawn = GUIBoard[7][i];
                        string packUri = "";
                        if (pawn.imgChessPiece.Source.ToString().Contains("BlackPiece"))
                        {
                            if (promotionStatus.Text.ToString().Contains("Queen"))
                            {
                                Queen queen = new Queen();
                                queen.IsLight = false;
                                Program.board[7, i].Piece = queen;
                                packUri = "pack://application:,,,/ChessWPF;component/Images/BlackPieces/BlackQueen.png";
                            }
                            else if (promotionStatus.Text.ToString().Contains("Bishop"))
                            {
                                Bishop bishop = new Bishop();
                                bishop.IsLight = false;
                                Program.board[7, i].Piece = bishop;
                                packUri = "pack://application:,,,/ChessWPF;component/Images/BlackPieces/BlackBishop.png";
                            }
                            else if (promotionStatus.Text.ToString().Contains("Rook"))
                            {
                                Rook rook = new Rook();
                                rook.IsLight = false;
                                Program.board[7, i].Piece = rook;
                                packUri = "pack://application:,,,/ChessWPF;component/Images/BlackPieces/BlackRook.png";
                            }
                            else if (promotionStatus.Text.ToString().Contains("Knight"))
                            {
                                Knight knight = new Knight();
                                knight.IsLight = false;
                                Program.board[7, i].Piece = knight;
                                packUri = "pack://application:,,,/ChessWPF;component/Images/BlackPieces/BlackKnight.png";
                            }
                            GUIBoard[7][i].imgChessPiece.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
                        }
                    }
                }
            }
        }
    }
}
