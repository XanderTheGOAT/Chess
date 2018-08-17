using ChessLibrary.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary.Models
{
    public class Knight : ChessPiece
    {
        public Knight()
        {

        }
        bool isLight;

        public new bool IsLight { get; set; }

        public override bool ValidMovement(BoardLogic.ChessCoordinates startLocation, BoardLogic.ChessCoordinates endLocation)
        {
            //validate movement eventually
            List<BoardLogic.ChessCoordinates> validMoves = new List<BoardLogic.ChessCoordinates>();

            //validate movement eventually
            int column = FileLogic.GetColumnFromChar(startLocation.Column).GetHashCode();
            int row = startLocation.Row;

            if (row - 2 >= 0)
            {
                if (column - 1 >= 0)
                {
                    if (Program.board[column - 1, row - 2].Piece == null)
                    {
                        validMoves.Add(Program.board[column - 1, row - 2]);
                    }
                    else if (Program.board[column - 1, row - 2].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                    {
                        validMoves.Add(Program.board[column - 1, row - 2]);
                    }
                }
                if (column + 1 <= 7)
                {
                    if (Program.board[column + 1, row - 2].Piece == null)
                    {
                        validMoves.Add(Program.board[column + 1, row - 2]);
                    }
                    else if (Program.board[column + 1, row - 2].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                    {
                        validMoves.Add(Program.board[column + 1, row - 2]);
                    }
                }
            }
            if (row + 2 <= 7)
            {
                if (column - 1 >= 0)
                {
                    if (Program.board[column - 1, row + 2].Piece == null)
                    {
                        validMoves.Add(Program.board[column - 1, row + 2]);
                    }
                    else if (Program.board[column - 1, row + 2].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                    {
                        validMoves.Add(Program.board[column - 1, row + 2]);
                    }
                }
                if (column + 1 <= 7)
                {
                    if (Program.board[column + 1, row + 2].Piece == null)
                    {
                        validMoves.Add(Program.board[column + 1, row + 2]);
                    }
                    else if (Program.board[column + 1, row + 2].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                    {
                        validMoves.Add(Program.board[column + 1, row + 2]);
                    }
                }
            }
            if (column - 2 >= 0)
            {
                if (row - 1 >= 0)
                {
                    if (Program.board[column - 2, row - 1].Piece == null)
                    {
                        validMoves.Add(Program.board[column - 2, row - 1]);
                    }
                    else if (Program.board[column - 2, row - 1].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                    {
                        validMoves.Add(Program.board[column - 2, row - 1]);
                    }
                }
                if (row + 1 <= 7)
                {
                    if (Program.board[column - 2, row + 1].Piece == null)
                    {
                        validMoves.Add(Program.board[column - 2, row + 1]);
                    }
                    else if (Program.board[column - 2, row + 1].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                    {
                        validMoves.Add(Program.board[column - 2, row + 1]);
                    }
                }
            }
            if (column + 2 <= 7)
            {
                if (row - 1 >= 0)
                {
                    if (Program.board[column + 2, row - 1].Piece == null)
                    {
                        validMoves.Add(Program.board[column + 2, row - 1]);
                    }
                    else if (Program.board[column + 2, row - 1].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                    {
                        validMoves.Add(Program.board[column + 2, row - 1]);
                    }
                }
                if (row + 1 <= 7)
                {
                    if (Program.board[column + 2, row + 1].Piece == null)
                    {
                        validMoves.Add(Program.board[column + 2, row + 1]);
                    }
                    else if (Program.board[column + 2, row + 1].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                    {
                        validMoves.Add(Program.board[column + 2, row + 1]);
                    }
                }
            }
            BoardLogic.ChessCoordinates lookingFor = new BoardLogic.ChessCoordinates(BoardLogic.GetCharFromNumber(FileLogic.GetColumnFromChar(endLocation.Column).GetHashCode()), endLocation.Row, null);

            foreach (var space in validMoves)
            {
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
            return "N";
        }
    }
}
