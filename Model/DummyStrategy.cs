using System;
using Model.Services;
namespace Model
{
    public class DummyStrategy : IPlayerStrategy
    {
        MoveValidationService _moveValidationService;

        int stepCounter;

        public DummyStrategy() {
            _moveValidationService = new MoveValidationService();
            stepCounter = 0;
        }

        public void Think(Game game)
        {
            Random r = new Random();

            if (stepCounter % 2 == 0) {

                var curIndexX = r.Next(1, game.Board.Corners.GetLength(0)-2);
                var curIndexY = r.Next(1, game.Board.Corners.GetLength(0)-2);
                var isHorizontal = r.Next(2);

                game.WallIsHorizontal = isHorizontal == 1;
                game.TheWallIsPlaced = true;
                game.SelectedCorner = game.Board.Corners[curIndexX, curIndexY];
                game.PlaceTheWall();
                game.TheWallIsPlaced = false;

            }
            else {

                int index = r.Next(game.Board.GetAvailableMoves(game.ActivePlayer).Count);
                game.SelectedCell = game.Board.GetAvailableMoves(game.ActivePlayer)[index];
                if (game.DoDisplayStep) {
                    
                    MovePlayerCommand command = new MovePlayerCommand(game.SelectedCell);
                    game._stepsHistory.AddLast(command);
                }

                game.TheWallIsPlaced = false;
                game.ChangeTheCell();

            }

            stepCounter++;
        }

        public void SendVictory(Game game) { }
        
    }
}
