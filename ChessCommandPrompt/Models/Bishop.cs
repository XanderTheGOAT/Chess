using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    class Bishop: ChessPiece
    {
        bool isLight = true;

        public bool IsLight { get; set; }

        public new bool ValidMovement()
        {
            //validate movement eventually
            return true;
        }

        override
        public string ToString()
        {
            return "B";
        }
    }
}
