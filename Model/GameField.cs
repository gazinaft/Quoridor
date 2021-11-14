using System.Collections.Generic;
using Model.Services;
namespace Model
{
    public class GameField
    {

        private MoveValidationService _moveValidationService;
        private WallValidationService _wallValidationService;

        public GameField(MoveValidationService moveValidationService, WallValidationService wallValidationService, int x, int y)
        {
            Height = y;
            Width = x;
            
            _moveValidationService = moveValidationService;
            _wallValidationService = wallValidationService;

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

        }

        public GameField(GameField other) {
            _wallValidationService = other._wallValidationService;
            _moveValidationService = other._moveValidationService;
            Height = other.Height;
            Width = other.Width;
            
            Cells = new Cell[Width, Height];

            for (int i = 0; i < Width; i++) {
                for (int j = 0; j < Height; j++) {
                    Cells[i, j] = new Cell(i, j) {HasPlayer = other.Cells[i, j].HasPlayer};
                }
            }

            Corners = new Corner[Width + 1, Height + 1];

            for (int i = 0; i < Width + 1; i++) {
                for (int j = 0; j < Height + 1; j++) {
                    Corners[i, j] = new Corner(other.Corners[i, j]);
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

            bool[,][,] grid = new bool[Height+1, Width+1][,];

            foreach (var corner in Corners) {
                bool[,] curObstacles = corner.Obstacles;
                grid[corner.X, corner.Y] = curObstacles;
            }

            GridForObstacles = grid;
            return grid;
        }

        public Cell[,] Cells { get; private set; }
        public Corner[,] Corners { get; private set; }

        public List<Cell> GetNeighbours(Cell currentCell)
        {
            List<Cell> neighbours = new List<Cell>();
            if (currentCell.X + 1 < Cells.GetLength(0))
            {
                neighbours.Add(Cells[currentCell.X + 1, currentCell.Y]);
            }
            if (currentCell.Y + 1 < Cells.GetLength(1))
            {
                neighbours.Add(Cells[currentCell.X, currentCell.Y + 1]);
            }
            if (currentCell.X-1 >= 0)
            {
                neighbours.Add(Cells[currentCell.X - 1, currentCell.Y]);
            }
            if (currentCell.Y - 1 >= 0 )
            {
                neighbours.Add(Cells[currentCell.X, currentCell.Y - 1]);
            }

            return neighbours;
        }

        public bool CanMoveBetween(Cell first, Cell second, GameField field) {
            
            return _moveValidationService.CanMoveBetween(first, second, field);
        }

        public List<Cell> GetAvailableMoves(IPlayer player) {
            List<Cell> possibleMoves = _moveValidationService.GetPossibleMoves(this, player);
            return possibleMoves;
        }

        public List<(Corner, bool)> GetAvailableWalls(List<IPlayer> players, IPlayer enemy) {
            return _wallValidationService.GetPossibleWalls(this, players, enemy.CurrentCell);
        }

        public GameField SetBlock(int x, int y, bool isHorizontal, bool toAdd = true)
        {        
            
            if (isHorizontal)
            {
                Corners[x, y].Obstacles[0, 1] = toAdd;
                Corners[x, y].Obstacles[1, 1] = toAdd;
                Corners[x, y].Obstacles[2, 1] = toAdd;
                Corners[x + 1, y].Obstacles[0, 1] = toAdd;
                Corners[x - 1, y].Obstacles[2, 1] = toAdd;
            }
            else {
                Corners[x, y].Obstacles[1, 0] = toAdd;
                Corners[x, y].Obstacles[1, 1] = toAdd;
                Corners[x, y].Obstacles[1, 2] = toAdd;
                Corners[x, y + 1].Obstacles[1, 0] = toAdd;
                Corners[x, y - 1].Obstacles[1, 2] = toAdd;
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
