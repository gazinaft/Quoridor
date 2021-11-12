namespace Model {
    public interface AI {
        public ICommand GetBestMove(GameStateModel gsm);
    }
}