namespace Model {
    public class PlaceWallCommand: ICommand {

        private readonly int _x;
        private readonly int _y;
        private readonly bool _direction;

        
        public PlaceWallCommand(int x, int y, bool direction) {
            _x = x;
            _y = y;
            _direction = direction;
        }
        
        public void Execute(GameField field) {
            field.SetBlock(_x, _y, _direction);
        }
    }
}