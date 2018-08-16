using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary.Models
{
    public class Queen : ChessPiece
    {
        bool isLight;
        public Queen()
        {

<<<<<<< HEAD
        public bool IsLight { get; set; }
=======
<<<<<<< HEAD
        public new bool IsLight { get; set; }
=======
        }
        public bool IsLight { get; set; }
>>>>>>> parent of c702ba9... Made more gui work :)
>>>>>>> 938db760a7d4a534b126bed48e4b50f420e19649

        public void Movement()
        {
            //validate movement eventually
        }

        override
        public string ToString()
        {
            return "Q";
        }
    }
}
