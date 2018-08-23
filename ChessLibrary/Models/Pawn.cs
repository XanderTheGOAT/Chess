using ChessLibrary.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary.Models
{
    public class Pawn : ChessPiece
    {
        bool isLight;
        public new List<BoardLogic.ChessCoordinates> ValidMoves = new List<BoardLogic.ChessCoordinates>();

        public Pawn()
        {

        }
        public new bool IsLight { get; set; }

        public override bool Check(BoardLogic.ChessCoordinates startLocation, BoardLogic.ChessCoordinates endLocation)
        {
            ValidMovement(startLocation, endLocation);
            foreach (var space in ValidMoves)
            {
                if (space.Piece.ToString() == "K" && space.Piece.IsLight != startLocation.Piece.IsLight)
                {
                    return true;
                }
            }
            return false;
        }

        public override bool ValidMovement(BoardLogic.ChessCoordinates startLocation, BoardLogic.ChessCoordinates endLocation)
        {
            //validate movement eventually
            ValidMoves.Clear();

            int column = FileLogic.GetColumnFromChar(startLocation.Column).GetHashCode();
            int row = startLocation.Row;

            if (Program.board[column, row].Piece.IsLight)
            {
                if (column - 1 >= 0)
                {
                    if (Program.board[column - 1, row].Piece == null)
                    {
                        ValidMoves.Add(Program.board[column - 1, row]);
                        if (column == 6)
                        {
                            if (Program.board[column - 2, row].Piece == null)
                            {
                                ValidMoves.Add(Program.board[column - 2, row]);
                            }
                        }
                    }
                    if (row - 1 >= 0 && Program.board[column - 1, row - 1].Piece != null && Program.board[column - 1, row - 1].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                    {
                        ValidMoves.Add(Program.board[column - 1, row - 1]);
                    }
                    if (row + 1 <= 7 && Program.board[column - 1, row + 1].Piece != null && Program.board[column - 1, row + 1].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                    {
                        ValidMoves.Add(Program.board[column - 1, row + 1]);
                    }
                }
            }
            else if (!Program.board[column, row].Piece.IsLight)
            {
                if (column + 1 <= 7)
                {
                    if (Program.board[column + 1, row].Piece == null)
                    {
                        ValidMoves.Add(Program.board[column + 1, row]);
                        if (column == 1)
                        {
                            if (Program.board[column + 2, row].Piece == null)
                            {
                                ValidMoves.Add(Program.board[column + 2, row]);
                            }
                        }
                    }
                    if (row - 1 <= 7 && Program.board[column + 1, row - 1].Piece != null && Program.board[column + 1, row - 1].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                    {
                        ValidMoves.Add(Program.board[column + 1, row - 1]);
                    }
                    if (row + 1 <= 7 && Program.board[column + 1, row + 1].Piece != null && Program.board[column + 1, row + 1].Piece.IsLight != Program.board[column, row].Piece.IsLight)
                    {
                        ValidMoves.Add(Program.board[column + 1, row + 1]);
                    }
                }
            }
            BoardLogic.ChessCoordinates lookingFor = new BoardLogic.ChessCoordinates(BoardLogic.GetCharFromNumber(FileLogic.GetColumnFromChar(endLocation.Column).GetHashCode()), endLocation.Row, null);
            //BoardLogic.ChessCoordinates lookingFor2 = new BoardLogic.ChessCoordinates(BoardLogic.GetCharFromNumber(endLocation.Row), FileLogic.GetColumnFromChar(endLocation.Column).GetHashCode(), null);

            foreach (var space in ValidMoves)
            {
                if (space == lookingFor)
                {
                    return true;
                }
            }
            return false;
        }

        BoardLogic.ChessCoordinates CheckSpaces(BoardLogic.ChessCoordinates checkingTheseCoordinates, BoardLogic.ChessCoordinates originalCoordinates)
        {
            int row = checkingTheseCoordinates.Row;
            int column = FileLogic.GetColumnFromChar(checkingTheseCoordinates.Column).GetHashCode();

            if (Program.board[column, row].Piece == null)
            {
                return Program.board[column, row];
            }
            else if (Program.board[column, row].Piece.IsLight != Program.board[FileLogic.GetColumnFromChar(originalCoordinates.Column).GetHashCode(), originalCoordinates.Row].Piece.IsLight)
            {
                return Program.board[column, row];
            }
            else
            {
                return new BoardLogic.ChessCoordinates('0', -1, null);
            }
        }

        override
        public string ToString()
        {
            return "P";
        }
    }
}
