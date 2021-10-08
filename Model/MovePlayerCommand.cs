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
        
        public void Execute(GameField field) {
            field.MovePlayer(_x, _y, _player);
        }
    }
}