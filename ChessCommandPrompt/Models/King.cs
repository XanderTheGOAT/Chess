using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    class King: ChessPiece
    {
        public void Movement()
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
