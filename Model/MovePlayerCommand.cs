namespace Model {
    public class MovePlayerCommand: ICommand {
        
        private readonly int _x;
        private readonly int _y;
        private IPlayer _player;
        
        public MovePlayerCommand(int x, int y, IPlayer player) {
            _x = x;
            _y = y;
            _player = player;
        }

        public void Execute(Game game) {

            game.ChangeTheCell();

        }

        public void Undo(Game game)
        {
            game.Undo(this);
        }
    }
}