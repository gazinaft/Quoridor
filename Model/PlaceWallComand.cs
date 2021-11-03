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

        public void Execute(Game game) {
            game.PlaceTheWall();
        }

        public void Undo(Game game)
        {
            game.Undo(this);
        }
    }
}