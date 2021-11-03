namespace Model {
    public interface ICommand {
        public Game Execute(ref Game game);

        public void Undo(ref Game game);
    }
}
