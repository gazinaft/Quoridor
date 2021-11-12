using System;

namespace Model {
    public class Node {

        public Node Parent;
        public int Level;
        public ICommand Command;
        public float BestValue;
        public bool BackTrack = false;
        public bool Max;
        public Node BestNode = null;
        
        public Node(ICommand command, int level, Node parent = null, bool max = true) {
            Command = command;
            Parent = parent;
            Level = level;
            Max = max;
        }

        private void BestForDepth(Node child) {
            var childV = child.BestValue;
            if (BestValue > childV) {
                if (!Max) {
                    BestNode = child;
                    BestValue = childV;
                }
            }
            else {
                if (Max) {
                    BestNode = child;
                    BestValue = childV;
                }
            }
        }
        
        public void Undo(Game game) {
            Parent?.BestForDepth(this);
            Command.Undo(game);
            if(BackTrack) Parent?.Undo(game);
        }

    }
}