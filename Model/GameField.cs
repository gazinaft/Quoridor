using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    class GameField {
        public GameField(int x, int y) {
            Cells = new Cell[x, y];
            Corners = new Corner[x, y];
        }

        public Cell[,] Cells { get; private set; }
        public Corner[,] Corners { get; private set; }

        public List<Cell> GetAvailableMoves(Cell cell) { }
        public List<(Corner, bool)> GetAvailableBlocks() { }

        public GameField SetBlock((Corner, bool) block) { }
        public GameField MovePlayer(Cell cell) { }

        private (bool, int) AStar(Cell cell, int player) { }
    }
}
