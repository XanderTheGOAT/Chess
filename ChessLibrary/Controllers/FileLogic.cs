using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary.Controllers
{
    public class FileLogic
    {
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

            BoardLogic.ChessCoordinates cc1 = BoardLogic.Coordinates(movement.Substring(0, 2));

            BoardLogic.ChessCoordinates cc2 = BoardLogic.Coordinates(movement.Substring(3));

            BoardLogic.ColumnCoordinates col1 = GetColumnFromChar(cc1.Column);

            BoardLogic.ColumnCoordinates col2 = GetColumnFromChar(cc2.Column);

            if (Program.board[cc1.Row - 1, col1.GetHashCode()].Piece != null)
            {
                if (Program.board[cc1.Row - 1, col1.GetHashCode()].Piece.ValidMovement(cc1, cc2))
                {
                    Program.board[cc2.Row - 1, col2.GetHashCode()].Piece = Program.board[cc1.Row - 1, col1.GetHashCode()].Piece;
                    Program.board[cc1.Row - 1, col1.GetHashCode()].Piece = null;
                    output = $"The piece at {cc1.ToString()} has moved to {cc2.ToString()}.";
                }
                else
                {
                    output = "Fuck you it didn't work";
                }
            }
            else
            {
                output = $"I'm not seeing a piece at {cc1.ToString()}";
            }


            return output;
        }

        public static char GetCharFromColumn(BoardLogic.ColumnCoordinates column)
        {
            switch (column)
            {
                case BoardLogic.ColumnCoordinates.A:
                    return 'a';
                case BoardLogic.ColumnCoordinates.B:
                    return 'b';
                case BoardLogic.ColumnCoordinates.C:
                    return 'c';
                case BoardLogic.ColumnCoordinates.D:
                    return 'd';
                case BoardLogic.ColumnCoordinates.E:
                    return 'e';
                case BoardLogic.ColumnCoordinates.F:
                    return 'f';
                case BoardLogic.ColumnCoordinates.G:
                    return 'g';
                case BoardLogic.ColumnCoordinates.H:
                    return 'h';
            }
            return 'a';
        }

        public static BoardLogic.ColumnCoordinates GetColumnFromChar(char column)
        {
            switch (char.ToUpper(column))
            {
                case 'A':
                    return BoardLogic.ColumnCoordinates.A;
                case 'B':
                    return BoardLogic.ColumnCoordinates.B;
                case 'C':
                    return BoardLogic.ColumnCoordinates.C;
                case 'D':
                    return BoardLogic.ColumnCoordinates.D;
                case 'E':
                    return BoardLogic.ColumnCoordinates.E;
                case 'F':
                    return BoardLogic.ColumnCoordinates.F;
                case 'G':
                    return BoardLogic.ColumnCoordinates.G;
                case 'H':
                    return BoardLogic.ColumnCoordinates.H;
            }
            return BoardLogic.ColumnCoordinates.A;
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
            BoardLogic.ChessCoordinates cc = BoardLogic.Coordinates(message.Substring(2));
            InitialPlacement(piece, color, cc);
            return output += cc.ToString();
        }

        static void InitialPlacement(string piece, string color, BoardLogic.ChessCoordinates coordinates)
        {
            BoardLogic.ColumnCoordinates column = GetColumnFromChar(coordinates.Column);
            switch (piece)
            {
                case "Q":
                    Program.board[(coordinates.Row - 1), (column.GetHashCode())].Piece = new Chess.Models.Queen();
                    break;
                case "K":
                    Program.board[(coordinates.Row - 1), (column.GetHashCode())].Piece = new Chess.Models.King();
                    break;
                case "B":
                    Program.board[(coordinates.Row - 1), (column.GetHashCode())].Piece = new Chess.Models.Bishop();
                    break;
                case "N":
                    Program.board[(coordinates.Row - 1), (column.GetHashCode())].Piece = new Chess.Models.Knight();
                    break;
                case "R":
                    Program.board[(coordinates.Row - 1), (column.GetHashCode())].Piece = new Chess.Models.Rook();
                    break;
                case "P":
                    Program.board[(coordinates.Row - 1), (column.GetHashCode())].Piece = new Chess.Models.Pawn();
                    break;
            }
            Program.board[(coordinates.Row - 1), (column.GetHashCode())].Piece.IsLight = (color == "White") ? true : false;
            Console.WriteLine("Yeet" + Program.board[(coordinates.Row - 1), (column.GetHashCode())]);
        }

        static string ParseCastling(string move)
        {
            string output = "";

            string[] moves = move.Split(' ');

            BoardLogic.ChessCoordinates cc1 = BoardLogic.Coordinates(moves[0]);
            BoardLogic.ChessCoordinates cc2 = BoardLogic.Coordinates(moves[1]);
            BoardLogic.ChessCoordinates cc3 = BoardLogic.Coordinates(moves[2]);
            BoardLogic.ChessCoordinates cc4 = BoardLogic.Coordinates(moves[3]);

            CastleMovement(cc1, cc2, cc3, cc4);

            output = $"The piece at {cc1.ToString()} moved to {cc2.ToString()} and the piece at {cc3.ToString()} moved to {cc4.ToString()}.";

            return output;
        }

        static void CastleMovement(BoardLogic.ChessCoordinates Piece1MoveFrom, BoardLogic.ChessCoordinates Piece1MoveTo, BoardLogic.ChessCoordinates Piece2MoveFrom, BoardLogic.ChessCoordinates Piece2MoveTo)
        {
            Program.board[Piece1MoveTo.Row - 1, GetColumnFromChar(Piece1MoveTo.Column).GetHashCode()].Piece = Program.board[Piece1MoveFrom.Row - 1, GetColumnFromChar(Piece1MoveFrom.Column).GetHashCode()].Piece;
            Program.board[Piece1MoveFrom.Row - 1, GetColumnFromChar(Piece1MoveFrom.Column).GetHashCode()].Piece = null;

            Program.board[Piece2MoveTo.Row - 1, GetColumnFromChar(Piece2MoveTo.Column).GetHashCode()].Piece = Program.board[Piece2MoveFrom.Row - 1, GetColumnFromChar(Piece2MoveFrom.Column).GetHashCode()].Piece;
            Program.board[Piece2MoveFrom.Row - 1, GetColumnFromChar(Piece2MoveFrom.Column).GetHashCode()].Piece = null;
        }
        #endregion
        #endregion
    }
}
