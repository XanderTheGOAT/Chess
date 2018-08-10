using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    class Rook : ChessPiece
    {
        bool isLight = true;

        public new bool IsLight { get; set; }

        public override bool ValidMovement(Program.ChessCoordinates startLocation, Program.ChessCoordinates endLocation)
        {
            List<Program.ChessCoordinates> validMoves = new List<Program.ChessCoordinates>();

            int column = Program.GetColumnFromChar(startLocation.Column).GetHashCode();
            int row = startLocation.Row - 1;

            Console.WriteLine("Row: " + row);
            Console.WriteLine("Column: " + column);

            if (row - 1 >= 0)
            {
                int goingDown = row;
                while (true)
                {
                    if (goingDown - 1 >= 0)
                    {
                        Program.ChessCoordinates maybeGood = CheckSpaces(Program.board[goingDown - 1, column], startLocation);
                        Console.WriteLine(maybeGood);
                        if (maybeGood == new Program.ChessCoordinates('0', -1, null))
                        {
                            break;
                        }
                        else
                        {
                            validMoves.Add(maybeGood);
                            goingDown = goingDown - 1;
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
                        Program.ChessCoordinates maybeGood = CheckSpaces(Program.board[goingUp + 1, column], startLocation);
                        Console.WriteLine(maybeGood);
                        if (maybeGood == new Program.ChessCoordinates('0', -1, null))
                        {
                            break;
                        }
                        else
                        {
                            validMoves.Add(maybeGood);
                            goingUp = goingUp + 1;
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
                        Program.ChessCoordinates maybeGood = CheckSpaces(Program.board[row, goingLeft - 1], startLocation);
                        if (maybeGood == new Program.ChessCoordinates('0', -1, null))
                        {
                            break;
                        }
                        else
                        {
                            validMoves.Add(maybeGood);
                            goingLeft = goingLeft - 1;
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
                        Program.ChessCoordinates maybeGood = CheckSpaces(Program.board[row, goingRight + 1], startLocation);
                        if (maybeGood == new Program.ChessCoordinates('0', -1, null))
                        {
                            break;
                        }
                        else
                        {
                            validMoves.Add(maybeGood);
                            goingRight = goingRight + 1;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            Program.ChessCoordinates lookingFor = new Program.ChessCoordinates(Program.GetCharFromNumber(Program.GetColumnFromChar(endLocation.Column).GetHashCode()), endLocation.Row - 1, null);
            Program.ChessCoordinates lookingFor2 = new Program.ChessCoordinates(Program.GetCharFromNumber(endLocation.Row - 1), Program.GetColumnFromChar(endLocation.Column).GetHashCode(), null);

            foreach (var space in validMoves)
            {
                if (space == lookingFor || space == lookingFor2)
                {
                    return true;
                }
            }
            return false;
        }

        Program.ChessCoordinates CheckSpaces(Program.ChessCoordinates checkingTheseCoordinates, Program.ChessCoordinates originalCoordinates)
        {
            int row = checkingTheseCoordinates.Row;
            int column = Program.GetColumnFromChar(checkingTheseCoordinates.Column).GetHashCode();

            if (Program.board[column, row].Piece == null)
            {
                return Program.board[row, column];
            }
            else if (Program.board[column, row].Piece.IsLight != Program.board[originalCoordinates.Row - 1, Program.GetColumnFromChar(originalCoordinates.Column).GetHashCode()].Piece.IsLight)
            {
                return Program.board[row, column];
            }
            else
            {
                return new Program.ChessCoordinates('0', -1, null);
            }
        }

        override
        public string ToString()
        {
            return "R";
        }
    }
}
