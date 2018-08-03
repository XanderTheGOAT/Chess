using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Chess.Models;

namespace Chess
{
    class Program
    {
        static ChessCoordinates[,] board = new ChessCoordinates[8, 8];

        static void Main(string[] args)
        {
            string fileMessage = "";
            FileRead("Chess.txt", ref fileMessage);
            string[] splitString = SplitString('\n', fileMessage);
            DrawChessBoard(board);
            foreach (string line in splitString)
            {
                ParseInput(line);
            }
<<<<<<< Updated upstream
=======
            ChessCoordinates[,] board = new ChessCoordinates[8, 8];
            DrawChessBoard(board);
>>>>>>> Stashed changes
        }

        #region fileParsing
        static void FileRead(string path, ref string text)
        {
<<<<<<< Updated upstream
            string fullpath = Path.GetFullPath(path);
=======
            string fullpath = Path.GetFullPath(path);
>>>>>>> Stashed changes
            text = File.ReadAllText(fullpath);
        }

        static string[] SplitString(char symbol, string textBeingSplit)
        {
            return textBeingSplit.Split(symbol);
        }
<<<<<<< Updated upstream

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
=======
        #region Parsing Data
        static void ParseInput(string toParse)
        {
>>>>>>> Stashed changes

        static void ParseInput(string toParse)
        {            
            
            if (toParse.Length == 5)
            {
                Console.WriteLine(parsePiecePlacement(toParse));
            }
            else if(toParse.Length == 6)
            {
                parsePieceMovement(toParse);
            }
            else if(toParse.Length == 12)
            {
                parseCastling(toParse);
            }
        }

        static string parsePieceMovement(string movement)
        {
            string output = "";

            ChessCoordinates cc1 = Coordinates(movement.Substring(0, 2));

            ChessCoordinates cc2 = Coordinates(movement.Substring(3));

            output = $"The piece at {cc1.ToString()} has moved to {cc2.ToString()}.";

            return output;
        }

        static string parsePiecePlacement(string message)
        {
            string output = "";
            string color = "";
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
                    output += $"Place the {color} Queen at ";
                    break;
                case 'K':
                    output += $"Place the {color} King at ";
                    break;
                case 'B':
                    output += $"Place the {color} Bishop at ";
                    break;
                case 'N':
                    output += $"Place the {color} Knight at ";
                    break;
                case 'R':
                    output += $"Place the {color} Rook at ";
                    break;
                case 'P':
                    output += $"Place the {color} Pawn at ";
                    break;
            }
            ChessCoordinates cc = Coordinates(message.Substring(2));
            return output += cc.ToString();
        }

        static string parseCastling(string move)
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
            char Column;
            int Row;
<<<<<<< Updated upstream
            ChessPiece piece;

=======
>>>>>>> Stashed changes
            public ChessCoordinates(char column, int row, ChessPiece piece = null) : this()
            {
                Column = column;
                Row = row;
                Piece = piece;
            }

            public ChessPiece Piece { get; set; }

            override
            public string ToString()
            {
                return $"Column: {Column}, Row: {Row}";
            }
        }

        static void DrawChessBoard(ChessCoordinates[,] coordinates)
        {
<<<<<<< Updated upstream
            for (int i = 0; i < 8; i++)
            {
                Console.Write($"  {ColumnCoordinates.A + i}");
            }
            Console.WriteLine();
            for (int i = 0; i < coordinates.GetLength(0); i++)
            {
                Console.Write($"{1 + i}");
                for (int j = 0; j < coordinates.GetLength(1); j++)
                {
                    if (i % 2 == 0)
                    {
                        if (coordinates[i, j].Piece == null && j % 2 == 0)
                        {
                            Console.Write(" - ");
                        }
                        else if (coordinates[i, j].Piece == null && j % 2 == 1)
                        {
                            Console.Write(" + ");
                        }
                    }
                    else if (i % 2 == 1)
                    {
                        if (coordinates[i, j].Piece == null && j % 2 == 0)
                        {
                            Console.Write(" + ");
                        }
                        else if (coordinates[i, j].Piece == null && j % 2 == 1)
                        {
                            Console.Write(" - ");
                        }
                    }
                }
                Console.WriteLine($" {1 + i} ");
            }
            for (int i = 0; i < 8; i++)
            {
                Console.Write($"  {ColumnCoordinates.A + i}");
            }
=======
            for (int i = 0; i < 8; i++)
            {
                Console.Write($"  {ColumnCoordinates.A + i}");
            }
            Console.WriteLine();
            for (int i = 0; i < coordinates.GetLength(0); i++)
            {
                Console.Write($"{1 + i}");
                for (int j = 0; j < coordinates.GetLength(1); j++)
                {
                    if (i % 2 == 0)
                    {
                        if (coordinates[i, j].Piece == null && j % 2 == 0)
                        {
                            Console.Write(" - ");
                        }
                        else if (coordinates[i, j].Piece == null && j % 2 == 1)
                        {
                            Console.Write(" + ");
                        }
                    }
                    else if (i % 2 == 1)
                    {
                        if (coordinates[i, j].Piece == null && j % 2 == 0)
                        {
                            Console.Write(" + ");
                        }
                        else if (coordinates[i, j].Piece == null && j % 2 == 1)
                        {
                            Console.Write(" - ");
                        }
                    }
                }
                Console.WriteLine($" {1 + i} ");
            }
            for (int i = 0; i < 8; i++)
            {
                Console.Write($"  {ColumnCoordinates.A + i}");
            }
>>>>>>> Stashed changes
            Console.WriteLine();
        }



        static ChessCoordinates Coordinates(string movement)
        {
            int.TryParse(movement[1].ToString(), out int number);
            ChessCoordinates coordinates = new ChessCoordinates(movement[0], number);
            return coordinates;
        }
        #endregion
    }
}
