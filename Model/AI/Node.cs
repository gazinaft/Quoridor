namespace Model {
    public class Node {

        public Node Parent;
        public int Level;
        
        public Node(int level, Node parent = null) {
            Parent = parent;
            Level = level;
        }

    }
}