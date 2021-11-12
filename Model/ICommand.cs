namespace Model {
    public interface ICommand {
        public Game Execute(Game game);

        public GameStateModel Execute (GameStateModel game);

        public void Undo(GameStateModel gsm);

        public void Undo(Game game);

    }
}
