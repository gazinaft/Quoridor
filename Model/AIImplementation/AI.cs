namespace Model {
    public interface AI {
        public ICommand GetBestMove(Game game);
    }
}