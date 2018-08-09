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

        public void Movement(Program.ChessCoordinates startLocation, Program.ChessCoordinates endLocation)
        {

        }

        override
        public string ToString()
        {
            return "";
        }
    }
}
