﻿using System;
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
<<<<<<< HEAD:Controllers/Program.cs
            string fileMessage = "";
            FileRead("C:\\Neumont Apps\\Visual Studio\\Project Course\\ChessRepo\\Chess\\Chess.txt", ref fileMessage);
            string[] splitString = SplitString('\n', fileMessage);
            foreach(string line in splitString)
            {
                ParseInput(line);
            }
=======
            //string fileMessage = "";
            //FileRead("C:\\Neumont Apps\\Visual Studio\\Project Course\\Chess\\Chess.txt", ref fileMessage);
            //string[] splitString = SplitString('\n', fileMessage);
            //foreach(string line in splitString)
            //{
            //    ParseInput(line);
            //}
            ParseInput("d1 f4 ");
>>>>>>> master:Views/Program.cs
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

        static ChessCoordinates Coordinates(string movement)
        {
            int.TryParse(movement[1].ToString(), out int number);
            ChessCoordinates coordinates = new ChessCoordinates(movement[0], number);
            return coordinates;
        }
    }
}
