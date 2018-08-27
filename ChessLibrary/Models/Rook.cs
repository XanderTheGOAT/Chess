using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLibrary.Controllers;

namespace ChessLibrary.Models
{
    public class Rook : ChessPiece
    {
        bool shouldStop;
        public new List<BoardLogic.ChessCoordinates> ValidMoves = new List<BoardLogic.ChessCoordinates>();
        public Rook()
        {

        }

        public Rook(bool isLight)
        {
            IsLight = isLight;
        }

        override
        public bool IsLight { get; set; }

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

            int column = FileLogic.GetColumnFromChar(startLocation.Column).GetHashCode();
            int row = startLocation.Row;

            if (row - 1 >= 0)
            {
                int goingDown = row;
                while (true)
                {
                    if (goingDown - 1 >= 0)
                    {
                        BoardLogic.ChessCoordinates maybeGood = CheckSpaces(Program.board[column, goingDown - 1], startLocation);
                        if (maybeGood == new BoardLogic.ChessCoordinates('0', -1, null))
                        {
                            break;
                        }
                        else
                        {
                            ValidMoves.Add(maybeGood);
                            goingDown = goingDown - 1;
                            if (shouldStop)
                            {
                                shouldStop = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (row + 1 <= 7)
            {
                int goingUp = row;
                while (true)
                {
                    if (goingUp + 1 <= 7)
                    {
                        BoardLogic.ChessCoordinates maybeGood = CheckSpaces(Program.board[column, goingUp + 1], startLocation);
                        if (maybeGood == new BoardLogic.ChessCoordinates('0', -1, null))
                        {
                            break;
                        }
                        else
                        {
                            ValidMoves.Add(maybeGood);
                            goingUp = goingUp + 1;
                            if (shouldStop)
                            {
                                shouldStop = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (column - 1 >= 0)
            {
                int goingLeft = column;
                while (true)
                {
                    if (goingLeft - 1 >= 0)
                    {
                        BoardLogic.ChessCoordinates maybeGood = CheckSpaces(Program.board[goingLeft - 1, row], startLocation);
                        Console.WriteLine(maybeGood);
                        if (maybeGood == new BoardLogic.ChessCoordinates('0', -1, null))
                        {
                            break;
                        }
                        else
                        {
                            ValidMoves.Add(maybeGood);
                            goingLeft = goingLeft - 1;
                            if (shouldStop)
                            {
                                shouldStop = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (column + 1 <= 7)
            {
                int goingRight = column;
                while (true)
                {
                    if (goingRight + 1 <= 7)
                    {
                        BoardLogic.ChessCoordinates maybeGood = CheckSpaces(Program.board[goingRight + 1, row], startLocation);
                        if (maybeGood == new BoardLogic.ChessCoordinates('0', -1, null))
                        {
                            break;
                        }
                        else
                        {
                            ValidMoves.Add(maybeGood);
                            goingRight = goingRight + 1;
                            if (shouldStop)
                            {
                                shouldStop = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
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
                shouldStop = true;
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
            return "R";
        }
    }
}
