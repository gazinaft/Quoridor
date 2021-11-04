namespace Model {
    public class ABTree {
        
        private Node _root;
        private Game _game;
        
        public ABTree(Game game, int depth) {
            _game = game;
            _root = new Node(depth);
        }

        public ICommand getBestTurn() {
            return new PlaceWallCommand(1, 2, false);
        }

        private void alphaBeta() {
            int alpha = -1000;
            int beta = 1000;
            
            
            
        }

    }
}