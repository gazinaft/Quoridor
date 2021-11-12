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

        public void Undo(Game game) {
            game.SelectedCell = _currentCell;
            game.ChangeTheCell();
        }

    }
}