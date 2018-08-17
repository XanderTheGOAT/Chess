using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    class Knight: ChessPiece
    {
        bool isLight = true;

        public new bool IsLight { get; set; }

        public bool ValidMovement()
        {
            //validate movement eventually
            return true;
        }

        override
        public string ToString()
        {
            return "N";
        }
    }
}
