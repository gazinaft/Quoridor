namespace Model {
    public interface ICommand {
        public Game Execute(Game game);

        public void Undo(Game game);
    }
}
