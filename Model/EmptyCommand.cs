namespace Model {
    public class EmptyCommand: ICommand {
        public Game Execute(Game game) {
            return game;
        }

        public GameStateModel Execute(GameStateModel game) {
            return game;
        }

        public void Undo(Game game) {}
        
        public void Undo(GameStateModel gsm) {}
    }
}