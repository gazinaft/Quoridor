namespace Model {
    public class Node {

        public Node Parent;
        public int Level;
        public ICommand Command;
        
        public Node(ICommand command, int level, Node parent = null) {
            Command = command;
            Parent = parent;
            Level = level;
        }

        
        
    }
}