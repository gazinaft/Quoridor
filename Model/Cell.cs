using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Cell {

        public Cell(int x, int y) {
            X = x;
            Y = y;
            //Player = 0; 
        }



        public int X { get; }
        public int Y { get; }
        public bool HasPlayer { get; private set; } // мені здається, що клітина не повинна містити гравця,
                                                    // лише має містити флаг, чи є клітина зайнятою

    }
}
