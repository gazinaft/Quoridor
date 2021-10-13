using System;
using System.Collections.Generic;

namespace Model
{
    public class GameField
    {

        public int Height { get; }

        public int Width { get; }

        public bool[,] GridForPlayers { get; set; }

        public bool[,][,] GridForObstacles { get; set; }

        public bool[,] FormGridForPlayers() {

            bool[,] Grid = new bool[Height, Width];
            
            foreach (Cell cell in this.Cells) {

                if (cell.HasPlayer) {

                    Grid[cell.X, cell.Y] = true;
                
                }

            }

            GridForPlayers = Grid;

            return Grid;
        
        }

        public bool[,][,] FormGridForObstacles() {

            bool[,][,] Grid = new bool[Height+1, Width+1][,];

            foreach (var corner in Corners) {
                
                bool[,] curObstacles = corner.Obstacles;

                Grid[corner.X, corner.Y] = curObstacles;

            }

            GridForObstacles = Grid;

            return Grid;
        
        }



        public GameField(int x, int y)
        {

            Cells = new Cell[x, y];

            for (int i = 0; i < x; i++)
            {

                for (int j = 0; j < y; j++)
                {

                    Cells[i, j] = new Cell(i, j);

                }

            }

            Corners = new Corner[x + 1, y + 1];

            for (int i = 0; i < x + 1; i++)
            {

                for (int j = 0; j < y + 1; j++)
                {

                    Corners[i, j] = new Corner(i, j);

                }

            }

            Height = y;

            Width = x;

        }

        public Cell[,] Cells { get; private set; }

        public Corner[,] Corners { get; private set; }

        public List<Cell> GetNeighbours(Cell currentCell)
        {

            List<Cell> neighbours = new List<Cell>();

            if (currentCell.X + 1 < Cells.GetLength(0))
            {

                neighbours.Add(this.Cells[currentCell.X + 1, currentCell.Y]);

            }
            if (currentCell.Y + 1 < Cells.GetLength(1))
            {

                neighbours.Add(this.Cells[currentCell.X, currentCell.Y + 1]);

            }
            if (currentCell.X-1 >= 0)
            {

                neighbours.Add(this.Cells[currentCell.X - 1, currentCell.Y]);

            }
            if (currentCell.Y - 1 >= 0 )
            {

                neighbours.Add(this.Cells[currentCell.X, currentCell.Y - 1]);

            }

            return neighbours;

        }

        public bool CanMoveBetween(Cell firstCell, Cell secondCell) {
            throw new NotImplementedException("CanMoveBetween is not ready");
        }

        public List<Cell> GetAvailableMoves(IPlayer player) {

            List<Cell> availableMoves = new List<Cell>();

            foreach (Cell cell in GetNeighbours(player.CurrentCell)) {

                if (cell.HasPlayer)
                {

                    if (cell.X - player.CurrentCell.X < 0)
                    {

                        if (cell.X - 1 > 0)
                        {

                            availableMoves.Add(Cells[player.CurrentCell.X - 2, player.CurrentCell.Y]);

                        }

                    }
                    else if (cell.X - player.CurrentCell.X > 0)
                    {

                        if (cell.X + 1 < Cells.GetLength(0))
                        {

                            availableMoves.Add(Cells[player.CurrentCell.X + 2, player.CurrentCell.Y]);

                        }

                    }
                    else if (cell.Y - 1 > 0)
                    {

                        availableMoves.Add(Cells[player.CurrentCell.X, player.CurrentCell.Y - 2]);

                    }
                    else if (cell.Y + 1 < Cells.GetLength(0))
                    {

                        availableMoves.Add(Cells[player.CurrentCell.X, player.CurrentCell.Y + 2]);

                    }

                }
                else {

                    availableMoves.Add(cell);

                }
            
            }

            return GetNeighbours(player.CurrentCell);

        }

        public List<(Corner, bool)> GetAvailableBlocks()
        {

            throw new NotImplementedException("GetAvailableBlocks is not implemented");

        }

        public GameField SetBlock(int x, int y, bool isHorizintal)
        {        
            
            if (isHorizintal)
            {

                this.Corners[x, y].Obstacles[0, 1] = true;
                this.Corners[x, y].Obstacles[1, 1] = true;
                this.Corners[x, y].Obstacles[2, 1] = true;
                this.Corners[x + 1, y].Obstacles[0, 1] = true;
                this.Corners[x - 1, y].Obstacles[2, 1] = true;

            }
            else {

                this.Corners[x, y].Obstacles[1, 0] = true;
                this.Corners[x, y].Obstacles[1, 1] = true;
                this.Corners[x, y].Obstacles[1, 2] = true;
                this.Corners[x, y + 1].Obstacles[1, 2] = true;
                this.Corners[x, y - 1].Obstacles[1, 0] = true;

            }

            return this;

        }

        public void MovePlayer(int x, int y, IPlayer player)
        {

            Cell selectedCell = Cells[x, y];

            if (GetAvailableMoves(player).Contains(selectedCell)) {

                player.CurrentCell.HasPlayer = false;                
                Cells[x, y].HasPlayer = true;
                player.CurrentCell = Cells[x, y];

            }

            
        }


        private (bool, int) AStar(IPlayer player)
        {

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
}
