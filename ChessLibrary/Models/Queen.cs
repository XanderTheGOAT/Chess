﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary.Models
{
    public class Queen : ChessPiece
    {
        bool isLight;

        public bool IsLight { get; set; }

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
