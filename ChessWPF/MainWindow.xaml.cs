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

        List<List<Board>> GUIBoard = new List<List<Board>>();
        bool isBlackTurn = false;
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
            ugChessBoard.Children.Add(promotionStatus);
            for (int i = 0; i < 8; i++)
            {
                Label column = new Label();
                column.HorizontalContentAlignment = HorizontalAlignment.Center;
                column.Foreground = Brushes.White;
                column.Content = Columns[i].ToString();
                ugChessBoard.Children.Add(column);
            }
            Label emptySpot2 = new Label();
            ugChessBoard.Children.Add(emptySpot2);

            for (int i = 0; i < 8; i++)
            {
                Label rowNumber = new Label();
                rowNumber.HorizontalContentAlignment = HorizontalAlignment.Right;
                rowNumber.Content = i + 1;
                rowNumber.Foreground = Brushes.White;
                ugChessBoard.Children.Add(rowNumber);
                for (int j = 0; j < 8; j++)
                {
                    GUIBoard[i][j].Name = $"{Columns[j]}{i + 1}";
                    GUIBoard[i][j].MouseLeftButtonDown += ImageLocation;
                    ugChessBoard.Children.Add(GUIBoard[i][j]);
                }
                Label endRowNumber = new Label();
                endRowNumber.Foreground = Brushes.White;
                endRowNumber.Content = i + 1;
                ugChessBoard.Children.Add(endRowNumber);
            }
            Label emptySpot3 = new Label();
            ugChessBoard.Children.Add(emptySpot3);
            for (int i = 0; i < 8; i++)
            {
                Label column = new Label();
                column.HorizontalContentAlignment = HorizontalAlignment.Center;
                column.Foreground = Brushes.White;
                column.Content = Columns[i].ToString();
                ugChessBoard.Children.Add(column);
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
            }


        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image)sender;
            Border border = (Border)image.Parent;

            image.CaptureMouse();

        }

        Board clickedLocation = new Board();
        Board savedLocation = new Board();
        int column = 0;
        int row = 0;


        private void ValidateMovement()
        {
            bool validMove = true;
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
                                    King king = new King();
                                    validMove = king.ValidMovement(Program.board[row, column], Program.board[i, j]);
                                    if (validMove)
                                        GUIBoard[i][j].imgChessPiece.Source = savedLocation.imgChessPiece.Source;
                                }
                                else if (savedLocation.imgChessPiece.Source.ToString().Contains("Queen"))
                                {
                                    Queen queen = new Queen();
                                    validMove = queen.ValidMovement(Program.board[row, column], Program.board[i, j]);
                                    if (validMove)
                                        GUIBoard[i][j].imgChessPiece.Source = savedLocation.imgChessPiece.Source;
                                }
                                else if (savedLocation.imgChessPiece.Source.ToString().Contains("Knight"))
                                {
                                    Knight knight = new Knight();
                                    validMove = knight.ValidMovement(Program.board[row, column], Program.board[i, j]);
                                    if (validMove)
                                        GUIBoard[i][j].imgChessPiece.Source = savedLocation.imgChessPiece.Source;
                                }
                                else if (savedLocation.imgChessPiece.Source.ToString().Contains("Rook"))
                                {
                                    Rook rook = new Rook();
                                    validMove = rook.ValidMovement(Program.board[row, column], Program.board[i, j]);
                                    if (validMove)
                                        GUIBoard[i][j].imgChessPiece.Source = savedLocation.imgChessPiece.Source;

                                }
                                else if (savedLocation.imgChessPiece.Source.ToString().Contains("Bishop"))
                                {
                                    Bishop bishop = new Bishop();
                                    validMove = bishop.ValidMovement(Program.board[row, column], Program.board[i, j]);
                                    if (validMove)
                                        GUIBoard[i][j].imgChessPiece.Source = savedLocation.imgChessPiece.Source;

                                }
                                else if (savedLocation.imgChessPiece.Source.ToString().Contains("Pawn"))
                                {
                                    GUIBoard[i][j].imgChessPiece.Source = savedLocation.imgChessPiece.Source;
                                    if (GUIBoard[i][j] == GUIBoard[0][j])
                                    {
                                        GUIBoard[i][j]= PromotionValidation(GUIBoard[i][j]);
                                    }
                                }
                            }
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
                    if (validMove)
                    {
                        savedLocation.imgChessPiece.Source = null;
                        isBlackTurn = isBlackTurn ? false : true;
                    }
                    savedLocation = new Board();
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
                                    validMove = king.ValidMovement(Program.board[row, column], Program.board[i, j]);
                                    if (validMove)
                                        GUIBoard[i][j].imgChessPiece.Source = savedLocation.imgChessPiece.Source;
                                }
                                else if (savedLocation.imgChessPiece.Source.ToString().Contains("Queen"))
                                {
                                    Queen queen = new Queen();
                                    validMove = queen.ValidMovement(Program.board[row, column], Program.board[i, j]);
                                    if (validMove)
                                        GUIBoard[i][j].imgChessPiece.Source = savedLocation.imgChessPiece.Source;
                                }
                                else if (savedLocation.imgChessPiece.Source.ToString().Contains("Knight"))
                                {
                                    Knight knight = new Knight();
                                    validMove = knight.ValidMovement(Program.board[row, column], Program.board[i, j]);
                                    if (validMove)
                                        GUIBoard[i][j].imgChessPiece.Source = savedLocation.imgChessPiece.Source;
                                }
                                else if (savedLocation.imgChessPiece.Source.ToString().Contains("Rook"))
                                {
                                    Rook rook = new Rook();
                                    validMove = rook.ValidMovement(Program.board[row, column], Program.board[i, j]);
                                    if (validMove)
                                        GUIBoard[i][j].imgChessPiece.Source = savedLocation.imgChessPiece.Source;

                                }
                                else if (savedLocation.imgChessPiece.Source.ToString().Contains("Bishop"))
                                {
                                    Bishop bishop = new Bishop();
                                    validMove = bishop.ValidMovement(Program.board[row, column], Program.board[i, j]);
                                    if (validMove)
                                        GUIBoard[i][j].imgChessPiece.Source = savedLocation.imgChessPiece.Source;
                                }
                                else if (savedLocation.imgChessPiece.Source.ToString().Contains("Pawn"))
                                {
                                    GUIBoard[i][j].imgChessPiece.Source = savedLocation.imgChessPiece.Source;
                                    if(GUIBoard[i][j] == GUIBoard[7][j])
                                    {
                                        GUIBoard[i][j] = PromotionValidation(GUIBoard[i][j]);
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
                                if (validMove)
                                {
                                    savedLocation.imgChessPiece.Source = null;
                                    isBlackTurn = isBlackTurn ? false : true;
                                }
                                savedLocation = new Board();
                            }
                        }
                    }
                }
                #endregion

            }
        }
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
                savedLocation = new Board();
            }
            #endregion
            else
            {
                ValidateMovement();
            }
        }


        private Board PromotionValidation(Board pawn)
        {
            string packUri = "";
            ChessPiece piece = new Pawn();
            for (int i = 0; i < 8; i++)
            {
                if (pawn.imgChessPiece.Source.ToString().Contains("WhitePiece"))
                {
                    if (promotionStatus.Text.ToString().Contains("Queen"))
                    {
                        packUri = "pack://application:,,,/ChessWPF;component/Images/WhitePieces/WhiteQueen.png";
                    }
                    else if (promotionStatus.Text.ToString().Contains("Bishop"))
                    {
                        packUri = "pack://application:,,,/ChessWPF;component/Images/WhitePieces/WhiteBishop.png";

                    }
                    else if (promotionStatus.Text.ToString().Contains("Rook"))
                    {
                        packUri = "pack://application:,,,/ChessWPF;component/Images/WhitePieces/WhiteRook.png";

                    }
                    else if (promotionStatus.Text.ToString().Contains("Knight"))
                    {
                        packUri = "pack://application:,,,/ChessWPF;component/Images/WhitePieces/WhiteKnight.png";

                    }
                    pawn.imgChessPiece.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
                }
                else if (pawn.imgChessPiece.Source.ToString().Contains("BlackPiece"))
                {
                    if (promotionStatus.Text.ToString().Contains("Queen"))
                    {
                        packUri = "pack://application:,,,/ChessWPF;component/Images/BlackPieces/BlackQueen.png";
                    }
                    else if (promotionStatus.Text.ToString().Contains("Bishop"))
                    {
                        packUri = "pack://application:,,,/ChessWPF;component/Images/BlackPieces/BlackBishop.png";
                    }
                    else if (promotionStatus.Text.ToString().Contains("Rook"))
                    {
                        packUri = "pack://application:,,,/ChessWPF;component/Images/BlackPieces/BlackRook.png";
                    }
                    else if (promotionStatus.Text.ToString().Contains("Knight"))
                    {
                        packUri = "pack://application:,,,/ChessWPF;component/Images/BlackPieces/BlackKnight.png";
                    }
                    pawn.imgChessPiece.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
                }
            }
            return pawn;
        }
    }
}
