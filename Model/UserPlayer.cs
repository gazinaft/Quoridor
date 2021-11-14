namespace Model
{
    public class UserPlayer : IPlayer
    {
        public int PlayerId { get; set; }

        public Cell StartCell { get; set; }
        
        private Cell _currentCell;

        public bool PlayerIsActive { get; set; }
        public int VictoryRow { get; set; }
        public Cell CurrentCell { get { return _currentCell; } set { _currentCell = value; _currentCell.HasPlayer = true; } }
        public int WallsCounter { get; set; }
        public IPlayerStrategy PlayerStrategy { get; set; }
        public ICommand LastStep { get; set; }

        public UserPlayer()
        {
            WallsCounter = 10;
        }

        public void Decide(Game game)
        {
            PlayerStrategy?.Think(game);
        }

        private UserPlayer(UserPlayer other, GameField board) {
            WallsCounter = other.WallsCounter + 0;
            StartCell = other.StartCell;
            _currentCell = board.Cells[other._currentCell.X, other._currentCell.Y];
            _currentCell.HasPlayer = true;
            VictoryRow = other.VictoryRow;
            PlayerIsActive = other.PlayerIsActive;
        }

        public IPlayer InfoClone(GameField board) {
            return new UserPlayer(this, board);
        }

        public bool IsVictory() {
            return _currentCell.Y == VictoryRow;
        }
    }
}
