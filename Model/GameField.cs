using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public class GameField {
        
        public int Height { get; }

        public int Width { get; }
        

        
        public GameField(int x, int y) {
            
            Cells = new Cell[x, y];

            for (int i = 0; i < x; i++) {

                for (int j = 0; j < y; j++) {

                    Cells[i, j] = new Cell(i, j);
                
                }
            
            }
            
            Corners = new Corner[x + 1, y + 1];

            for (int i = 0; i < x + 1; i++) {

                for (int j = 0; j < y + 1; j++) {

                    Corners[i, j] = new Corner(i, j);
                
                }
            
            }

            Height = y;

            Width = x;
        
        }

        public Cell[,] Cells { get; private set; }
        
        public Corner[,] Corners { get; private set; }

        public List<Cell> GetNeighbours(Cell currentCell) {

            List<Cell> neighbours = new List<Cell>();

            if (this.Cells[currentCell.X + 1, currentCell.Y]!=null) {

                neighbours.Add(this.Cells[currentCell.X + 1, currentCell.Y]);
            
            }
            if (this.Cells[currentCell.X, currentCell.Y + 1] != null)
            {

                neighbours.Add(this.Cells[currentCell.X, currentCell.Y + 1]);

            }
            if (this.Cells[currentCell.X - 1, currentCell.Y] != null)
            {

                neighbours.Add(this.Cells[currentCell.X - 1, currentCell.Y]);

            }
            if (this.Cells[currentCell.X, currentCell.Y - 1] != null)
            {

                neighbours.Add(this.Cells[currentCell.X, currentCell.Y - 1]);

            }

            return neighbours;

        }

        public bool CanMoveBetween(Cell firstCell, Cell secondCell) {

            if (firstCell.X == secondCell.X)
            {

                if (firstCell.Y > secondCell.Y)
                {

                    return !(this.Corners[firstCell.X, secondCell.Y].Obstacles[2, 1] == 1);

                }
                else
                {

                    return !(this.Corners[secondCell.X, firstCell.Y].Obstacles[2, 1] == 1);

                }

            }
            else
            {

                if (firstCell.X > secondCell.Y)
                {


                    return !(this.Corners[firstCell.X, secondCell.Y].Obstacles[1, 0] == 1);


                }
                else
                {


                    return !(this.Corners[secondCell.X, firstCell.Y].Obstacles[1, 0] == 1);


                }

            }

        }

        public List<Cell> GetAvailableMoves(IPlayer player) {

            List<Cell> availableMoves = new List<Cell>();

            if (Cells[player.CurrentCell.X, player.CurrentCell.Y + 1].HasPlayer)
            {


                if (!Cells[player.CurrentCell.X - 1, player.CurrentCell.Y].HasPlayer && !(Cells[player.CurrentCell.X - 1, player.CurrentCell.Y] == null))
                {

                    availableMoves.Add(Cells[player.CurrentCell.X - 1, player.CurrentCell.Y]);

                }

                if (!Cells[player.CurrentCell.X + 1, player.CurrentCell.Y].HasPlayer && !(Cells[player.CurrentCell.X + 1, player.CurrentCell.Y] == null))
                {

                    availableMoves.Add(Cells[player.CurrentCell.X + 1, player.CurrentCell.Y]);

                }

                if (!Cells[player.CurrentCell.X, player.CurrentCell.Y - 1].HasPlayer && !(Cells[player.CurrentCell.X, player.CurrentCell.Y - 1] == null))
                {

                    availableMoves.Add(Cells[player.CurrentCell.X, player.CurrentCell.Y - 1]);

                }


                if (!Cells[player.CurrentCell.X, player.CurrentCell.Y + 2].HasPlayer && !(Cells[player.CurrentCell.X - 1, player.CurrentCell.Y + 2] == null))
                {

                    availableMoves.Add(Cells[player.CurrentCell.X, player.CurrentCell.Y + 2]);

                }

            }
            else if (Cells[player.CurrentCell.X - 1, player.CurrentCell.Y].HasPlayer)
            {

                if (!Cells[player.CurrentCell.X, player.CurrentCell.Y + 1].HasPlayer && !(Cells[player.CurrentCell.X, player.CurrentCell.Y + 1] == null))
                {

                    availableMoves.Add(Cells[player.CurrentCell.X, player.CurrentCell.Y + 1]);

                }

                if (!Cells[player.CurrentCell.X, player.CurrentCell.Y - 1].HasPlayer && !(Cells[player.CurrentCell.X, player.CurrentCell.Y - 1] == null))
                {

                    availableMoves.Add(Cells[player.CurrentCell.X, player.CurrentCell.Y - 1]);

                }


                if (!Cells[player.CurrentCell.X + 1, player.CurrentCell.Y].HasPlayer && !(Cells[player.CurrentCell.X + 1, player.CurrentCell.Y] == null))
                {

                    availableMoves.Add(Cells[player.CurrentCell.X + 1, player.CurrentCell.Y]);

                }

                if (!Cells[player.CurrentCell.X - 2, player.CurrentCell.Y].HasPlayer && !(Cells[player.CurrentCell.X - 2, player.CurrentCell.Y] == null))
                {

                    availableMoves.Add(Cells[player.CurrentCell.X - 2, player.CurrentCell.Y]);

                }


            }

            else if (Cells[player.CurrentCell.X, player.CurrentCell.Y - 1].HasPlayer)
            {

                if (!Cells[player.CurrentCell.X, player.CurrentCell.Y - 2].HasPlayer && !(Cells[player.CurrentCell.X, player.CurrentCell.Y - 2] == null))
                {

                    availableMoves.Add(Cells[player.CurrentCell.X, player.CurrentCell.Y - 2]);

                }

                if (!Cells[player.CurrentCell.X + 1, player.CurrentCell.Y].HasPlayer && !(Cells[player.CurrentCell.X + 1, player.CurrentCell.Y] == null))
                {

                    availableMoves.Add(Cells[player.CurrentCell.X + 1, player.CurrentCell.Y]);

                }


                if (!Cells[player.CurrentCell.X - 1, player.CurrentCell.Y].HasPlayer && !(Cells[player.CurrentCell.X - 1, player.CurrentCell.Y] == null))
                {

                    availableMoves.Add(Cells[player.CurrentCell.X - 1, player.CurrentCell.Y]);

                }

                if (!Cells[player.CurrentCell.X, player.CurrentCell.Y + 1].HasPlayer && !(Cells[player.CurrentCell.X, player.CurrentCell.Y + 1] == null))
                {

                    availableMoves.Add(Cells[player.CurrentCell.X, player.CurrentCell.Y + 1]);

                }


            }

            else if (Cells[player.CurrentCell.X + 1, player.CurrentCell.Y].HasPlayer)
            {

                if (!Cells[player.CurrentCell.X, player.CurrentCell.Y + 1].HasPlayer && !(Cells[player.CurrentCell.X, player.CurrentCell.Y + 1] == null))
                {

                    availableMoves.Add(Cells[player.CurrentCell.X, player.CurrentCell.Y + 1]);

                }

                if (!Cells[player.CurrentCell.X, player.CurrentCell.Y - 1].HasPlayer && !(Cells[player.CurrentCell.X, player.CurrentCell.Y - 1] == null))
                {

                    availableMoves.Add(Cells[player.CurrentCell.X, player.CurrentCell.Y - 1]);

                }


                if (!Cells[player.CurrentCell.X - 1, player.CurrentCell.Y].HasPlayer && !(Cells[player.CurrentCell.X - 1, player.CurrentCell.Y] == null))
                {

                    availableMoves.Add(Cells[player.CurrentCell.X - 1, player.CurrentCell.Y]);

                }

                if (!Cells[player.CurrentCell.X + 2, player.CurrentCell.Y].HasPlayer && !(Cells[player.CurrentCell.X + 2, player.CurrentCell.Y] == null))
                {

                    availableMoves.Add(Cells[player.CurrentCell.X + 2, player.CurrentCell.Y]);

                }

            }
            else {

                if (!(Cells[player.CurrentCell.X, player.CurrentCell.Y + 1] == null)) {

                    availableMoves.Add(Cells[player.CurrentCell.X, player.CurrentCell.Y + 1]);

                }
                if (!(Cells[player.CurrentCell.X, player.CurrentCell.Y - 1] == null)) {

                    availableMoves.Add(Cells[player.CurrentCell.X, player.CurrentCell.Y - 1]);

                }
                if (!(Cells[player.CurrentCell.X+1, player.CurrentCell.Y] == null)) {

                    availableMoves.Add(Cells[player.CurrentCell.X+1, player.CurrentCell.Y]);
                
                }
                if (!(Cells[player.CurrentCell.X - 1, player.CurrentCell.Y] == null)) {

                    availableMoves.Add(Cells[player.CurrentCell.X - 1, player.CurrentCell.Y]);

                }
            
            }

            return availableMoves;
        
        }

        public List<(Corner, bool)> GetAvailableBlocks() {
            
            throw new NotImplementedException("GetAvailableBlocks is not implemented");

        }

        public GameField SetBlock(int x, int y, bool dir) {
            
            // Corner center = Corners[x, y]; 
            // Corners[dir ? x : x + 1, dir ? y + 1 : y].Obstacles[dir ? 0 : 1, 1] = 1;
            throw new NotImplementedException("SetBlock is not implemented");
        
        }

        public void MovePlayer(int x, int y, IPlayer player) {

            player.CurrentCell.HasPlayer = false;
            Cells[x, y].HasPlayer = true;
            player.CurrentCell = Cells[x, y];
        }


        private (bool, int) AStar(IPlayer player) {

           /* int finalRow = 0;

            List<Cell> allFinalCells = new List<Cell>();

            if (player.PlayerId == 1)
            {

                finalRow = 0;

            }
            else if (player.PlayerId == 2) {

                finalRow = Heigth;
            
            }

            foreach (Cell cell in Cells) {

                if (cell.Y == finalRow) {

                    allFinalCells.Add(cell);
                
                }
            
            }

            List<Cell> currentNeighbours = new List<Cell>();

            double theLeastPath;

            Cell nextCell;
            
            foreach (Cell cell1 in GetAvailableMoves(player.CurrentCell)) {

                Cell firstCell = cell1;

                double pathLength = 0;

                List<double> allPathes = new List<double>();

                foreach (Cell cell2 in allFinalCells) {

                    Cell finalCell = cell2;

                    pathLength = Math.Sqrt( (Math.Abs(Math.Pow(cell1.X, 2) - Math.Pow(cell2.X, 2))) - (Math.Abs(Math.Pow(cell1.Y, 2) - Math.Pow(cell2.Y, 2))));

                    allPathes.Add(pathLength);

                }
            
            }
            
            List<Cell> path = new List<Cell>();

            */

            
            throw new NotImplementedException("A* is not implemented");
        
    }
}
