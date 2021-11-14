namespace Model {
    public class Node {

        public readonly int Level;
        public readonly GameStateModel Gsm;
        public readonly bool Max;
        public Node BestNode = null;
        
        public Node(GameStateModel gsm, int level, bool max = true) {
            Gsm = gsm;
            Level = level;
            Max = max;
        }

    }
}