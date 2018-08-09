using ChessLibrary.Controllers;
using ChessLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    abstract class ChessPiece : IChessPiece
    {
        bool isLight;
    
        public bool IsLight { get; set; }

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
