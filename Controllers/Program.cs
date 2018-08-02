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
        static void Main(string[] args)
        {
            string fileMessage = "";
            //FileRead("C:\\Neumont Apps\\Visual Studio\\Project Course\\ChessRepo\\Chess\\Chess.txt", ref fileMessage);
            string[] splitString = SplitString('\n', fileMessage);
            foreach (string line in splitString)
            {
                ParseInput(args[0] + "\n");
            }
        }

        static void FileRead(string path, ref string text)
        {
            text = File.ReadAllText(path);
        }

        static string[] SplitString(char symbol, string textBeingSplit)
        {
            return textBeingSplit.Split(symbol);
        }

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

        struct ChessCoordinates
        {
            char Column;
            int Row;
            public ChessCoordinates(char column, int row)
            {
                Column = column;
                Row = row;
            }

            override
            public string ToString()
            {
                return $"Column: {Column}, Row: {Row}";
            }
        }

        static void DrawChessBoard()
        {

        }



        static ChessCoordinates Coordinates(string movement)
        {
            int.TryParse(movement[1].ToString(), out int number);
            ChessCoordinates coordinates = new ChessCoordinates(movement[0], number);
            return coordinates;
        }
    }
}
