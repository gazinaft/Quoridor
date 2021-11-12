using System;
using System.Collections.Generic;
using Model.Services;
using Model.Strategy;
namespace Model {

    public delegate float SEV(Game game);
    public class ABTree: AI{
        
        private Node _root;
        private SEV _sev;
        private PathFindingService _pathFindingService;
        public ABTree(int depth) {
            
            _root = new Node(new EmptyCommand(), depth);
            
            _pathFindingService = new PathFindingService();

            _pathFindingService.SelectedAlgorithm = new BFS();

            _sev = GetStateSuccess;

        }

        public float GetStateSuccess(Game game) {

            return game.SecondPlayer.WallsCounter + _pathFindingService.SelectedAlgorithm.FindThePath(game.SecondPlayer, game.Board).Count 
                - game.FirstPlayer.WallsCounter - _pathFindingService.SelectedAlgorithm.FindThePath(game.FirstPlayer, game.Board).Count; 
        
        }
        
        private void MiniMax(Game game) {
            var stack = new Stack<Node>();
            stack.Push(_root);
            
            while (stack.Count != 0) {
                var current = stack.Pop();
                current.Command.Execute(game);
                if (current.Level == 0) {
                    current.BestValue = _sev(game);
                    current.Undo(game);
                }
                else {
                    var actions = game.GetLegalActions();
                    // LIFO will recursively go up because it will be the last child node iterating and go up
                    var backTrack = new Node(actions[0], current.Level - 1, current, !current.Max) { BackTrack = true };
                    stack.Push(backTrack);
                    for (int i = 1; i < actions.Count; i++) {
                        stack.Push(new Node(actions[i], current.Level - 1, current));
                    }
                }
            }
            
        }

        public ICommand GetBestMove(Game game) {

            //game.DoDisplayStep = false;
            // here we are going to make a deepClone of the game
            
            //Game gameClone = new Game();

            //gameClone.InActivePlayer = game.InActivePlayer;

            //gameClone.ActivePlayer = game.ActivePlayer;

            //gameClone.Board = game.Board;

            //gameClone.PlaceTheWallCommand = game.PlaceTheWallCommand;

            //gameClone.WallIsHorizontal = game.WallIsHorizontal;

            //gameClone.MovePlayerCommand = game.MovePlayerCommand;

            //gameClone.DoDisplayStep = false;

            //game.DoDisplayStep = false;
            
            MiniMax(game);

            //game.DoDisplayStep = true;
            //game.DoDisplayStep = true;
            return _root.BestNode.Command;
        }
    }
}