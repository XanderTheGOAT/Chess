using Chess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    class King: ChessPiece
    {
        bool isLight;

        public bool IsLight { get; set; }

        public override bool ValidMovement(Program.ChessCoordinates startLocation, Program.ChessCoordinates endLocation)
        {
            List<Program.ChessCoordinates> validMoves = new List<Program.ChessCoordinates>();

            //validate movement eventually
            int column = Program.GetColumnFromChar(startLocation.Column).GetHashCode();
            int row = startLocation.Row-1;
            Console.WriteLine(startLocation);
            Console.WriteLine(endLocation);
            Console.WriteLine(row);
            Console.WriteLine(column);

            if (row-1 >= 0)
            {
                if (Program.board[row-1, column].Piece == null)
                {
                    validMoves.Add(Program.board[row-1, column]);
                }
                else if (Program.board[row-1, column].Piece.IsLight != Program.board[row, column].Piece.IsLight)
                {
                    validMoves.Add(Program.board[row-1, column]);
                }
            }
            if (row + 1 <= 7)
            {
                if (Program.board[row+1, column].Piece == null)
                {
                    validMoves.Add(Program.board[row+1, column]);
                }
                else if (Program.board[row+1, column].Piece.IsLight != Program.board[row, column].Piece.IsLight)
                {
                    validMoves.Add(Program.board[row+1, column]);
                }
            }
            if (column - 1 >= 0)
            {
                if (Program.board[row, column-1].Piece == null || Program.board[row, column-1].Piece.IsLight != Program.board[row, column].Piece.IsLight)
                {
                    validMoves.Add(Program.board[row, column-1]);
                }
                else if (Program.board[row, column-1].Piece.IsLight != Program.board[row, column].Piece.IsLight)
                {
                    validMoves.Add(Program.board[row, column-1]);
                }
            }
            if (column + 1 <= 7)
            {
                if (Program.board[row, column+1].Piece == null || Program.board[row, column+1].Piece.IsLight != Program.board[row, column].Piece.IsLight)
                {
                    validMoves.Add(Program.board[row, column+1]);
                }
                else if (Program.board[row, column + 1].Piece.IsLight != Program.board[row, column].Piece.IsLight)
                {
                    validMoves.Add(Program.board[row, column + 1]);
                }
            }
            
            Program.ChessCoordinates lookingFor = new Program.ChessCoordinates(Program.GetCharFromNumber(endLocation.Row-1), Program.GetColumnFromChar(endLocation.Column).GetHashCode(), null);
            Console.WriteLine("Looking for: " + lookingFor);
            foreach (var space in validMoves)
            {
                Console.WriteLine(space);
                if (space == lookingFor)
                {
                    return true;
                }
            }
            return false;
        }

        override
        public string ToString()
        {
            return "K";
        }
    }
}
