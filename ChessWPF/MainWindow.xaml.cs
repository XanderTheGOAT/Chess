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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


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
            }
            DrawBoard();
        }

        private void DrawBoard()
        {
            char[] Columns = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

            Label emptySpot = new Label();
            ugChessBoard.Children.Add(emptySpot);
            for (int i = 0; i < 8; i++)
            {
                Label column = new Label();
                column.Foreground = Brushes.White;
                column.Content = Columns[i].ToString();
                ugChessBoard.Children.Add(column);
            }
            Label emptySpot2 = new Label();
            ugChessBoard.Children.Add(emptySpot2);

            for (int i = 0; i < 8; i++)
            {
                Label rowNumber = new Label();
                rowNumber.Content = i + 1;
                rowNumber.Foreground = Brushes.White;
                ugChessBoard.Children.Add(rowNumber);
                for (int j = 0; j < 8; j++)
                {
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
                column.Foreground = Brushes.White;
                column.Content = Columns[i].ToString();
                ugChessBoard.Children.Add(column);
            }
            Label emptySpot4 = new Label();
            ugChessBoard.Children.Add(emptySpot4);

            Program.board[0, 0].Piece = King;
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
                                    case "King":
                                        var image = new ImageBrush();
                                        image.ImageSource = new BitmapImage(new Uri("/ChessPieces/White Pieces/WhiteKing.png", UriKind.Relative));
                                        GUIBoard[i][j].imgChessPiece.Source = image.ImageSource;
                                        break;
                                    case "Queen":
                                        break;
                                    case "Knight":
                                        break;
                                    case "Bishop":
                                        break;
                                    case "Rook":
                                        break;
                                    case "Pawn":
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case false:
                                switch (Program.board[i, j].Piece.ToString())
                                {
                                    case "King":
                                        break;
                                    case "Queen":
                                        break;
                                    case "Knight":
                                        break;
                                    case "Bishop":
                                        break;
                                    case "Rook":
                                        break;
                                    case "Pawn":
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

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image)sender;
            Border border = (Border)image.Parent;

            image.CaptureMouse();

        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

    }

}
