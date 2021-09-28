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
            Player = 0;
        }
        public int Player;
        public int X;
        public int Y;
    }
}
