using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public class Corner {

        public Corner(int x, int y) {
            X = x;
            Y = y;
            Obstacles = new int[3,3];
        }

        public int X { get; }
        public int Y { get; }
        public int[,] Obstacles;
    }
}
