namespace Model {
    public class MovePlayerCommand: ICommand {
        
        private readonly int _x;
        private readonly int _y;

        
        public MovePlayerCommand(int x, int y, bool direction) {
            _x = x;
            _y = y;
        }
        
        public void Execute(GameField field) {
            field.MovePlayer(_x, _y);
        }
    }
}