using System;
using System.Collections.Generic;
using Model.Services;
using Model.Strategy;
namespace Model {

    public delegate float SEV(GameStateModel game);
    public class ABTree: AI{
        
        private Node _root;
        private SEV _sev;
        private PathFindingService _pathFindingService;
        private int _depth;
        public ABTree(int depth) {

            _depth = depth;
            
            _pathFindingService = new PathFindingService();

            _pathFindingService.SelectedAlgorithm = new BFS();

            _sev = GetStateSuccess;

        }

        

        private float Minimax(Node node, ref float alpha, ref float beta) {
            if (node.Level == 0 || node.Gsm.IsTerminal()) return _sev(node.Gsm);

            var children = node.Gsm.GetChildren();

            if (node.Max) {
                float best = -1000;
 
                for (int i = 0; i < children.Count; i++) {
                    var child = new Node(children[i], node.Level - 1, false);
                    float val = Minimax(child, ref alpha, ref beta);
                    if (val > best) {
                        best = val;
                        node.BestNode = child;
                    }
                    alpha = Math.Max(alpha, best);
 
                    // Alpha Beta Pruning
                    // if (beta <= alpha)
                    //     break;
                }
                return best;
            }
            else {
                float best = 1000;
 
                // Recur for left and
                // right children
                for (int i = 0; i < children.Count; i++) {

                    var child = new Node(children[i], node.Level - 1);
                    float val = Minimax(child, ref alpha, ref beta);
                    if (val < best) {
                        best = val;
                        node.BestNode = child;
                    }
                    beta = Math.Min(beta, best);
 
                    // Alpha Beta Pruning
                    // if (beta <= alpha)
                    //     break;
                }
                return best;
            }
            
        }

        public ICommand GetBestMove(GameStateModel gsm) {
            _root = new Node(gsm, _depth);
            float max = 1000;
            float min = -1000;
            Minimax(_root, ref min, ref max);
            return _root.BestNode.Gsm.ComToGet;
        }
        
        private float GetStateSuccess(GameStateModel game) {
            var winLen = _pathFindingService.SelectedAlgorithm.FindThePath(game.HasToWin, game.Board).Count;
            var loseLen = _pathFindingService.SelectedAlgorithm.FindThePath(game.HasToLose, game.Board).Count;
            // return 5 * loseLen - 5 * winLen;
            return 5 / (winLen + 0.001f) - 5 / (loseLen + 0.01f);
        }

        // NonWorking iterative MiniMax
        // private void MiniMax(GameStateModel game) {
        //     var stack = new Stack<Node>();
        //     stack.Push(_root);
        //     
        //     while (stack.Count != 0) {
        //         var current = stack.Pop();
        //         current.Command.Execute(game);
        //         if (current.Level == 0) {
        //             current.BestValue = _sev(game);
        //             current.Undo(game);
        //         }
        //         else {
        //             var actions = game.GetLegalActions();
        //             if (game.ActivePlayer.VictoryRow == game.ActivePlayer.CurrentCell.Y) {
        //                 current.BestValue = game.HasToWin == game.ActivePlayer ? 1000 : -1000;
        //                 current.Undo(game);
        //                 continue;
        //             }
        //             // LIFO will recursively go up because it will be the last child node iterating and go up
        //             var backTrack = new Node(actions[0], current.Level - 1, current, !current.Max) { BackTrack = true };
        //             stack.Push(backTrack);
        //             for (int i = 1; i < actions.Count; i++) {
        //                 stack.Push(new Node(actions[i], current.Level - 1, current, !current.Max));
        //             }
        //         }
        //     }
        //     
        // }

    }
}