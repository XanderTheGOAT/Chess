using Chess.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Program
    {
        public static ChessCoordinates[,] board = new ChessCoordinates[8, 8];
        static void Main(string[] args)
        {
            //string fileMessage = "";
            //FileRead(args[0], ref fileMessage);
            //var lines = File.ReadLines($"../../{"MoveTests1.txt"}");
            //string[] splitString = SplitString('\n', fileMessage);
            InitializeBoard();
            ParseInput("Qlb3");
            ParseInput("Kda2");
            ParseInput("Pla3");
            ParseInput("Rdh4");
            ParseInput("b3 a2 a3 h4");
            //foreach (string line in lines)
            //{
            //    ParseInput(line);
            //}
            DrawChessBoard();
        }

        #region fileParsing
        static void FileRead(string path, ref string text)
        {
            string fullpath = Path.GetFullPath(path);
            text = File.ReadAllText(fullpath);
        }

        static string[] SplitString(char symbol, string textBeingSplit)
        {
            return textBeingSplit.Split(symbol);
        }
        #region Parsing Data

        static void ParseInput(string toParse)
        {
            char[] acceptableColumns = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
            if (toParse.Length == 4)
            {
                if (!acceptableColumns.Contains(toParse[2]) || int.Parse(toParse[3].ToString()) < 1 || int.Parse(toParse[3].ToString()) > 8)
                {

                }
                else
                {
                    Console.WriteLine(ParsePiecePlacement(toParse));
                }
            }
            else if (toParse.Length == 5)
            {
                if (!acceptableColumns.Contains(toParse[0]) || int.Parse(toParse[1].ToString()) < 1 || int.Parse(toParse[1].ToString()) > 8 || !acceptableColumns.Contains(toParse[3]) || int.Parse(toParse[4].ToString()) < 1 || int.Parse(toParse[4].ToString()) > 8)
                {

                }
                else
                {
                    Console.WriteLine(ParsePieceMovement(toParse));
                }
            }
            else if (toParse.Length == 11)
            {
                Console.WriteLine(ParseCastling(toParse));
            }
        }

        static string ParsePieceMovement(string movement)
        {
            string output = "";

            ChessCoordinates cc1 = Coordinates(movement.Substring(0, 2));

            ChessCoordinates cc2 = Coordinates(movement.Substring(3));

            ColumnCoordinates col1 = GetColumnFromChar(cc1.Column);

            ColumnCoordinates col2 = GetColumnFromChar(cc2.Column);

            if (board[(col1.GetHashCode()), (cc1.Row - 1)].Piece != null)
            {
                board[(col2.GetHashCode()), (cc2.Row - 1)].Piece = board[(col1.GetHashCode()), (cc1.Row - 1)].Piece;
                board[(col1.GetHashCode()), (cc1.Row - 1)].Piece = null;
            }

            output = $"The piece at {cc1.ToString()} has moved to {cc2.ToString()}.";

            return output;
        }

        static ColumnCoordinates GetColumnFromChar(char column)
        {
            switch (char.ToUpper(column))
            {
                case 'A':
                    return ColumnCoordinates.A;
                case 'B':
                    return ColumnCoordinates.B;
                case 'C':
                    return ColumnCoordinates.C;
                case 'D':
                    return ColumnCoordinates.D;
                case 'E':
                    return ColumnCoordinates.E;
                case 'F':
                    return ColumnCoordinates.F;
                case 'G':
                    return ColumnCoordinates.G;
                case 'H':
                    return ColumnCoordinates.H;
            }
            return ColumnCoordinates.A;
        }

        static string ParsePiecePlacement(string message)
        {
            string output = "";
            string color = "";
            string piece = "";
            if (string.Equals(message[1].ToString(), "l", StringComparison.CurrentCultureIgnoreCase))
            {
                color = "White";
            }
            else if (string.Equals(message[1].ToString(), "d", StringComparison.CurrentCultureIgnoreCase))
            {
                color = "Black";
            }
            switch (message[0])
            {
                case 'Q':
                    piece = "Q";
                    output += $"Place the {color} Queen at ";
                    break;
                case 'K':
                    piece = "K";
                    output += $"Place the {color} King at ";
                    break;
                case 'B':
                    piece = "B";
                    output += $"Place the {color} Bishop at ";
                    break;
                case 'N':
                    piece = "N";
                    output += $"Place the {color} Knight at ";
                    break;
                case 'R':
                    piece = "R";
                    output += $"Place the {color} Rook at ";
                    break;
                case 'P':
                    piece = "P";
                    output += $"Place the {color} Pawn at ";
                    break;
            }
            ChessCoordinates cc = Coordinates(message.Substring(2));
            cc.Piece.IsLight = (color == "White") ? true : false;
            InitialPlacement(piece, color, cc);
            return output += cc.ToString();
        }

        static void InitialPlacement(string piece, string color, ChessCoordinates coordinates)
        {
            ColumnCoordinates column = GetColumnFromChar(coordinates.Column);
            switch (piece)
            {
                case "Q":
                    board[(coordinates.Row - 1), (column.GetHashCode())].Piece = new Queen();
                    break;
                case "K":
                    board[(coordinates.Row - 1), (column.GetHashCode())].Piece = new King();
                    break;
                case "B":
                    board[(coordinates.Row - 1), (column.GetHashCode())].Piece = new Bishop();
                    break;
                case "N":
                    board[(coordinates.Row - 1), (column.GetHashCode())].Piece = new Knight();
                    break;
                case "R":
                    board[(coordinates.Row - 1), (column.GetHashCode())].Piece = new Rook();
                    break;
                case "P":
                    board[(coordinates.Row - 1), (column.GetHashCode())].Piece = new Pawn();
                    break;
            }
        }

        static string ParseCastling(string move)
        {
            string output = "";

            string[] moves = move.Split(' ');

            ChessCoordinates cc1 = Coordinates(moves[0]);
            ChessCoordinates cc2 = Coordinates(moves[1]);
            ChessCoordinates cc3 = Coordinates(moves[2]);
            ChessCoordinates cc4 = Coordinates(moves[3]);

            CastleMovement(cc1, cc2, cc3, cc4);

            output = $"The piece at {cc1.ToString()} moved to {cc2.ToString()} and the piece at {cc3.ToString()} moved to {cc4.ToString()}.";

            return output;
        }

        static void CastleMovement(ChessCoordinates Piece1MoveFrom, ChessCoordinates Piece1MoveTo, ChessCoordinates Piece2MoveFrom, ChessCoordinates Piece2MoveTo)
        {
            board[Piece1MoveTo.Row-1, GetColumnFromChar(Piece1MoveTo.Column).GetHashCode()].Piece = board[Piece1MoveFrom.Row-1, GetColumnFromChar(Piece1MoveFrom.Column).GetHashCode()].Piece;
            board[Piece1MoveFrom.Row-1, GetColumnFromChar(Piece1MoveFrom.Column).GetHashCode()].Piece = null;

            board[Piece2MoveTo.Row-1, GetColumnFromChar(Piece2MoveTo.Column).GetHashCode()].Piece = board[Piece2MoveFrom.Row-1, GetColumnFromChar(Piece2MoveFrom.Column).GetHashCode()].Piece;
            board[Piece2MoveFrom.Row-1, GetColumnFromChar(Piece2MoveFrom.Column).GetHashCode()].Piece = null;
        }
        #endregion
        #endregion

        #region ChessBoard Logic
        public enum ColumnCoordinates
        {
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H
        }

        public struct ChessCoordinates
        {
            char column;
            int row;
            ChessPiece piece;

            public ChessCoordinates(char column, int row, ChessPiece piece1) : this()
            {
                Column = column;
                Row = row;
                Piece = piece1;
            }

            public char Column { get; set; }

            public int Row { get; set; }

            public ChessPiece Piece { get; set; }

            override
            public string ToString()
            {
                return $"Column: {Column}, Row: {Row}";
            }
        }

        static void InitializeBoard()
        {
            char[] columnLetters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = new ChessCoordinates(columnLetters[i], j, null);
                }
            }
        }

        static void DrawChessBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                Console.Write($"  {ColumnCoordinates.A + i}");
            }
            Console.WriteLine();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.Write($"{1 + i}");
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (i % 2 == 0)
                    {
                        if (board[i, j].Piece == null && j % 2 == 0)
                        {
                            Console.Write(" - ");
                        }
                        else if (board[i, j].Piece == null && j % 2 == 1)
                        {
                            Console.Write(" + ");
                        }
                        else
                        {
                            Console.Write($" {board[i, j].Piece.ToString()} ");
                        }
                    }
                    else if (i % 2 == 1)
                    {
                        if (board[i, j].Piece == null && j % 2 == 0)
                        {
                            Console.Write(" + ");
                        }
                        else if (board[i, j].Piece == null && j % 2 == 1)
                        {
                            Console.Write(" - ");
                        }
                        else
                        {
                            Console.Write($" {board[i, j].Piece.ToString()} ");
                        }
                    }
                }
                Console.WriteLine($" {1 + i} ");
            }
            for (int i = 0; i < 8; i++)
            {
                Console.Write($"  {ColumnCoordinates.A + i}");
            }
            Console.WriteLine();
        }

        static ChessCoordinates Coordinates(string movement)
        {
            int.TryParse(movement[1].ToString(), out int number);
            ChessCoordinates coordinates = new ChessCoordinates(movement[0], number, null);
            return coordinates;
        }
        #endregion

        static void PlaceOnBoard(ChessCoordinates currentPlace, ChessCoordinates placeToGo)
        {

            //need a board to do this part but I'm gonna remove the piece from its space on the board and put it at the new space
            //actually wtf
        }
    }
}
