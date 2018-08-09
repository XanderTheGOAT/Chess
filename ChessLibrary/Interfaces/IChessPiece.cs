using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessLibrary.Controllers;

namespace ChessLibrary.Interfaces
{
    interface IChessPiece
    {
        bool ValidMovement(BoardLogic.ChessCoordinates startLocation, BoardLogic.ChessCoordinates endLocation);

        string ToString();
    }
}
