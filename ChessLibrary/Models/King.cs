using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLibrary.Controllers;

namespace ChessLibrary.Models
{
    public class King : ChessPiece
    {
        List<BoardLogic.ChessCoordinates> validMoves = new List<BoardLogic.ChessCoordinates>();

        public override List<BoardLogic.ChessCoordinates> ValidMoves { set { } get { return validMoves; } }

        override
        public bool IsLight { get; set; }

        public King()
        {

        }

        public King(bool isLight)
        {
            IsLight = isLight;
        }

        public override bool Check(BoardLogic.ChessCoordinates startLocation, BoardLogic.ChessCoordinates endLocation)
        {
            ValidMovement(startLocation, endLocation);
            foreach (var space in ValidMoves)
            {
                if (space.Piece != null && space.Piece.ToString() == "K" && space.Piece.IsLight != startLocation.Piece.IsLight)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool ValidMovement(BoardLogic.ChessCoordinates startLocation, BoardLogic.ChessCoordinates endLocation)
        {
            ValidMoves.Clear();

            //validate movement eventually
            int column = FileLogic.GetColumnFromChar(startLocation.Column).GetHashCode();
            int row = startLocation.Row;

            if (row - 1 >= 0)
            {
                if (Program.board[column, row - 1].Piece == null)
                {
                    ValidMoves.Add(Program.board[column, row - 1]);
                }
                else if (Program.board[column, row - 1].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                {
                    ValidMoves.Add(Program.board[column, row - 1]);
                }
                if (column + 1 <= 7)
                {
                    if (Program.board[column + 1, row - 1].Piece == null)
                    {
                        ValidMoves.Add(Program.board[column + 1, row - 1]);
                    }
                    else if (Program.board[column + 1, row - 1].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                    {
                        ValidMoves.Add(Program.board[column + 1, row - 1]);
                    }
                }
            }
            if (row + 1 <= 7)
            {
                if (Program.board[column, row + 1].Piece == null)
                {
                    ValidMoves.Add(Program.board[column, row + 1]);
                }
                else if (Program.board[column, row + 1].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                {
                    ValidMoves.Add(Program.board[column, row + 1]);
                }
                if (column - 1 >= 0)
                {
                    if (Program.board[column - 1, row + 1].Piece == null)
                    {
                        ValidMoves.Add(Program.board[column - 1, row + 1]);
                    }
                    else if (Program.board[column - 1, row + 1].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                    {
                        ValidMoves.Add(Program.board[column - 1, row + 1]);
                    }
                }
            }
            if (column - 1 >= 0)
            {
                if (Program.board[column - 1, row].Piece == null)
                {
                    ValidMoves.Add(Program.board[column - 1, row]);
                }
                else if (Program.board[column - 1, row].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                {
                    ValidMoves.Add(Program.board[column - 1, row]);
                }
                if (row - 1 >= 0)
                {
                    if (Program.board[column - 1, row - 1].Piece == null)
                    {
                        ValidMoves.Add(Program.board[column - 1, row - 1]);
                    }
                    else if (Program.board[column - 1, row - 1].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                    {
                        ValidMoves.Add(Program.board[column - 1, row - 1]);
                    }
                }
            }
            if (column + 1 <= 7)
            {
                if (Program.board[column + 1, row].Piece == null)
                {
                    ValidMoves.Add(Program.board[column + 1, row]);
                }
                else if (Program.board[column + 1, row].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                {
                    ValidMoves.Add(Program.board[column + 1, row]);
                }
                if (row + 1 <= 7)
                {
                    if (Program.board[column + 1, row + 1].Piece == null)
                    {
                        ValidMoves.Add(Program.board[column + 1, row + 1]);
                    }
                    else if (Program.board[column + 1, row + 1].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                    {
                        ValidMoves.Add(Program.board[column + 1, row + 1]);
                    }
                }
            }

            BoardLogic.ChessCoordinates lookingFor = new BoardLogic.ChessCoordinates(BoardLogic.GetCharFromNumber(FileLogic.GetColumnFromChar(endLocation.Column).GetHashCode()), endLocation.Row, null);
            foreach (var space in ValidMoves)
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
