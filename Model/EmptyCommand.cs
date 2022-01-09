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

        public (int, int, bool) InfoForSerialize() {
            return (0, 0, false);
        }
    }
}