using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLibrary.Controllers;

namespace ChessLibrary.Models
{
    public class King: ChessPiece
    {
        bool isLight;

        public bool IsLight { get; set; }

        public King()
        {

        }

        public override bool ValidMovement(BoardLogic.ChessCoordinates startLocation, BoardLogic.ChessCoordinates endLocation)
        {
            List<BoardLogic.ChessCoordinates> validMoves = new List<BoardLogic.ChessCoordinates>();

            //validate movement eventually
            int column = FileLogic.GetColumnFromChar(startLocation.Column).GetHashCode();
            int row = startLocation.Row - 1;
            Console.WriteLine(startLocation);
            Console.WriteLine(endLocation);
            Console.WriteLine(row);
            Console.WriteLine(column);

            if (row - 1 >= 0)
            {
                if (Program.board[row - 1, column].Piece == null)
                {
                    validMoves.Add(Program.board[row - 1, column]);
                }
                else if (Program.board[row - 1, column].Piece.IsLight != Program.board[row, column].Piece.IsLight)
                {
                    validMoves.Add(Program.board[row - 1, column]);
                }
            }
            if (row + 1 <= 7)
            {
                if (Program.board[row + 1, column].Piece == null)
                {
                    validMoves.Add(Program.board[row + 1, column]);
                }
                else if (Program.board[row + 1, column].Piece.IsLight != Program.board[row, column].Piece.IsLight)
                {
                    validMoves.Add(Program.board[row + 1, column]);
                }
            }
            if (column - 1 >= 0)
            {
                if (Program.board[row, column - 1].Piece == null || Program.board[row, column - 1].Piece.IsLight != Program.board[row, column].Piece.IsLight)
                {
                    validMoves.Add(Program.board[row, column - 1]);
                }
                else if (Program.board[row, column - 1].Piece.IsLight != Program.board[row, column].Piece.IsLight)
                {
                    validMoves.Add(Program.board[row, column - 1]);
                }
            }
            if (column + 1 <= 7)
            {
                if (Program.board[row, column + 1].Piece == null || Program.board[row, column + 1].Piece.IsLight != Program.board[row, column].Piece.IsLight)
                {
                    validMoves.Add(Program.board[row, column + 1]);
                }
                else if (Program.board[row, column + 1].Piece.IsLight != Program.board[row, column].Piece.IsLight)
                {
                    validMoves.Add(Program.board[row, column + 1]);
                }
            }

            BoardLogic.ChessCoordinates lookingFor = new BoardLogic.ChessCoordinates(BoardLogic.GetCharFromNumber(endLocation.Row - 1), FileLogic.GetColumnFromChar(endLocation.Column).GetHashCode(), null);
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
