using ChessLibrary.Controllers;
using ChessLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary.Models
{
    public abstract class ChessPiece : IChessPiece
    {

        public virtual List<BoardLogic.ChessCoordinates> ValidMoves { get; set; }

        public ChessPiece()
        {

        }

        public virtual bool IsLight { get; set; }

        public virtual bool Check(BoardLogic.ChessCoordinates startLocation, BoardLogic.ChessCoordinates endLocation)
        {
            return false;
        }

        public virtual bool ValidMovement(BoardLogic.ChessCoordinates startLocation, BoardLogic.ChessCoordinates endLocation)
        {
            return false;
        }

        override
        public string ToString()
        {
            return "";
        }
    }
}
