﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Interfaces
{
    interface IChessPiece
    {
        bool ValidMovement(Program.ChessCoordinates startLocation, Program.ChessCoordinates endLocation);

        string ToString();
    }
}
