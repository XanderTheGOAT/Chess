using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ChessLibrary.Controllers
{
    public class Program
    {
        public static BoardLogic.ChessCoordinates[,] board = new BoardLogic.ChessCoordinates[8, 8];
    }
}
