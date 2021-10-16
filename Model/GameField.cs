using System;
using System.Collections.Generic;
using Model.Services;
namespace Model
{
    public class GameField
    {

        private MoveValidationService _moveValidationService;

        private WallValidationService _wallValidationService;

        private PathFindingService _pathFindingService;

        public GameField(MoveValidationService moveValidationService, WallValidationService wallValidationService, PathFindingService pathFindingService, int x, int y)
        {
            Height = y;

            Width = x;
            
            _moveValidationService = moveValidationService;

            _wallValidationService = wallValidationService;

            _pathFindingService = pathFindingService;

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

        }

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

        public bool CanMoveBetween(Cell first, Cell second, GameField field) {
            Cell max = MaxCell(first, second);
            return field.Corners[max.X, max.Y]
                .Obstacles[IsXAxis(first, second) ? 1 : 2, IsXAxis(first, second) ? 0 : 1];
        }

        private Cell MaxCell(Cell first, Cell second)
        {
            if (first.X > second.X || first.Y > second.Y) return first;
            return second;
        }

        private bool IsXAxis(Cell first, Cell second)
        {
            return first.Y == second.Y;
        }

        public List<Cell> GetAvailableMoves(IPlayer player) {

            List<Cell> possibleMoves = _moveValidationService.GetPossibleMoves(this, player);

            return possibleMoves;

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

    }
}
