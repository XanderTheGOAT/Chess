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

        public new bool IsLight { get; set; }

        public King()
        {

        }

        public override bool ValidMovement(BoardLogic.ChessCoordinates startLocation, BoardLogic.ChessCoordinates endLocation)
        {
            List<BoardLogic.ChessCoordinates> validMoves = new List<BoardLogic.ChessCoordinates>();

            //validate movement eventually
            int column = FileLogic.GetColumnFromChar(startLocation.Column).GetHashCode();
            int row = startLocation.Row;

            if (row - 1 >= 0)
            {
                if (Program.board[column, row - 1].Piece == null)
                {
                    validMoves.Add(Program.board[column, row - 1]);
                }
                else if (Program.board[column, row - 1].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                {
                    validMoves.Add(Program.board[column, row - 1]);
                }
                if (column + 1 <= 7)
                {
                    if (Program.board[column + 1, row - 1].Piece == null)
                    {
                        validMoves.Add(Program.board[column + 1, row - 1]);
                    }
                    else if (Program.board[column + 1, row - 1].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                    {
                        validMoves.Add(Program.board[column + 1, row - 1]);
                    }
                }
            }
            if (row + 1 <= 7)
            {
                if (Program.board[column, row + 1].Piece == null)
                {
                    validMoves.Add(Program.board[column, row + 1]);
                }
                else if (Program.board[column, row + 1].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                {
                    validMoves.Add(Program.board[column, row + 1]);
                }
                if (column - 1 >= 0)
                {
                    if (Program.board[column - 1, row + 1].Piece == null)
                    {
                        validMoves.Add(Program.board[column - 1, row + 1]);
                    }
                    else if (Program.board[column - 1, row + 1].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                    {
                        validMoves.Add(Program.board[column - 1, row + 1]);
                    }
                }
            }
            if (column - 1 >= 0)
            {
                if (Program.board[column - 1, row].Piece == null)
                {
                    validMoves.Add(Program.board[column - 1, row]);
                }
                else if (Program.board[column - 1, row].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                {
                    validMoves.Add(Program.board[column - 1, row]);
                }
                if (row - 1 >= 0)
                {
                    if (Program.board[column - 1, row - 1].Piece == null)
                    {
                        validMoves.Add(Program.board[column - 1, row - 1]);
                    }
                    else if (Program.board[column - 1, row - 1].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                    {
                        validMoves.Add(Program.board[column - 1, row - 1]);
                    }
                }
            }
            if (column + 1 <= 7)
            {
                if (Program.board[column + 1, row].Piece == null)
                {
                    validMoves.Add(Program.board[column + 1, row]);
                }
                else if (Program.board[column + 1, row].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                {
                    validMoves.Add(Program.board[column + 1, row]);
                }
                if (row + 1 <= 7)
                {
                    if (Program.board[column + 1, row + 1].Piece == null)
                    {
                        validMoves.Add(Program.board[column + 1, row + 1]);
                    }
                    else if (Program.board[column + 1, row + 1].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                    {
                        validMoves.Add(Program.board[column + 1, row + 1]);
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
            return "K";
        }
    }
}
