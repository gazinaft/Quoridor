namespace Model {
    public class EmptyCommand: ICommand {
        public Game Execute(Game game) {
            return game;
        }

        public void Undo(Game game) {}
    }
}