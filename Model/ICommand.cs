namespace Model {
    public interface ICommand {

        public void Execute(Game game);
        public void Undo(Game game);
    }
}
