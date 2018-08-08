using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    class King: ChessPiece
    {
        bool isLight;

        public bool IsLight { get; set; }

        public void Movement(Program.ChessCoordinates startLocation, Program.ChessCoordinates endLocation)
        {
            //validate movement eventually
        }

        override
        public string ToString()
        {
            return "K";
        }
    }
}
