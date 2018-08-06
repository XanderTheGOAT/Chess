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
        static ChessCoordinates[,] board = new ChessCoordinates[8, 8];
        static void Main(string[] args)
        {
            //string fileMessage = "";
            //FileRead(args[0], ref fileMessage);
            //var lines = File.ReadLines($"../../{"MoveTests1.txt"}");
            //string[] splitString = SplitString('\n', fileMessage);
            //foreach (string line in lines)
            //{
            //    ParseInput(line);
            //}
            ParseInput("Qla1");
            ParseInput("a1 b2");
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

            if (toParse.Length == 4)
            {
                Console.WriteLine(ParsePiecePlacement(toParse));
            }
            else if (toParse.Length == 5)
            {
                ParsePieceMovement(toParse);

                Console.WriteLine(ParsePieceMovement(toParse));
            }
            else if (toParse.Length == 11)
            {
                ParseCastling(toParse);
            }
        }

        static string ParsePieceMovement(string movement)
        {
            string output = "";

            ChessCoordinates cc1 = Coordinates(movement.Substring(0, 2));

            ChessCoordinates cc2 = Coordinates(movement.Substring(3));

            ColumnCoordinates col1 = GetColumnFromChar(cc1.Column);

            ColumnCoordinates col2 = GetColumnFromChar(cc2.Column);

            board[(col2.GetHashCode() + 1), (cc2.Row + 1)].Piece = board[(col1.GetHashCode() + 1), (cc1.Row + 1)].Piece;
            board[(col1.GetHashCode() + 1), (cc1.Row + 1)].Piece = null;

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
            InitialPlacement(piece, color, cc);
            return output += cc.ToString();
        }

        static void InitialPlacement(string piece, string color, ChessCoordinates coordinates)
        {
            ColumnCoordinates column = GetColumnFromChar(coordinates.Column);
            switch (piece)
            {
                case "Q":
                    board[(column.GetHashCode() + 1), (coordinates.Row + 1)].Piece = new Queen();
                    break;
                case "K":
                    board[(column.GetHashCode() + 1), (coordinates.Row + 1)].Piece = new King();
                    break;
                case "B":
                    board[(column.GetHashCode() + 1), (coordinates.Row + 1)].Piece = new Bishop();
                    break;
                case "N":
                    board[(column.GetHashCode() + 1), (coordinates.Row + 1)].Piece = new Knight();
                    break;
                case "R":
                    board[(column.GetHashCode() + 1), (coordinates.Row + 1)].Piece = new Rook();
                    break;
                case "P":
                    board[(column.GetHashCode() + 1), (coordinates.Row + 1)].Piece = new Pawn();
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

            output = $"The piece at {cc1.ToString()} moved to {cc2.ToString()} and the piece at {cc3.ToString()} moved to {cc4.ToString()}.";

            return output;
        }
        #endregion
        #endregion

        #region ChessBoard Logic
        enum ColumnCoordinates
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

        struct ChessCoordinates
        {
            char column;
            int row;
            public ChessCoordinates(char column, int row, ChessPiece piece = null) : this()
            {
                Column = column;
                Row = row;
                Piece = piece;
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
            ChessCoordinates coordinates = new ChessCoordinates(movement[0], number);
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
