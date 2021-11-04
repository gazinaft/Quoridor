using System.Collections.Generic;

namespace Model {

    public delegate float SEV(Game game);
    public class ABTree {
        
        private Node _root;
        private Game _game;
        private SEV _sev;

        public ABTree(SEV sev, Game game, int depth) {
            _sev = sev;
            _game = game;
            _root = new Node(new EmptyCommand(), depth);
        }

        public ICommand getBestTurn() {
            return new EmptyCommand();
        }

        private void alphaBeta() {
            int alpha = -1000;
            int beta = 1000;
            
        }
        
    }
}