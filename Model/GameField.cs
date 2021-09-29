using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public class GameField {
        public GameField(int x, int y) {
            Cells = new Cell[x, y];
            Corners = new Corner[x, y];
        }

        public Cell[,] Cells { get; private set; }
        public Corner[,] Corners { get; private set; }

        public List<Cell> GetAvailableMoves(Cell cell) {
            throw new NotImplementedException("GetAvailableMoves is not implemented");
        }

        public List<(Corner, bool)> GetAvailableBlocks() {
            throw new NotImplementedException("GetAvailableBlocks is not implemented");

        }

        public GameField SetBlock((Corner, bool) block) {
            throw new NotImplementedException("SetBlock is not implemented");
        }

        public GameField MovePlayer(Cell cell) {
            throw new NotImplementedException("MovePlayer is not implemented");
        }

        private (bool, int) AStar(Cell cell, int player) {
            throw new NotImplementedException("A* is not implemented");
        }
    }
}
