﻿using Chess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary.Controllers
{
    class BoardLogic
    {
        public List<ColumnCoordinates> coordinates = new List<ColumnCoordinates>
        {
            ColumnCoordinates.A,
            ColumnCoordinates.B,
            ColumnCoordinates.C,
            ColumnCoordinates.D,
            ColumnCoordinates.E,
            ColumnCoordinates.F,
            ColumnCoordinates.G,
            ColumnCoordinates.H
        };

        #region Chessboard Logic
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

            public static bool operator ==(ChessCoordinates cc1, ChessCoordinates cc2)
            {
                return (cc1.Column == cc2.Column && cc1.Row == cc2.Row);
            }

            public static bool operator !=(ChessCoordinates cc1, ChessCoordinates cc2)
            {
                return (cc1.Column == cc2.Column && cc1.Row == cc2.Row);
            }
        }

        static void InitializeBoard()
        {
            char[] columnLetters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Program.board[i, j] = new ChessCoordinates(columnLetters[i], j, null);
                }
            }
        }

        static void DrawChessboard()
        {
            for (int i = 0; i < 8; i++)
            {
                Console.Write($"  {ColumnCoordinates.A + i}");
            }
            Console.WriteLine();
            for (int i = 0; i < Program.board.GetLength(0); i++)
            {
                Console.Write($"{1 + i}");
                for (int j = 0; j < Program.board.GetLength(1); j++)
                {
                    if (i % 2 == 0)
                    {
                        if (Program.board[i, j].Piece == null && j % 2 == 0)
                        {
                            Console.Write(" - ");
                        }
                        else if (Program.board[i, j].Piece == null && j % 2 == 1)
                        {
                            Console.Write(" + ");
                        }
                        else
                        {
                            Console.Write($" {Program.board[i, j].Piece.ToString()} ");
                        }
                    }
                    else if (i % 2 == 1)
                    {
                        if (Program.board[i, j].Piece == null && j % 2 == 0)
                        {
                            Console.Write(" + ");
                        }
                        else if (Program.board[i, j].Piece == null && j % 2 == 1)
                        {
                            Console.Write(" - ");
                        }
                        else
                        {
                            Console.Write($" {Program.board[i, j].Piece.ToString()} ");
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

        public static ChessCoordinates Coordinates(string movement)
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
