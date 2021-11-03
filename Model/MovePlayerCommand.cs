namespace Model {
    public class MovePlayerCommand: ICommand {

        private readonly Cell _cell;
        private IPlayer _player;
        
        public MovePlayerCommand(Cell cell, IPlayer player) {
            _cell = cell;
            _player = player;
        }
        
        public Game Execute(ref Game game) {
            game.SelectedCell = _cell;
            game.ChangeTheCell();
            return game;
        }

        public void Undo(ref Game game) {
            throw new System.NotImplementedException();
        }
    }
}