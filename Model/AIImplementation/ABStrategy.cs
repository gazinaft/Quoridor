namespace Model {
    public class ABStrategy: IPlayerStrategy {

        private AI _ai;
        
        public ABStrategy(AI ai) {
            _ai = ai;
        }
        
        public void Think(Game game) {
            _ai.GetBestMove(game.GetGameState()).Execute(game);
        }
    }
}