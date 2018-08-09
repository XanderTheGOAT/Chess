using Chess.Interfaces;
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

        public virtual bool ValidMovement(Program.ChessCoordinates startLocation, Program.ChessCoordinates endLocation)
        {
            return true;
        }

        override
        public string ToString()
        {
            return "";
        }
    }
}
