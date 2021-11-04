namespace Model {
    public class MovePlayerCommand: ICommand {

        private readonly Cell _selectedCell;
        private readonly Cell _currentCell;
        private IPlayer _player;
        
        public MovePlayerCommand(Cell cell, IPlayer player) {
            _selectedCell = cell;
            _currentCell = player.CurrentCell;
            _player = player;
        }
        
        public Game Execute(Game game) {
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