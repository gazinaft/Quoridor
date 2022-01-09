namespace Model {
    public class MovePlayerCommand: ICommand {

        private readonly Cell _selectedCell;
        private Cell _currentCell;

        public MovePlayerCommand(Cell cell) {
            _selectedCell = cell;
        }
        
        public Game Execute(Game game) {
            _currentCell = game.ActivePlayer.CurrentCell;
            game.SelectedCell = _selectedCell;
            game.ChangeTheCell();
            return game;
        }

        public GameStateModel Execute(GameStateModel game) {
            _currentCell = game.ActivePlayer.CurrentCell;
            game.MakeMove(_selectedCell.X, _selectedCell.Y);
            return game;
        }

        public void Undo(Game game) {
            game.SelectedCell = _currentCell;
            game.ChangeTheCell();
        }

        public void Undo(GameStateModel gsm) {
            gsm.MakeMove(_currentCell.X, _selectedCell.Y);
        }

        public (int, int, bool) InfoForSerialize() {
            return (_selectedCell.X, _selectedCell.Y, false);
        }
    }
}