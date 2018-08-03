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
        public void Movement()
        {

        }

        override
        public string ToString()
        {
            return "";
        }
    }
}
